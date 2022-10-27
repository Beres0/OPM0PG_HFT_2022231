using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class GenreLogic : IGenreLogic
    {
        IRepository<Contribution> contributions;
        IRepository<AlbumGenre> genres;

        public GenreLogic(IRepository<AlbumGenre> genres, IRepository<Contribution> contributions)
        {
            this.contributions = contributions;
            this.genres = genres;
        }

        public IEnumerable<AlbumGenre> ReadAllAlbumGenre()
        {
            return genres.ReadAll();
        }
        public IEnumerable<ArtistGenreDTO> ReadAllArtistGenre()
        {
            return genres.ReadAll().Join
                (contributions.ReadAll(),
                (g) => g.AlbumId, (c) => c.AlbumId,
                (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                .Distinct();
        }
        public void AddGenre(int albumId, string genre)
        {
            genres.Create(new AlbumGenre { AlbumId = albumId, Genre = genre });
        }
        public void RemoveGenre(int albumId, string genre)
        {
            genres.Delete(albumId, genre);
        }
        public IEnumerable<string> GetGenres()
        {
            return genres.ReadAll()
                 .Select(g => g.Genre).Distinct();
        }
        public IEnumerable<AlbumPerGenreDTO> GetAlbumPerGenre()
        {
            return genres.ReadAll()
                   .GroupBy(g => g.Genre)
                   .Select(g => new AlbumPerGenreDTO(g.Key, g.Count()));
        }
        public IEnumerable<ArtistPerGenreDTO> GetArtistPerGenre()
        {
            return ReadAllArtistGenre()
                   .GroupBy(g => g.Genre)
                   .Select(g => new ArtistPerGenreDTO(g.Key, g.Count()));
        }

    }
}
