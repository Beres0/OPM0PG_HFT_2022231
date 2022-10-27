using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class AlbumLogic : IAlbumLogic
    {
        public IRepository<int, Album> Albums { get; }
        public IRepository<object, Part> Parts { get; }
        public IRepository<object, Track> Tracks { get; }


        public AlbumLogic(IRepository<int, Album> albums,
                          IRepository<object, Part> parts,
                          IRepository<object, Track> tracks)
        {
            Albums = albums;
            Parts = parts;
            Tracks = tracks;
        }

        public void CreateAlbum(Album album)
        {
            Albums.Create(album);
        }
        public void CreatePart(Part part)
        {
            Parts.Create(part);
        }
        public void CreateTrack(Track track)
        {
            Tracks.Create(track);
        }
        public IEnumerable<Album> ReadAllAlbum()
        {
            return Albums.ReadAll();
        }
        public Album ReadAlbum(int id)
        {
            return Albums.Read(id);
        }
        public IEnumerable<Part> ReadAllPart()
        {
            return Parts.ReadAll();
        }
        public Part ReadPart(int albumid, int partId)
        {
            return Parts.Read(new { albumid, partId });
        }
        public IEnumerable<Track> ReadAllTrack()
        {
            return Tracks.ReadAll();
        }
        public Track ReadTrack(int albumId, int partId, int trackId)
        {
            return Tracks.Read(new { albumId, partId, trackId });
        }

        public void UpdateAlbum(Album album)
        {
            Albums.Update(album);
        }
        public void UpdatePart(Part part)
        {
            Parts.Update(part);
        }

        public void UpdateTrack(Track track)
        {
            Tracks.Update(track);
        }

        public void DeleteAlbum(int id)
        {
            Albums.Delete(id);
        }
        public void DeletePart(int albumId, int partId)
        {
            Tracks.Delete(new { albumId, partId });
        }
        public void DeleteTrack(int albumId, int partId, int trackId)
        {
            Tracks.Delete(new { albumId, partId, trackId });
        }

        public TimeSpan GetTotalDurationOfPart(int albumId, int partId)
        {
            return TimeSpan.FromMinutes(Tracks.ReadAll()
                .Where(t => t.AlbumId == albumId && t.PartId == partId)
                .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }
        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            return TimeSpan.FromMinutes(Tracks.ReadAll()
                .Where(t => t.AlbumId == albumId)
                .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }

        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return Albums.ReadAll()
                .GroupBy(a => a.Year)
                .Select(g => new AlbumPerYearDTO(g.Key, g.Count()));
        }


    }

}