using OPM0PG_HFT_2022231.Client.Writers;

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
            MapperClient client = new MapperClient("http://localhost:15486/api/Meta/GetApiInterfaceMap", new RestService(), args);
            client.Writers.Add(new AlbumSummaryWriter());
            client.Writers.Add(new ArtistSummaryWriter());

            client.Run();
        }
    }
}