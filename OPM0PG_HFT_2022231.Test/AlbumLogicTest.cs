using NUnit.Framework;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Test.Repository;
using System;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test
{
    [TestFixture]
    internal class AlbumLogicTest
    {
        private AlbumLogic logic;
        private FakeMusicRepository repository;

        [Test]
        public void CreateAlbumTest()
        {
            Album nullAlbum = null;
            var emptyTitle = new Album() { Title = "" };
            var nullTitle = new Album() { Title = null };
            var longTitle = new Album { Title = new string('x', 256) };
            var invalidYear1 = new Album() { Title = "Test", Year = 1799 };
            var invalidYear2 = new Album() { Title = "Test", Year = 2101 };
            Assert.Throws<ArgumentNullException>(() => logic.CreateAlbum(nullAlbum));
            Assert.Throws<CreateException>(() => logic.CreateAlbum(emptyTitle));
            Assert.Throws<CreateException>(() => logic.CreateAlbum(nullTitle));
            Assert.Throws<CreateException>(() => logic.CreateAlbum(longTitle));
            Assert.Throws<CreateException>(() => logic.CreateAlbum(invalidYear1));
            Assert.Throws<CreateException>(() => logic.CreateAlbum(invalidYear2));

            var ok = new Album() { Title = "Ok", Year = 1950 };
            Assert.DoesNotThrow(() => logic.CreateAlbum(ok));
            Assert.That(repository.Read<Album>(ok.GetId()) is not null);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void CreatePartPositionInsertTest(int position)
        {
            var part = new Part() { Position = position, AlbumId = 27320, Title = "Test" };
            logic.CreatePart(part);
            var parts = repository.ReadAll<Part>().Where(t => t.AlbumId == 27320).OrderBy(t => t.Position).ToList();
            int max = parts.Max(t => t.Position);
            if (position < 1 || position > max)
            {
                Assert.That(parts[max - 1] == part);
            }
            else
            {
                Assert.That(parts[position - 1] == part);
            }
            Assert.That(parts.Select(t => t.Position).SequenceEqual(Enumerable.Range(1, 3)));
        }

        [Test]
        public void CreatePartTest()
        {
            Part nullPart = null;
            var nonExistAlbumId = new Part { AlbumId = 1001, Title = "Test" };
            var nullTitle = new Part { AlbumId = 36, Title = null };
            var emptyTitle = new Part { AlbumId = 36, Title = "" };
            var longTitle = new Part { AlbumId = 36, Title = new string('x', 256) };
            Assert.Throws<ArgumentNullException>(() => logic.CreatePart(nullPart));
            Assert.Throws<CreateException>(() => logic.CreatePart(nonExistAlbumId));
            Assert.Throws<CreateException>(() => logic.CreatePart(nullTitle));
            Assert.Throws<CreateException>(() => logic.CreatePart(emptyTitle));
            Assert.Throws<CreateException>(() => logic.CreatePart(longTitle));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(6)]
        [TestCase(12)]
        [TestCase(13)]
        public void CreateTrackPositionInsertTest(int position)
        {
            var track = new Track() { Position = position, PartId = 36, Title = "Test" };
            logic.CreateTrack(track);
            var tracks = repository.ReadAll<Track>().Where(t => t.PartId == 36).OrderBy(t => t.Position).ToList();
            int max = tracks.Max(t => t.Position);
            if (position < 1 || position > max)
            {
                Assert.That(tracks[max - 1] == track);
            }
            else
            {
                Assert.That(tracks[position - 1] == track);
            }
            Assert.That(tracks.Select(t => t.Position).SequenceEqual(Enumerable.Range(1, 12)));
        }

        [Test]
        public void CreateTrackTest()
        {
            Track nullTrack = null;
            var nonExistPartId = new Track { PartId = 1001, Title = "Test" };
            var nullTitle = new Track { PartId = 36, Title = null };
            var emptyTitle = new Track { PartId = 36, Title = "" };
            var longTitle = new Track { PartId = 36, Title = new string('x', 256) };
            Assert.Throws<ArgumentNullException>(() => logic.CreateTrack(nullTrack));
            Assert.Throws<CreateException>(() => logic.CreateTrack(nonExistPartId));
            Assert.Throws<CreateException>(() => logic.CreateTrack(nullTitle));
            Assert.Throws<CreateException>(() => logic.CreateTrack(emptyTitle));
            Assert.Throws<CreateException>(() => logic.CreateTrack(longTitle));
        }

        [Test]
        public void DeleteAlbumTest()
        {
            int nonExistAlbum = 3123;

            int ok = 395;
            Assert.Throws<DeleteException>(() => logic.DeleteAlbum(nonExistAlbum));

            Assert.DoesNotThrow(() => logic.DeleteTrack(ok));
            Assert.Throws<ReadException>(() => logic.ReadTrack(ok));
        }

        [Test]
        public void DeletePartByPositionTest()
        {
            int nonExistAlbum = 13213123;
            int nonExistPos = 4;
            int okAlbum = 27320;
            int okPos = 2;
            Assert.Throws<DeleteException>(() => logic.DeletePartByPosition(nonExistAlbum, okPos));
            Assert.Throws<DeleteException>(() => logic.DeletePartByPosition(okAlbum, nonExistPos));

            Assert.DoesNotThrow(() => logic.DeletePartByPosition(okAlbum, okPos));
        }

        [Test]
        public void DeletePartTest()
        {
            int nonExist = -1;
            Assert.Throws<DeleteException>(() => logic.DeletePart(nonExist));

            int ok = 42;
            Assert.DoesNotThrow(() => logic.DeletePart(ok));
            var parts = repository.ReadAll<Part>().Where(p => p.AlbumId == 27320);
            Assert.That(parts.Count() == 1);
            Assert.That(parts.First().Position == 1);
        }

        [Test]
        public void DeleteTrackByPosition()
        {
            int nonExistPart = 13213123;
            int nonExistPos = 40;
            int okPart = 36;
            int okPos = 9;
            Assert.Throws<DeleteException>(() => logic.DeleteTrackByPosition(nonExistPart, okPos));
            Assert.Throws<DeleteException>(() => logic.DeleteTrackByPosition(okPart, nonExistPos));

            Assert.DoesNotThrow(() => logic.DeleteTrackByPosition(okPart, okPos));
        }

        [Test]
        public void GetTotalDurationOfAlbumTest()
        {
            Assert.That(logic.GetTotalDurationOfAlbum(27320) == new TimeSpan(1, 7, 0));
        }

        [Test]
        public void GetTotalDurationOfPartTest()
        {
            Assert.That(logic.GetTotalDurationOfPart(40) == TimeSpan.FromMinutes(53));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new AlbumLogic(repository);
        }

        [Test]
        public void ReadAlbumTest()
        {
            int nonExist = 1323;
            Assert.Throws<ReadException>(() => logic.ReadAlbum(nonExist));

            int ok = 279762;
            Assert.DoesNotThrow(() => logic.ReadAlbum(ok));
        }

        [Test]
        public void ReadPartTest()
        {
            int nonExist = 1323;
            Assert.Throws<ReadException>(() => logic.ReadAlbum(nonExist));

            int ok = 42;
            Assert.DoesNotThrow(() => logic.ReadPart(ok));
        }

        [Test]
        public void ReadTrackByPosition()
        {
            int nonExistAlbumId = 1;
            int okAlbumId = 27320;
            int nonExistPartId = 123123;
            int okPartId = 2;
            int nonExistTrackId = 13133;
            int okTrackId = 3;

            Assert.Throws<ReadException>(() => logic.ReadTrackByPosition(nonExistAlbumId, 0, 0));
            Assert.Throws<ReadException>(() => logic.ReadTrackByPosition(okAlbumId, nonExistPartId, 0));
            Assert.Throws<ReadException>(() => logic.ReadTrackByPosition(okAlbumId, nonExistPartId, nonExistTrackId));
            Assert.Throws<ReadException>(() => logic.ReadTrackByPosition(okAlbumId, nonExistPartId, okTrackId));

            Assert.DoesNotThrow(() => logic.ReadTrackByPosition(okAlbumId, okPartId, okTrackId));
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void UpdateAlbum()
        {
            Album nullAlbum = null;
            var emptyTitle = new Album() { Id = 796235, Title = "" };
            var nullTitle = new Album() { Id = 796235, Title = null };
            var longTitle = new Album { Id = 796235, Title = new string('x', 256) };
            var invalidYear1 = new Album() { Id = 796235, Title = "Test", Year = 1799 };
            var invalidYear2 = new Album() { Id = 796235, Title = "Test", Year = 2101 };
            var nonExist = new Album() { Id = 31231242, Title = "Test", Year = 1985 };

            Assert.Throws<ArgumentNullException>(() => logic.UpdateAlbum(nullAlbum));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(emptyTitle));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(nullTitle));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(longTitle));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(invalidYear1));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(invalidYear2));
            Assert.Throws<UpdateException>(() => logic.UpdateAlbum(nonExist));

            var original = logic.ReadAlbum(796235);
            var ok = new Album() { Id = original.Id, Title = "updated", Year = 2000 };
            Assert.DoesNotThrow(() => logic.UpdateAlbum(ok));
            Assert.That(repository.Read<Album>(ok.Id).Title == "updated");
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void UpdatePartPositionInsertTest(int position)
        {
            var part = new Part() { Id = 42, Position = position, AlbumId = 27320, Title = "Test" };
            logic.UpdatePart(part);
            var parts = repository.ReadAll<Part>().Where(t => t.AlbumId == 27320).OrderBy(t => t.Position).ToList();
            int max = parts.Max(t => t.Position);
            if (position < 1 || position > max)
            {
                Assert.That(parts[max - 1] == part, "1");
            }
            else
            {
                Assert.That(parts[position - 1] == part, "2");
            }
            Assert.That(parts.Select(t => t.Position).SequenceEqual(Enumerable.Range(1, 2)), "3");
        }

        [Test]
        public void UpdatePartTest()
        {
            Part nullPart = null;
            var nonExistAlbumId = new Part { Id = 40, AlbumId = 1001, Title = "Test" };
            var nullTitle = new Part { Id = 40, AlbumId = 36, Title = null };
            var emptyTitle = new Part { Id = 40, AlbumId = 36, Title = "" };
            var longTitle = new Part { Id = 40, AlbumId = 36, Title = new string('x', 256) };
            var nonExistPart = new Part { Id = 44140, AlbumId = 36, Title = new string('x', 256) };
            Assert.Throws<ArgumentNullException>(() => logic.UpdatePart(nullPart));
            Assert.Throws<UpdateException>(() => logic.UpdatePart(nonExistAlbumId));
            Assert.Throws<UpdateException>(() => logic.UpdatePart(nullTitle));
            Assert.Throws<UpdateException>(() => logic.UpdatePart(emptyTitle));
            Assert.Throws<UpdateException>(() => logic.UpdatePart(longTitle));
            Assert.Throws<UpdateException>(() => logic.UpdatePart(nonExistPart));

            var original = logic.ReadPart(40);
            var ok = new Part() { Id = original.Id, Title = "updated", AlbumId = original.AlbumId, Position = original.Position };
            Assert.DoesNotThrow(() => logic.UpdatePart(ok));
            Assert.That(repository.Read<Part>(ok.Id).Title == "updated");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(2)]
        [TestCase(7)]
        [TestCase(11)]
        [TestCase(12)]
        public void UpdateTrackPositionInsertTest(int position)
        {
            var track = new Track() { Id = 393, Position = position, PartId = 36, Title = "Test" };
            logic.UpdateTrack(track);
            var tracks = repository.ReadAll<Track>().Where(t => t.PartId == 36).OrderBy(t => t.Position).ToList();
            int max = tracks.Max(t => t.Position);
            if (position < 1 || position > max)
            {
                Assert.That(tracks[max - 1] == track);
            }
            else
            {
                Assert.That(tracks[position - 1] == track);
            }
            Assert.That(tracks.Select(t => t.Position).SequenceEqual(Enumerable.Range(1, 11)));
        }

        [Test]
        public void UpdateTrackTest()
        {
            Track nullTrack = null;
            var nonExistTrack = new Track() { Id = 3123, Position = 3, PartId = 36, Title = "Test" };
            var nonExistPartId = new Track { Id = 392, PartId = 1001, Title = "Test" };
            var nullTitle = new Track { Id = 392, PartId = 36, Title = null };
            var emptyTitle = new Track { Id = 392, PartId = 36, Title = "" };
            var longTitle = new Track { Id = 392, PartId = 36, Title = new string('x', 256) };
            Assert.Throws<ArgumentNullException>(() => logic.UpdateTrack(nullTrack));
            Assert.Throws<UpdateException>(() => logic.UpdateTrack(nonExistTrack));
            Assert.Throws<UpdateException>(() => logic.UpdateTrack(nonExistPartId));
            Assert.Throws<UpdateException>(() => logic.UpdateTrack(nullTitle));
            Assert.Throws<UpdateException>(() => logic.UpdateTrack(emptyTitle));
            Assert.Throws<UpdateException>(() => logic.UpdateTrack(longTitle));
        }
    }
}