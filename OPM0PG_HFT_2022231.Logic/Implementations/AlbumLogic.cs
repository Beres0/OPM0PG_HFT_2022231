using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static OPM0PG_HFT_2022231.Logic.Exceptions.ExceptionThrowHelper;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class AlbumLogic : IAlbumLogic
    {
        IRepository<Album> albums;
        IRepository<Part> parts;
        IRepository<Track> tracks;


        public AlbumLogic(IRepository<Album> albums,
                          IRepository<Part> parts,
                          IRepository<Track> tracks)
        {
            this.albums = albums;
            this.parts = parts;
            this.tracks = tracks;
        }

        public void CreateAlbum(Album album)
        {
            albums.Create(album);
        }
        public void CreatePart(Part part)
        {
            parts.Create(part);
        }
        public void CreateTrack(Track track)
        {
            tracks.Create(track);
        }
        public IEnumerable<Album> ReadAllAlbum()
        {
            return albums.ReadAll();
        }
        public Album ReadAlbum(int id)
        {
            return albums.Read(id);
        }
        public IEnumerable<Part> ReadAllPart()
        {
            return parts.ReadAll();
        }
        public Part ReadPart(int albumId, int partId)
        {

            return parts.Read(albumId, partId);
        }
        public IEnumerable<Track> ReadAllTrack()
        {
            return tracks.ReadAll();
        }
        public Track ReadTrack(int albumId, int partId, int trackId)
        {
            return tracks.Read(albumId, partId, trackId);
        }

        public void UpdateAlbum(Album album)
        {
            albums.Update(album);
        }
        public void UpdatePart(Part part)
        {
            parts.Update(part);
        }

        public void UpdateTrack(Track track)
        {
            tracks.Update(track);
        }

        public void DeleteAlbum(int id)
        {
            albums.Delete(id);
        }
        public void DeletePart(int albumId, int partId)
        {
            tracks.Delete(albumId, partId);
        }
        public void DeleteTrack(int albumId, int partId, int trackId)
        {
            tracks.Delete(albumId, partId, trackId);
        }

        public TimeSpan GetTotalDurationOfPart(int albumId, int partId)
        {
            return TimeSpan.FromMinutes(tracks.ReadAll()
                .Where(t => t.AlbumId == albumId && t.PartId == partId)
                .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }
        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            return TimeSpan.FromMinutes(tracks.ReadAll()
                .Where(t => t.AlbumId == albumId)
                .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }

        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return albums.ReadAll()
                .GroupBy(a => a.Year)
                .Select(g => new AlbumPerYearDTO(g.Key, g.Count()));
        }


    }

}