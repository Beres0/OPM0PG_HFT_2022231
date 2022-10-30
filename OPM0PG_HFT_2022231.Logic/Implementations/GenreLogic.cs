using OPM0PG_HFT_2022231.Logic.Internals;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class GenreLogic : BaseLogic, IGenreLogic
    {
        public GenreLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public IEnumerable<AlbumGenre> ReadAllAlbumGenre()
        {
            return repository.Genres.ReadAll();
        }

        public IEnumerable<ArtistGenreDTO> ReadAllArtistGenre()
        {
            return repository.Genres.ReadAll().Join
                (repository.Contributions.ReadAll(),
                (g) => g.AlbumId, (c) => c.AlbumId,
                (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                .Distinct();
        }

        public void AddGenre(int albumId, string genre)
        {
            Validator.ValidatePositiveNumber(albumId);
            Validator.ValidateRequiredText(genre);
            Validator.ValidateForeignKey(albumId, repository.Albums);

            repository.Genres.Create(new AlbumGenre { AlbumId = albumId, Genre = genre });
        }

        public void RemoveGenre(int albumId, string genre)
        {
            Validator.ValidatePositiveNumber(albumId);
            Validator.ValidateRequiredText(genre);
            repository.Genres.Delete(albumId, genre);
        }

        public IEnumerable<string> GetGenres()
        {
            return repository.Genres.ReadAll()
                 .Select(g => g.Genre).Distinct();
        }

        public IEnumerable<AlbumPerGenreDTO> GetAlbumPerGenre()
        {
            return repository.Genres.ReadAll()
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