using OPM0PG_HFT_2022231.Models.Support.Serialization;
using System.Net.Http;
using System.Text;
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

    public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
    {
        return await client.DeleteAsync(requestUri);
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await client.GetAsync(requestUri);
    }

    public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content)
    {
        return await client.PostAsync(requestUri, Serialize(content));
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content)
    {
        return await client.PutAsync(requestUri, Serialize(content));
    }

    private StringContent Serialize<T>(T content)
    {
        var json = ModelJsonSerializer.Serialize(content);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}