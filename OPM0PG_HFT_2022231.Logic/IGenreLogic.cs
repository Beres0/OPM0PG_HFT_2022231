using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IGenreLogic
    {
        void CreateGenre(AlbumGenre genre);

        void DeleteGenre(int albumId, string genre);

        IEnumerable<AlbumPerGenreDTO> GetAlbumPerGenre();

        IEnumerable<ArtistPerGenreDTO> GetArtistPerGenre();

        IEnumerable<string> GetGenres();

        IEnumerable<AlbumGenre> ReadAllAlbumGenre();

        IEnumerable<ArtistGenreDTO> ReadAllArtistGenre();

        AlbumGenre ReadGenre(int albumId, string genre);
    }
}