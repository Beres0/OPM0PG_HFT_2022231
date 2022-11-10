using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Support;

using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class GenreLogic : BaseLogic, IGenreLogic
    {
        public GenreLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public void CreateGenre(AlbumGenre genre)
        {
            CreateEntity(() =>
            {
                Validator<AlbumGenre>.Validate(genre.Genre);
                CheckKeyExists<Album>(genre.AlbumId);
                CheckKeyAlreadyAdded<AlbumGenre>(genre.GetId());
            }, genre);
        }

        public void DeleteGenre(int albumId, string genre)
        {
            DeleteEntity<AlbumGenre>(null, albumId, genre);
        }

        public IEnumerable<AlbumPerGenreDTO> GetAlbumPerGenre()
        {
            return ReadAllAlbumGenre()
                   .GroupBy(g => g.Genre)
                   .Select(g => new AlbumPerGenreDTO(g.Key, g.Count()));
        }

        public IEnumerable<ArtistPerGenreDTO> GetArtistPerGenre()
        {
            return ReadAllArtistGenre()
                   .GroupBy(g => g.Genre)
                   .Select(g => new ArtistPerGenreDTO(g.Key, g.Count()));
        }

        public IEnumerable<string> GetGenres()
        {
            return ReadAllAlbumGenre()
                 .Select(g => g.Genre).Distinct();
        }

        public IEnumerable<AlbumGenre> ReadAllAlbumGenre()
        {
            return repository.ReadAll<AlbumGenre>();
        }

        public IEnumerable<ArtistGenreDTO> ReadAllArtistGenre()
        {
            return ReadAllAlbumGenre().Join
                (repository.ReadAll<Contribution>(),
                (g) => g.AlbumId, (c) => c.AlbumId,
                (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                .Distinct();
        }

        public AlbumGenre ReadGenre(int albumId, string genre)
        {
            return ReadEntity<AlbumGenre>(null, albumId, genre);
        }
    }
}