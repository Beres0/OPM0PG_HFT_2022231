using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IAlbumLogic
    {
        void CreateAlbum(Album album);

        void CreatePart(Part part);

        void CreateTrack(Track track);

        void DeleteAlbum(int albumid);

        void DeletePart(int partId);

        void DeletePartByPosition(int albumId, int partPosition);

        void DeleteTrack(int trackId);

        void DeleteTrackByPosition(int partId, int trackPosition);

        void DeleteTrackByPosition(int albumId, int partPosition, int trackPosition);

        IEnumerable<AlbumPerYearDTO> GetAlbumPerYear();

        TimeSpan GetTotalDurationOfAlbum(int albumId);

        TimeSpan GetTotalDurationOfPart(int partId);

        Album ReadAlbum(int albumId);

        IEnumerable<Album> ReadAllAlbum();

        IEnumerable<Part> ReadAllPart();

        IEnumerable<Track> ReadAllTrack();

        Part ReadPart(int partId);

        Part ReadPartByPosition(int albumId, int partPosition);

        Track ReadTrack(int trackId);

        Track ReadTrackByPosition(int partId, int trackPosition);

        Track ReadTrackByPosition(int albumId, int partPosition, int trackPosition);

        void UpdateAlbum(Album album);

        void UpdatePart(Part part);

        void UpdateTrack(Track track);
    }
}