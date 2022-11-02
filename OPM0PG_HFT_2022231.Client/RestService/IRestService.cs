using System.Net.Http;
using System.Threading.Tasks;

public interface IRestService
{
    Task<HttpResponseMessage> DeleteAsync(string url);

    Task<HttpResponseMessage> GetAsync(string url);

    Task<HttpResponseMessage> PostAsync<T>(string url, T content);

    Task<HttpResponseMessage> PutAsync<T>(string url, T content);
}