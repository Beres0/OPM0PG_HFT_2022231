using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IMediaLogic
    {
        void CreateAlbumMedia(AlbumMedia albumMedia);

        void CreateArtistMedia(ArtistMedia artistMedia);

        void DeleteAlbumMedia(int albumMediaId);

        void DeleteArtistMedia(int artistMediaId);

        AlbumMedia ReadAlbumMedia(int albumMediaId);

        IEnumerable<AlbumMedia> ReadAllAlbumMedia();

        IEnumerable<ArtistMedia> ReadAllArtistMedia();

        ArtistMedia ReadArtistMedia(int artistMediaId);

        void UpdateAlbumMedia(AlbumMedia albumMedia);

        void UpdateArtistMedia(ArtistMedia artistMedia);
    }
}