using NUnit.Framework;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Test.Repository;

namespace OPM0PG_HFT_2022231.Test
{
    internal class ContributionLogicTest
    {
        private ContributionLogic logic;
        private FakeMusicRepository repository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new ContributionLogic(repository);
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void AddContribution()
        {
            int negArtistId = -1;
            int negAlbumId = -1;
            int nonExistArtistId = 4124124;
            int nonExistAlbumId = 4124124;
            int okArtistId = 1775650;
            int okAlbumId = 1308061;

            Assert.Throws<CreateException>(() => logic.AddContribution(okAlbumId, negArtistId));
            Assert.Throws<CreateException>(() => logic.AddContribution(negAlbumId, okArtistId));
            Assert.Throws<CreateException>(() => logic.AddContribution(okAlbumId, nonExistArtistId));
            Assert.Throws<CreateException>(() => logic.AddContribution(nonExistAlbumId, okArtistId));
            Assert.Throws<CreateException>(() => logic.AddContribution(okArtistId, okAlbumId));

            Assert.DoesNotThrow(() => logic.AddContribution(27320, okArtistId));
        }

        [Test]
        public void RemoveContribution()
        {
            int negArtistId = -1;
            int negAlbumId = -1;
            int nonExistArtistId = 4124124;
            int nonExistAlbumId = 4124124;
            int okArtistId = 1775650;
            int okAlbumId = 1308061;

            Assert.Throws<DeleteException>(() => logic.RemoveContribution(okAlbumId, negArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(negAlbumId, okArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(okAlbumId, nonExistArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(nonExistAlbumId, okArtistId));
            Assert.Throws<DeleteException>(() => logic.RemoveContribution(okAlbumId, nonExistAlbumId));

            Assert.DoesNotThrow(() => logic.RemoveContribution(okAlbumId, okArtistId));
            Assert.That(!repository.Contributions.TryRead(new object[] { okAlbumId, okArtistId }, out var result));
        }
    }
}