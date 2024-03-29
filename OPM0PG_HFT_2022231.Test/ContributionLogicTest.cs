﻿using NUnit.Framework;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Test.Repository;

namespace OPM0PG_HFT_2022231.Test
{
    [TestFixture]
    internal class ContributionLogicTest
    {
        private ContributionLogic logic;
        private FakeMusicRepository repository;

        [Test]
        public void CreateContributionTest()
        {
            int nonExistArtistId = 4124124;
            int nonExistAlbumId = 4124124;
            int okArtistId = 1775650;
            int okAlbumId = 1308061;

            AssertCreateContributionException(okAlbumId, nonExistArtistId);
            AssertCreateContributionException(nonExistAlbumId, okArtistId);
            AssertCreateContributionException(okArtistId, okAlbumId);

            Assert.DoesNotThrow(() => logic.CreateContribution(new Contribution() { AlbumId = 27320, ArtistId = okArtistId }));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new ContributionLogic(repository);
        }

        [Test]
        public void ReadContributionTest()
        {
            int nonExistArtistId = 4124124;
            int nonExistAlbumId = 4124124;
            int okArtistId = 1775650;
            int okAlbumId = 1308061;

            Assert.Throws<ReadException>(() => logic.ReadContribution(okAlbumId, nonExistArtistId));
            Assert.Throws<ReadException>(() => logic.ReadContribution(nonExistAlbumId, okArtistId));
            Assert.Throws<ReadException>(() => logic.ReadContribution(okAlbumId, nonExistAlbumId));

            Assert.DoesNotThrow(() => logic.ReadContribution(okAlbumId, okArtistId));
            Assert.That(repository.TryRead(new object[] { okAlbumId, okArtistId }, out Contribution result));
        }

        [Test]
        public void RemoveContribution()
        {
            int nonExistArtistId = 4124124;
            int nonExistAlbumId = 4124124;
            int okArtistId = 1775650;
            int okAlbumId = 1308061;

            Assert.Throws<DeleteException>(() => logic.RemoveContribution(okAlbumId, nonExistArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(nonExistAlbumId, okArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(okAlbumId, nonExistAlbumId));

            Assert.DoesNotThrow(() => logic.RemoveContribution(okAlbumId, okArtistId));
            Assert.That(!repository.TryRead(new object[] { okAlbumId, okArtistId }, out Contribution result));
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        private void AssertCreateContributionException(int albumId, int artistId)
        {
            Assert.Throws<CreateException>(() => logic.CreateContribution(new Contribution() { AlbumId = albumId, ArtistId = artistId }));
        }
    }
}