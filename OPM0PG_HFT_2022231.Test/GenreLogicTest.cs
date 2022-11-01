using NUnit.Framework;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Test.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test
{
    internal class GenreLogicTest
    {
        private GenreLogic logic;
        private FakeMusicRepository repository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new GenreLogic(repository);
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void AddGenreTest()
        {
            int negAlbumid = -1;
            int nonExistAlbumId = 1322313;
            int okAlbumId = 796235;
            string nullGenre = null;
            string emptyGenre = "";
            string longGenre = new string('x', 256);
            string okGenre = "Electronic";

            Assert.Throws<CreateException>(() => logic.AddGenre(negAlbumid, okGenre));
            Assert.Throws<CreateException>(() => logic.AddGenre(nonExistAlbumId, okGenre));
            Assert.Throws<CreateException>(() => logic.AddGenre(okAlbumId, nullGenre));
            Assert.Throws<CreateException>(() => logic.AddGenre(okAlbumId, emptyGenre));
            Assert.Throws<CreateException>(() => logic.AddGenre(okAlbumId, longGenre));
            Assert.Throws<CreateException>(() => logic.AddGenre(okAlbumId, okGenre));

            Assert.DoesNotThrow(() => logic.AddGenre(okAlbumId, "Test"));
            Assert.That(repository.Genres.TryRead(new object[] { okAlbumId, "Test" }, out var result));
        }

        [Test]
        public void RemoveGenreTest()
        {
            int negAlbumid = -1;
            int nonExistAlbumId = 1322313;
            int okAlbumId = 796235;
            string nullGenre = null;
            string emptyGenre = "";
            string longGenre = new string('x', 256);
            string nonExistGenre = "Test";
            string okGenre = "Electronic";

            Assert.Throws<DeleteException>(() => logic.RemoveGenre(negAlbumid, okGenre));
            Assert.Throws<DeleteException>(() => logic.RemoveGenre(nonExistAlbumId, okGenre));
            Assert.Throws<DeleteException>(() => logic.RemoveGenre(okAlbumId, nullGenre));
            Assert.Throws<DeleteException>(() => logic.RemoveGenre(okAlbumId, emptyGenre));
            Assert.Throws<DeleteException>(() => logic.RemoveGenre(okAlbumId, longGenre));
            Assert.Throws<DeleteException>(() => logic.RemoveGenre(okAlbumId, nonExistGenre));

            Assert.DoesNotThrow(() => logic.RemoveGenre(okAlbumId, okGenre));
            Assert.That(!repository.Genres.TryRead(new object[] { okAlbumId, okGenre }, out var result));
        }

        [Test]
        public void GetGenresTest()
        {
            Assert.That(repository.Genres.ReadAll()
                .Select(g => g.Genre)
                .Distinct()
                .SequenceEqual(logic.GetGenres()));
        }

        [Test]
        public void GetAlbumPerGenreTest()
        {
            Assert.That(repository.Genres.ReadAll()
                     .GroupBy(g => g.Genre)
                     .Select(g => new AlbumPerGenreDTO(g.Key, g.Count()))
                     .SequenceEqual(logic.GetAlbumPerGenre()));
        }

        [Test]
        public void ReadAllArtistGenreTest()
        {
            Assert.That(repository.Genres.ReadAll().Join
                   (repository.Contributions.ReadAll(),
                   (g) => g.AlbumId, (c) => c.AlbumId,
                   (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                   .Distinct()
                   .SequenceEqual(logic.ReadAllArtistGenre()));
        }

        [Test]
        public void GetArtistPerGenre()
        {
            Assert.That(repository.Genres.ReadAll().Join
                    (repository.Contributions.ReadAll(),
                    (g) => g.AlbumId, (c) => c.AlbumId,
                    (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                   .Distinct()
                   .GroupBy(g => g.Genre)
                   .Select(g => new ArtistPerGenreDTO(g.Key, g.Count()))
                   .SequenceEqual(logic.GetArtistPerGenre()));
        }
    }
}