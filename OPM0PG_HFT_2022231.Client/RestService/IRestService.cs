using System.Net.Http;
using System.Threading.Tasks;

public interface IRestService
{
    Task<HttpResponseMessage> DeleteAsync(string requestUri);

    Task<HttpResponseMessage> GetAsync(string requestUri);

    Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content);

    Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content);
}