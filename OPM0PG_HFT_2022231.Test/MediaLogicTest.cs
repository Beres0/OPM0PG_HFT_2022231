using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OPM0PG_HFT_2022231.Logic.Implementations;
using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using OPM0PG_HFT_2022231.Test.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test
{
    [TestFixture]
    internal class MediaLogicTest
    {
        private MediaLogic logic;
        private Mock<IMusicRepository> mock;

        [Test]
        public void CreateMediaTest()
        {
            Album value = new Album()
            {
                Id = 13
            };
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out value)).Returns(true);

            AlbumMedia test = new AlbumMedia()
            {
                AlbumId = 13,
                Uri = "https://www.google.hu/"
            };

            Assert.DoesNotThrow(() => logic.CreateAlbumMedia(test));
        }

        [Test]
        public void CreateMediaTestForeignKeyNonExist()
        {
            Album foreign = null;
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out foreign)).Returns(false);

            AlbumMedia test = new AlbumMedia()
            {
                AlbumId = 131,
                Uri = "https://www.google.hu/"
            };

            Assert.Throws<CreateException>(() => logic.CreateAlbumMedia(test));
        }

        [Test]
        public void CreateMediaTestInvalidUri()
        {
            AlbumMedia test = new AlbumMedia()
            {
                Uri = "bad:("
            };

            Assert.Throws<CreateException>(() => logic.CreateAlbumMedia(test));
        }

        [Test]
        public void CreateMediaTestMainSwitch()
        {
            Album foreign = new Album()
            {
                Id = 13
            };

            FakeRepository<AlbumMedia> repo = new FakeRepository<AlbumMedia>(new List<AlbumMedia>()
            {
                new AlbumMedia(){Id=1, Main=true},
                new AlbumMedia(){Id=2, Main=false},
                new AlbumMedia(){Id=3, Main=false},
            });

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out foreign)).Returns(true);
            mock.Setup((m) => m.ReadAll<AlbumMedia>()).Returns(repo.ReadAll());
            mock.Setup((m) => m.Update(It.IsAny<AlbumMedia>())).Callback<AlbumMedia>((am) => repo.Update(am));
            mock.Setup((m) => m.Create(It.IsAny<AlbumMedia>())).Callback<AlbumMedia>((am) => repo.Create(am));
            AlbumMedia test = new AlbumMedia()
            {
                Id = 10,
                AlbumId = 13,
                Uri = "https://www.google.hu/",
                Main = true
            };
            Assert.DoesNotThrow(() => logic.CreateAlbumMedia(test));
            Assert.That(!repo.ReadAll().Any(am => am != test && am.Main));
        }

        [Test]
        public void CreateMediaTestNull()
        {
            AlbumMedia test = null;
            Assert.Throws<ArgumentNullException>(() => logic.CreateAlbumMedia(test));
        }

        [Test]
        public void DeleteMediaTest()
        {
            AlbumMedia value = new AlbumMedia();
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out value)).Returns(true);
            mock.Setup((m) => m.Read<AlbumMedia>(It.IsAny<object[]>())).Returns(value);

            Assert.DoesNotThrow(() => logic.DeleteAlbumMedia(10));
        }

        [Test]
        public void DeleteMediaTestMainSwitch()
        {
            AlbumMedia test = new AlbumMedia()
            {
                Id = 1,
                Main = true
            };
            FakeRepository<AlbumMedia> repo = new FakeRepository<AlbumMedia>(new List<AlbumMedia>()
            {
                test,
                new AlbumMedia(){Id=2, Main=false},
                new AlbumMedia(){Id=3, Main=false},
            });

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(true);
            mock.Setup((m) => m.ReadAll<AlbumMedia>()).Returns(repo.ReadAll());
            mock.Setup((m) => m.Read<AlbumMedia>(It.IsAny<object[]>())).Returns<object[]>(i => repo.Read(i));
            mock.Setup((m) => m.Update(It.IsAny<AlbumMedia>())).Callback<AlbumMedia>((am) => repo.Update(am));
            mock.Setup((m) => m.Delete<AlbumMedia>(It.IsAny<object[]>())).Callback<object[]>((id) => repo.Delete(id));

            Assert.DoesNotThrow(() => logic.DeleteAlbumMedia(test.Id));
            Assert.That(repo.ReadAll().Count(m => m.Main) == 1);
        }

        [Test]
        public void DeleteMediaTestNonExistKey()
        {
            AlbumMedia test = null;
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(false);

            Assert.Throws<DeleteException>(() => logic.DeleteAlbumMedia(10));
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            mock = new Mock<IMusicRepository>();
            logic = new MediaLogic(mock.Object);
        }

        [Test]
        public void ReadMediaTest()
        {
            AlbumMedia test = new AlbumMedia();
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(true);
            Assert.DoesNotThrow(() => logic.ReadAlbumMedia(10));
        }

        [Test]
        public void ReadMediaTestNonExistKey()
        {
            AlbumMedia test = null;
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(false);
            Assert.Throws<ReadException>(() => logic.ReadAlbumMedia(30));
        }

        [SetUp]
        public void Setup()
        {
            mock.Reset();
        }

        [Test]
        public void UpdateMediaInvalidUri()
        {
            AlbumMedia test = new AlbumMedia()
            {
                Id = 10,
                Uri = "aacsc"
            };
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(true);
            Assert.Throws<UpdateException>(() => logic.UpdateAlbumMedia(test));
        }

        [Test]
        public void UpdateMediaNull()
        {
            AlbumMedia test = null;

            Assert.Throws<ArgumentNullException>(() => logic.UpdateAlbumMedia(test));
        }

        [Test]
        public void UpdateMediaTest()
        {
            Album foreign = new Album()
            {
                Id = 13
            };

            AlbumMedia test = new AlbumMedia()
            {
                AlbumId = 13,
                Uri = "https://www.google.hu/"
            };

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out foreign)).Returns(true);
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(true);

            Assert.DoesNotThrow(() => logic.CreateAlbumMedia(test));
        }

        [Test]
        public void UpdateMediaTestMainSwitch()
        {
            Album foreign = new Album()
            {
                Id = 13
            };

            AlbumMedia test1 = new AlbumMedia() { Id = 1, Main = false };
            AlbumMedia test2 = new AlbumMedia() { Id = 3, Main = true };

            FakeRepository<AlbumMedia> repo = new FakeRepository<AlbumMedia>(new List<AlbumMedia>()
            {
                new AlbumMedia(){Id=1, Main=true},
                new AlbumMedia(){Id=2, Main=false},
                new AlbumMedia(){Id=3, Main=false},
            });

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out foreign)).Returns(true);

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test1)).Returns(true);
            mock.Setup((m) => m.Read<AlbumMedia>(It.IsAny<object[]>())).Returns<object[]>((id) => repo.Read(id));
            mock.Setup((m) => m.ReadAll<AlbumMedia>()).Returns(repo.ReadAll());
            mock.Setup((m) => m.Update(It.IsAny<AlbumMedia>())).Callback<AlbumMedia>((am) => repo.Update(am));

            Assert.DoesNotThrow(() => logic.UpdateAlbumMedia(test1));
            Assert.That(repo.ReadAll().Any((m) => m.Id != test1.Id && m.Main));

            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test2)).Returns(true);
            Assert.DoesNotThrow(() => logic.UpdateAlbumMedia(test2));
            Assert.That(repo.ReadAll().Any((m) => m.Id == test2.Id && m.Main));
        }

        [Test]
        public void UpdateMediaTestNonExistForeignKey()
        {
            AlbumMedia test = new AlbumMedia()
            {
                Id = 12,
                Uri = "https://www.google.hu/",
                AlbumId = 330,
            };
            Album foreign = null;
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out test)).Returns(true);
            mock.Setup((m) => m.TryRead(It.IsAny<object[]>(), out foreign)).Returns(false);

            Assert.Throws<UpdateException>(() => logic.UpdateAlbumMedia(test));
        }
    }
}