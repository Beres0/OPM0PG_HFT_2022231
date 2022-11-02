using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

class RestService : IRestService
{
    HttpClient client;
    public RestService()
    {
        client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue
            ("application/json"));
    }

    public async Task<HttpResponseMessage> PutAsync<T>(string url, T content)
    {
       return await client.PutAsJsonAsync(url, content);
    }
    public async Task<HttpResponseMessage> PostAsync<T>(string url, T content)
    {
        return await client.PostAsJsonAsync(url, content);
    }
    public async Task<HttpResponseMessage> DeleteAsync(string url)
    {
        return await client.DeleteAsync(url);
    }
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        return await client.GetAsync(url);
    }
}
