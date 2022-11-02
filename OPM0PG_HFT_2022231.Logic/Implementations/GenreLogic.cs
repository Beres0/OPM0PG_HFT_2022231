using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Support;

using OPM0PG_HFT_2022231.Repository;
using System;
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
            try
            {
                Validator<AlbumGenre>.Validate(genre.AlbumId);
                Validator<AlbumGenre>.Validate(genre.Genre);
                CheckKeyExists(repository.Albums, genre.AlbumId);
                CheckKeyAlreadyAdded(repository.Genres, nameof(genre), genre.GetId());
                repository.Genres.Create(genre);
            }
            catch (Exception ex)
            {
                throw new CreateException(genre, ex);
            }
        }

        public void DeleteGenre(int albumId, string genre)
        {
            try
            {
                Validator<AlbumGenre>.Validate(albumId);
                Validator<AlbumGenre>.Validate(genre);
                CheckKeyExists(repository.Genres, "(albumId,genre)", albumId, genre);
                repository.Genres.Delete(albumId, genre);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(AlbumGenre), ex, albumId, genre);
            }
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

        public IEnumerable<string> GetGenres()
        {
            return repository.Genres.ReadAll()
                 .Select(g => g.Genre).Distinct();
        }

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

        public AlbumGenre ReadGenre(int albumId, string genre)
        {
            try
            {
                Validator<AlbumGenre>.Validate(albumId);
                Validator<AlbumGenre>.Validate(genre);
                CheckKeyExists(repository.Genres, "(albumId,genre)", albumId, genre);
                return repository.Genres.Read(albumId, genre);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(AlbumGenre), ex, albumId, genre);
            }
        }
    }
}