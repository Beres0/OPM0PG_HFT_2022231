using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;

namespace OPM0PG_HFT_2022231.Client.Writers
{
    public class ArtistSummaryWriter : ConsoleTypeWriter
    {
        public ArtistSummaryWriter() : base(typeof(ArtistSummaryDTO))
        { }

        protected override void WriteMethod(object obj)
        {
            var summary = (ArtistSummaryDTO)obj;
            Console.WriteLine($"[{summary.Id}] {summary.Name}");
            WriteCollection("Members", summary.Members, (m) => $"[{m.Id}] {m.Name} - {m.Active}");
            WriteCollection("Bands", summary.Bands, (m) => $"[{m.Id}] {m.Name} - {m.IsMemberActive}");
            WriteCollection("Genres", summary.Genres, (g) => g);
            WriteCollection("Albums", summary.Albums, (a) =>
            $"[{a.Id}] {a.Title} ({a.Year}) - Tracks({a.NumberOfTracks}) Duration({a.TotalDuration})");
        }
    }
}