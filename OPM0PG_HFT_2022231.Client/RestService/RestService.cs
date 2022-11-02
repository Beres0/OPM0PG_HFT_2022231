using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

internal class RestService : IRestService
{
    private HttpClient client;

    public RestService()
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
            ("application/json"));
    }

    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        return await client.DeleteAsync(url);
    }

    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await client.GetAsync(url);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string url, T content)
    {
        return await client.PostAsJsonAsync(url, content);
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string url, T content)
    {
        return await client.PutAsJsonAsync(url, content);
    }
}