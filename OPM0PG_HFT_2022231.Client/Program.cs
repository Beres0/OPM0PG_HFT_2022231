using OPM0PG_HFT_2022231.Client.Readers;
using OPM0PG_HFT_2022231.Client.Writers;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support.JsonConverters;

namespace OPM0PG_HFT_2022231.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Run(args);
        }

        private static void Run(string[] args)
        {
            ReflectedClient client = new ReflectedClient("http://localhost:15486/api/Meta/GetApiInterfaceMap", new RestService(), args);
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

            client.JsonConverters.Add(new NullableDurationJsonConverter());
            client.JsonConverters.Add(new DurationJsonConverter());
            client.JsonConverters.Add(new HttpMethodTypeConverter());
            client.Run();
        }
    }
}