using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IAlbumLogic
    {
        IRepository<int, Album> Albums { get; }
        IRepository<object, Part> Parts { get; }
        IRepository<object, Track> Tracks { get; }

        void CreateAlbum(Album album);
        void CreatePart(Part part);
        void CreateTrack(Track track);
        void DeleteAlbum(int id);
        void DeletePart(int albumId, int partId);
        void DeleteTrack(int albumId, int partId, int trackId);
        IEnumerable<AlbumPerYearDTO> GetAlbumPerYear();
        TimeSpan GetTotalDurationOfAlbum(int albumId);
        TimeSpan GetTotalDurationOfPart(int albumId, int partId);
        Album ReadAlbum(int id);
        IEnumerable<Album> ReadAllAlbum();
        IEnumerable<Part> ReadAllPart();
        IEnumerable<Track> ReadAllTrack();
        Part ReadPart(int albumid, int partId);
        Track ReadTrack(int albumId, int partId, int trackId);
        void UpdateAlbum(Album album);
        void UpdatePart(Part part);
        void UpdateTrack(Track track);
    }
}