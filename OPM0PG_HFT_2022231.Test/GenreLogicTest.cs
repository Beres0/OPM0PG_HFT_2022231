﻿using NUnit.Framework;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Test.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test
{
    [TestFixture]
    internal class GenreLogicTest
    {
        private GenreLogic logic;
        private FakeMusicRepository repository;

        [Test]
        public void CreateGenreTest()
        {
            int nonExistAlbumId = 1322313;
            int okAlbumId = 796235;
            string nullGenre = null;
            string emptyGenre = "";
            string longGenre = new string('x', 256);
            string okGenre = "Electronic";

            AssertCreateGenreException(nonExistAlbumId, okGenre);
            AssertCreateGenreException(okAlbumId, nullGenre);
            AssertCreateGenreException(okAlbumId, emptyGenre);
            AssertCreateGenreException(okAlbumId, longGenre);
            AssertCreateGenreException(okAlbumId, okGenre);

            Assert.DoesNotThrow(() => logic.CreateGenre(new AlbumGenre() { AlbumId = okAlbumId, Genre = "Test" }));
            Assert.That(repository.TryRead(new object[] { okAlbumId, "Test" }, out AlbumGenre result));
        }

        [Test]
        public void GetAlbumPerGenreTest()
        {
            Assert.That(repository.ReadAll<AlbumGenre>()
                     .GroupBy(g => g.Genre)
                     .Select(g => new AlbumPerGenreDTO(g.Key, g.Count()))
                     .SequenceEqual(logic.GetAlbumPerGenre()));
        }

        [Test]
        public void GetArtistPerGenre()
        {
            Assert.That(repository.ReadAll<AlbumGenre>().Join
                    (repository.ReadAll<Contribution>(),
                    (g) => g.AlbumId, (c) => c.AlbumId,
                    (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                   .Distinct()
                   .GroupBy(g => g.Genre)
                   .Select(g => new ArtistPerGenreDTO(g.Key, g.Count()))
                   .SequenceEqual(logic.GetArtistPerGenre()));
        }

        [Test]
        public void GetGenresTest()
        {
            Assert.That(repository.ReadAll<AlbumGenre>()
                .Select(g => g.Genre)
                .Distinct()
                .SequenceEqual(logic.GetGenres()));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new GenreLogic(repository);
        }

        [Test]
        public void ReadAllArtistGenreTest()
        {
            Assert.That(repository.ReadAll<AlbumGenre>().Join
                   (repository.ReadAll<Contribution>(),
                   (g) => g.AlbumId, (c) => c.AlbumId,
                   (g, c) => new ArtistGenreDTO(c.Artist, g.Genre))
                   .Distinct()
                   .SequenceEqual(logic.ReadAllArtistGenre()));
        }

        [Test]
        public void ReadGenreTest()
        {
            int nonExistAlbumId = 1322313;
            int okAlbumId = 796235;
            string nullGenre = null;
            string emptyGenre = "";
            string longGenre = new string('x', 256);
            string nonExistGenre = "Test";
            string okGenre = "Electronic";

            Assert.Throws<ReadException>(() => logic.ReadGenre(nonExistAlbumId, okGenre));
            Assert.Throws<ReadException>(() => logic.ReadGenre(okAlbumId, nullGenre));
            Assert.Throws<ReadException>(() => logic.ReadGenre(okAlbumId, emptyGenre));
            Assert.Throws<ReadException>(() => logic.ReadGenre(okAlbumId, longGenre));
            Assert.Throws<ReadException>(() => logic.ReadGenre(okAlbumId, nonExistGenre));

            Assert.DoesNotThrow(() => logic.ReadGenre(okAlbumId, okGenre));
            Assert.That(repository.TryRead(new object[] { okAlbumId, okGenre }, out AlbumGenre result));
        }

        [Test]
        public void RemoveGenreTest()
        {
            int nonExistAlbumId = 1322313;
            int okAlbumId = 796235;
            string nullGenre = null;
            string emptyGenre = "";
            string longGenre = new string('x', 256);
            string nonExistGenre = "Test";
            string okGenre = "Electronic";

            Assert.Throws<DeleteException>(() => logic.DeleteGenre(nonExistAlbumId, okGenre));
            Assert.Throws<DeleteException>(() => logic.DeleteGenre(okAlbumId, nullGenre));
            Assert.Throws<DeleteException>(() => logic.DeleteGenre(okAlbumId, emptyGenre));
            Assert.Throws<DeleteException>(() => logic.DeleteGenre(okAlbumId, longGenre));
            Assert.Throws<DeleteException>(() => logic.DeleteGenre(okAlbumId, nonExistGenre));

            Assert.DoesNotThrow(() => logic.DeleteGenre(okAlbumId, okGenre));
            Assert.That(!repository.TryRead(new object[] { okAlbumId, okGenre }, out AlbumGenre result));
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        private void AssertCreateGenreException(int albumId, string genre)
        {
            Assert.Throws<CreateException>(() => logic.CreateGenre(new AlbumGenre() { AlbumId = albumId, Genre = genre }));
        }
    }
}