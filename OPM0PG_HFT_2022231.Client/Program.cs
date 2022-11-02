using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using OPM0PG_HFT_2022231.Client.Readers;
using OPM0PG_HFT_2022231.Client.Writers;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;
using System.Net.Http;

namespace OPM0PG_HFT_2022231.Client
{
    internal class Program
    {

        private static void ClientTest(string[] args)
        {
            ApiClientGenerator client = new ApiClientGenerator("OPM0PG_HFT_2022231.Endpoint.dll", "http://localhost:15486/api/", new RestService(),args);
            client.Writers.Add(new AlbumSummaryWriter());
            client.Writers.Add(new ArtistSummaryWriter());

            client.Readers.Add(new EntityReader<Album>());
            client.Readers.Add(new EntityReader<Artist>());
            client.Readers.Add(new EntityReader<AlbumGenre>());
            client.Readers.Add(new EntityReader<Membership>());
            client.Readers.Add(new EntityReader<Contribution>());
            client.Readers.Add(new EntityReader<Part>());
            client.Readers.Add(new EntityReader<Release>());
            client.Readers.Add(new EntityReader<Track>());
            client.Show();
        }
        private static void Main(string[] args)
        {
            ClientTest(args);
        }
    }
}