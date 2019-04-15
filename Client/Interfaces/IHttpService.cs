using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Interfaces
{
    public interface IHttpService
    {
        string BearerToken { get; set; }
        Task<T> Get<T>();
        Task<T> Post<T,TBody>(TBody body);
        Task<T> Put<T,TBody>(TBody body);
        Task<T> Delete<T,TBody>(TBody body);
    }
}
