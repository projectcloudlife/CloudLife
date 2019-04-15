using Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Services
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
                return client.PostAsync($"{Host}/{path}", MakeContent(body));
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
            return new StringContent(JsonConvert.SerializeObject(obj));
        }

        async Task<T> Request<T>(Func<HttpClient, Task<HttpResponseMessage>> makeRequest)
        {
            HttpClient client = new HttpClient();
            
            if(string.IsNullOrEmpty(JWTBearerToken) == false)
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
