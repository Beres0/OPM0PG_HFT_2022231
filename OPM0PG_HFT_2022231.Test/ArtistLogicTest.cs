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
        }

        [Test]
        public void CreateMembershipTest()
        {
            Membership nullMembership = null;
            var sameId = new Membership { BandId = 142412, MemberId = 142412 };
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var circularRefBandId = new Membership { BandId = 480658, MemberId = 1328293 };
            var circularRefMemberId = new Membership { BandId = 1775650, MemberId = 58955 };
            var alreadyAdded = new Membership { BandId = 1775650, MemberId = 668288 };

            Assert.Throws<ArgumentNullException>(() => logic.CreateMembership(nullMembership));
            Assert.Throws<CreateException>(() => logic.CreateMembership(sameId));
            Assert.Throws<CreateException>(() => logic.CreateMembership(nonExistBandId));
            Assert.Throws<CreateException>(() => logic.CreateMembership(nonExistMemberId));
            Assert.Throws<CreateException>(() => logic.CreateMembership(circularRefBandId));
            Assert.Throws<CreateException>(() => logic.CreateMembership(circularRefMemberId));
            Assert.Throws<CreateException>(() => logic.CreateMembership(alreadyAdded));

            var ok = new Membership { BandId = 69226, MemberId = 400589 };
            Assert.DoesNotThrow(() => logic.CreateMembership(ok));

            Assert.That(repository.TryRead(ok.GetId(), out Membership okExists));
        }

        [Test]
        public void DeleteArtistTest()
        {
            int nonExistArtist = 40;
            Assert.Throws<DeleteException>(() => logic.DeleteArtist(nonExistArtist));

            Artist ok = logic.ReadArtist(308509);
            Assert.DoesNotThrow(() => logic.DeleteArtist(ok.Id));
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
            int id = 69226;
            var members = logic.GetMembers(id).Select(m => m.Id);

            Assert.That(members.Contains(244386));
            Assert.That(members.Contains(480658));
            Assert.That(members.Count() == 2);
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new ArtistLogic(repository);
        }

        [Test]
        public void ReadArtistTest()
        {
            var notFound = 10;
            Assert.Throws<ReadException>(() => logic.ReadArtist(notFound));

            var ok = 58955;
            Assert.DoesNotThrow(() => logic.ReadArtist(ok));
        }

        [Test]
        public void ReadMembershipTest()
        {
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var nonExistMembership = new Membership { BandId = 69226, MemberId = 308509 };

            AssertMembershipReadException(nonExistBandId);
            AssertMembershipReadException(nonExistMemberId);
            AssertMembershipReadException(nonExistMembership);

            Membership ok = repository.Read<Membership>(58955, 142412);
            Assert.DoesNotThrow(() => logic.ReadMembership(ok.BandId, ok.MemberId));
        }

        [Test]
        public void RemoveMembershipTest()
        {
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var nonExistMembership = new Membership { BandId = 69226, MemberId = 308509 };

            AssertMembershipDeleteException(nonExistBandId);
            AssertMembershipDeleteException(nonExistMemberId);
            AssertMembershipDeleteException(nonExistMembership);

            Membership deleted = repository.ReadAll<Membership>().Take(5).Last();
            Assert.DoesNotThrow(() => logic.DeleteMembership(deleted.BandId, deleted.MemberId));
            Assert.That(!logic.ReadAllMembership().Contains(deleted));
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void UpdateArtistTest()
        {
            Artist artist = new Artist() { Id = 668288, Name = "Luis Vasquez" };

            Artist nullArtist = null;
            var notFound = new Artist() { Id = 10, Name = "Test" };
            var nullName = new Artist() { Id = artist.Id, Name = null };
            var emptyName = new Artist() { Id = artist.Id, Name = "" };
            var longName = new Artist() { Id = artist.Id, Name = new string('x', 256) };

            Assert.Throws<ArgumentNullException>(() => logic.UpdateArtist(nullArtist));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(notFound));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(nullName));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(emptyName));
            Assert.Throws<UpdateException>(() => logic.UpdateArtist(longName));

            var ok = new Artist() { Id = 668288, Name = "updated" };
            Assert.DoesNotThrow(() => logic.UpdateArtist(ok));
            Assert.That(logic.ReadArtist(ok.Id).Name == "updated");
        }

        [Test]
        public void UpdateMembershipTest()
        {
            Membership nullMembership = null;
            var nonExistBandId = new Membership { BandId = 10, MemberId = 244386 };
            var nonExistMemberId = new Membership { BandId = 58955, MemberId = 30 };
            var nonExistMembership = new Membership { BandId = 69226, MemberId = 308509 };

            Assert.Throws<ArgumentNullException>(() => logic.UpdateMembership(nullMembership));
            Assert.Throws<UpdateException>(() => logic.UpdateMembership(nonExistBandId));
            Assert.Throws<UpdateException>(() => logic.UpdateMembership(nonExistMemberId));
            Assert.Throws<UpdateException>(() => logic.UpdateMembership(nonExistMembership));

            var trueSetFalse = new Membership() { BandId = 58955, MemberId = 142412 };
            logic.UpdateMembership(trueSetFalse);
            Assert.That(!repository.Read<Membership>(trueSetFalse.BandId, trueSetFalse.MemberId).Active);

            var falseSetTrue = new Membership() { BandId = 58955, MemberId = 400589, Active = true };
            logic.UpdateMembership(falseSetTrue);
            Assert.That(repository.Read<Membership>(falseSetTrue.BandId, falseSetTrue.MemberId).Active);
        }

        private void AssertMembershipDeleteException(Membership membership)
        {
            Assert.Throws<DeleteException>(() =>
                    logic.DeleteMembership(membership.BandId, membership.MemberId));
        }

        private void AssertMembershipReadException(Membership membership)
        {
            Assert.Throws<ReadException>(() =>
                    logic.ReadMembership(membership.BandId, membership.MemberId));
        }
    }
}