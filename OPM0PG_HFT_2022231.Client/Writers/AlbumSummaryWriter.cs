using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;

namespace OPM0PG_HFT_2022231.Client.Writers
{
    public class AlbumSummaryWriter : ConsoleTypeWriter
    {
        public AlbumSummaryWriter() : base(typeof(AlbumSummaryDTO))
        { }

        protected override void WriteMethod(object obj)
        {
            var summary = (AlbumSummaryDTO)obj;
            Console.WriteLine($"[{summary.Id}] {summary.Title}({summary.Year})");

            Console.WriteLine("Contributors:");
            foreach (var contributor in summary.Contributors)
            {
                Console.WriteLine($"[{contributor.Id}] {contributor.Name}");
            }
            Console.WriteLine();
            Console.WriteLine($"Genres: {string.Join(", ", summary.Genres)}");
            Console.WriteLine();
            Console.WriteLine("Tracks:");
            foreach (var part in summary.Parts)
            {
                Console.WriteLine($"\t[{part.Id}] {part.Position}. {part.Title} - {part.Duration}");
                foreach (var track in part.Tracks)
                {
                    Console.WriteLine($"\t\t[{track.Id}] {track.Position}. {track.Title} - {track.Duration}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Releases:");
            foreach (var release in summary.Releases)
            {
                Console.WriteLine($"\t[{release.Id}] {release.Publisher} - {release.Country} ({release.ReleaseYear})");
            }
        }
    }
}