using ClientLogic.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.Services
{
    public class HttpService : IHttpService
    {

        public HttpService(IConfigurationService configurationService)
        {
            var config = configurationService.GetAppConfiguration().Result;
            Host = config.Host;
        }

        public string Host { get; private set; }
        public string JWTBearerToken { get; set; }

        public Task<T> Delete<T>(string path)
        {
            return Request<T>(client =>
            {
                return client.DeleteAsync($"{Host}/{path}");
            });
        }

        public Task<T> Get<T>(string path)
        {
            return Request<T>(client =>
            {
                return client.GetAsync($"{Host}/{path}");
            });
        }

        public Task<T> Post<T, TBody>(string path, TBody body)
        {
            return Request<T>(client =>
            {
                var url = $"{Host}/{path}";
                return client.PostAsync( url, MakeContent(body));
            });
        }

        public Task<T> Put<T, TBody>(string path, TBody body)
        {
            return Request<T>(client =>
            {
                return client.PutAsync($"{Host}/{path}", MakeContent(body));
            });
        }

        HttpContent MakeContent<T>(T obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj),Encoding.UTF8, "application/json");
        }

        async Task<T> Request<T>(Func<HttpClient, Task<HttpResponseMessage>> makeRequest)
        {
            HttpClient client = new HttpClient();

            if (string.IsNullOrEmpty(JWTBearerToken) == false)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"bearer {JWTBearerToken}");
            }

            var response = await makeRequest(client);

            var bodyString = await response.Content.ReadAsStringAsync();

            var bodyObject = JsonConvert.DeserializeObject<T>(bodyString);

            return bodyObject;
        }
        
    }
}
