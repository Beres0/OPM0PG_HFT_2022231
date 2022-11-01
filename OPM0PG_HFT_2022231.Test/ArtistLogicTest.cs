using NUnit.Framework;
using NUnit.Framework.Internal;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Test.Repository;
using System;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test
{
    [TestFixture]
    internal class ArtistLogicTest
    {
        private ArtistLogic logic;
        private FakeMusicRepository repository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new ArtistLogic(repository);
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void CreateArtistTest()
        {
            Artist nullArtist = null;
            var nullName = new Artist() { Name = null };
            var emptyName = new Artist() { Name = "" };
            var longName = new Artist() { Name = new string('x', 256) };
            Assert.Throws<ArgumentNullException>(() => logic.CreateArtist(nullArtist));
            Assert.Throws<CreateException>(() => logic.CreateArtist(nullName));
            Assert.Throws<CreateException>(() => logic.CreateArtist(emptyName));
            Assert.Throws<CreateException>(() => logic.CreateArtist(longName));

            var ok = new Artist() { Id = 10, Name = "Teszt" };
            Assert.DoesNotThrow(() => logic.CreateArtist(ok));
            //repository.Artists.Delete(10);
        }

        private void AssertMembershipCreateException(Membership membership)
        {
            Assert.Throws<CreateException>(() =>
                    logic.AddMembership(membership.BandId, membership.MemberId));
        }

        [Test]
        public void AddMembershipTest()
        {
            var sameId = new Membership { BandId = 142412, MemberId = 142412 };
            var nonPosBandId = new Membership { BandId = -10, MemberId = 142412 };
            var nonPosMemberId = new Membership { BandId = 58955, MemberId = -10 };
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var circularRefBandId = new Membership { BandId = 480658, MemberId = 1328293 };
            var circularRefMemberId = new Membership { BandId = 1775650, MemberId = 58955 };
            var alreadyAdded = new Membership { BandId = 1775650, MemberId = 668288 };

            AssertMembershipCreateException(sameId);
            AssertMembershipCreateException(nonPosBandId);
            AssertMembershipCreateException(nonPosMemberId);
            AssertMembershipCreateException(nonExistBandId);
            AssertMembershipCreateException(nonExistMemberId);
            AssertMembershipCreateException(circularRefBandId);
            AssertMembershipCreateException(circularRefMemberId);
            AssertMembershipCreateException(alreadyAdded);

            var ok = new Membership { BandId = 69226, MemberId = 400589 };
            Assert.DoesNotThrow(() => logic.AddMembership(ok.BandId, ok.MemberId));

            Assert.That(repository.Memberships.TryRead(new object[] { ok.BandId, ok.MemberId }, out Membership okExists) && okExists.Active == true);
            //repository.Memberships.Delete(ok.BandId,ok.MemberId);
        }

        [Test]
        public void ReadArtistTest()
        {
            var nonPositive = -10;
            var notFound = 10;
            Assert.Throws<ReadException>(() => logic.ReadArtist(notFound));
            Assert.Throws<ReadException>(() => logic.ReadArtist(nonPositive));

            var ok = 58955;
            Assert.DoesNotThrow(() => logic.ReadArtist(ok));
        }

        [Test]
        public void UpdateArtistTest()
        {
            Artist artist = new Artist() { Id = 668288, Name = "Luis Vasquez" };

            Artist nullArtist = null;
            var nonPos = new Artist() { Id = -1, Name = null };
            var notFound = new Artist() { Id = 10, Name = "Test" };
            var nullName = new Artist() { Id = artist.Id, Name = null };
            var emptyName = new Artist() { Id = artist.Id, Name = "" };
            var longName = new Artist() { Id = artist.Id, Name = new string('x', 256) };

            Assert.Throws<ArgumentNullException>(() => logic.UpdateArtist(nullArtist));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(nonPos));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(notFound));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(nullName));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(emptyName));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(longName));

            var ok = new Artist() { Id = 668288, Name = "updated" };
            Assert.DoesNotThrow(() => logic.UpdateArtist(ok));
            Assert.That(logic.ReadArtist(ok.Id).Name == "updated");
            //logic.UpdateArtist(artist);
        }

        private void AsserMembershipUpdateException(Membership membership)
        {
            Assert.Throws<UpdateException>(() =>
                    logic.SetMembershipStatus(membership.BandId, membership.MemberId, false));
        }

        [Test]
        public void SetMemberShipStatusTest()
        {
            var nonPosBandId = new Membership { BandId = -10, MemberId = 142412 };
            var nonPosMemberId = new Membership { BandId = 58955, MemberId = -10 };
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var nonExistMembership = new Membership { BandId = 69226, MemberId = 308509 };

            AsserMembershipUpdateException(nonPosBandId);
            AsserMembershipUpdateException(nonPosMemberId);
            AsserMembershipUpdateException(nonExistBandId);
            AsserMembershipUpdateException(nonExistMemberId);
            AsserMembershipUpdateException(nonExistMembership);

            var trueSetFalse = new Membership() { BandId = 58955, MemberId = 142412 };
            logic.SetMembershipStatus(trueSetFalse.BandId, trueSetFalse.MemberId, false);
            Assert.That(!repository.Memberships.Read(trueSetFalse.BandId, trueSetFalse.MemberId).Active);
            //repository.Memberships.Read(trueSetFalse.BandId, trueSetFalse.MemberId).Active = true;

            var falseSetTrue = new Membership() { BandId = 58955, MemberId = 400589 };
            logic.SetMembershipStatus(falseSetTrue.BandId, falseSetTrue.MemberId, true);
            Assert.That(repository.Memberships.Read(falseSetTrue.BandId, falseSetTrue.MemberId).Active);
            //repository.Memberships.Read(falseSetTrue.BandId, falseSetTrue.MemberId).Active = false;
        }

        [Test]
        public void DeleteArtistTest()
        {
            int nonExistArtist = 40;
            Assert.Throws<DeleteException>(() => logic.DeleteArtist(nonExistArtist));

            Artist ok = logic.ReadArtist(308509);
            Assert.DoesNotThrow(() => logic.DeleteArtist(ok.Id));
            //repository.Artists.Create(ok);
        }

        [Test]
        public void RemoveMembershipTest()
        {
            var nonPosBandId = new Membership { BandId = -10, MemberId = 142412 };
            var nonPosMemberId = new Membership { BandId = 58955, MemberId = -10 };
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var nonExistMembership = new Membership { BandId = 69226, MemberId = 308509 };

            AssertMembershipDeleteException(nonPosBandId);
            AssertMembershipDeleteException(nonPosMemberId);
            AssertMembershipDeleteException(nonExistBandId);
            AssertMembershipDeleteException(nonExistMemberId);
            AssertMembershipDeleteException(nonExistMembership);

            Membership deleted = repository.Memberships.ReadAll().Take(5).Last();
            Assert.DoesNotThrow(() => logic.RemoveMembership(deleted.BandId, deleted.MemberId));
            Assert.That(logic.ReadAllMembership().Contains(deleted));
            //repository.Memberships.Create(deleted);
        }

        private void AssertMembershipDeleteException(Membership membership)
        {
            Assert.Throws<DeleteException>(() =>
                    logic.RemoveMembership(membership.BandId, membership.MemberId));
        }

        [Test]
        public void GetBandsTest()
        {
            var bands = logic.GetBands().Select(b => b.Id);
            Assert.That(bands.Contains(1775650));
            Assert.That(bands.Contains(69226));
            Assert.That(bands.Contains(58955));
            Assert.That(bands.Count() == 3);
        }

        [Test]
        public void GetMembersTest()
        {
            int nonPosId = -10;
            Assert.Throws<ReadException>(() => logic.GetMembers(nonPosId));

            int id = 69226;
            var members = logic.GetMembers(id).Select(m => m.Id);

            Assert.That(members.Contains(244386));
            Assert.That(members.Contains(480658));
            Assert.That(members.Count() == 2);
        }
    }
}