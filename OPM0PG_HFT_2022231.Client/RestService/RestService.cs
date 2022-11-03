using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

    private StringContent Serialize<T>(T content,params JsonConverter[] converters)
    {
        var json = JsonConvert.SerializeObject(content, converters);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T content,params JsonConverter[] converters)
    {
        return await client.PostAsync(requestUri, Serialize(content,converters));
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string requestUri, T content, params JsonConverter[] converters)
    {
        return await client.PutAsync(requestUri, Serialize(content, converters));
    }
}