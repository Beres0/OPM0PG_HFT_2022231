using NUnit.Framework;
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
    internal class ReleaseLogicTest
    {
        private ReleaseLogic logic;
        private FakeMusicRepository repository;

        [Test]
        public void CreateReleaseTest()
        {
            int negAlbumId = -1;
            int nonExistAlbumId = 21515123;
            int okAlbumId = 796235;
            string countryLong = new string('x', 256);
            string okCountry = "Test";
            string publisherLong = new string('x', 256);
            string okPublisher = "Test";
            int year1 = 1321;
            int year2 = 2333;
            int okYear = 1950;

            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = negAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = nonExistAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = okAlbumId,
                Country = countryLong,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = publisherLong,
                ReleaseYear = okYear
            }));
            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = year1
            }));
            Assert.Throws<CreateException>(() => logic.CreateRelease(new Release()
            {
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = year2
            }));
            var okRelease = new Release()
            {
                Id = 1000,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            };
            Assert.DoesNotThrow(() => logic.CreateRelease(okRelease));
            Assert.That(repository.Releases.TryRead(new object[] { okRelease.Id }, out var result));
        }

        [Test]
        public void DeleteReleaseTest()
        {
            int negReleaseId = -1;
            int nonExistReleaseId = 124214124;
            int okReleaseId = 10725053;

            Assert.Throws<DeleteException>(() => logic.DeleteRelease(negReleaseId));
            Assert.Throws<DeleteException>(() => logic.DeleteRelease(nonExistReleaseId));

            Assert.DoesNotThrow(() => logic.DeleteRelease(okReleaseId));
            Assert.That(!repository.Releases.TryRead(new object[] { okReleaseId }, out var result));
        }

        [Test]
        public void GetCountyStatistics()
        {
            Assert.That(repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()))
                .Join(repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .GroupBy(r => r.Country)
                .Select(g => new ReleasePerCountryDTO(g.Key, g.Count())),
                (p) => p.Country, (r) => r.Country,
                (p, r) => new CountryStatDTO(p.Country, p.NumberOfPublishers, r.NumberOfReleases))
                .SequenceEqual(logic.GetCountryStatistics()));
        }

        [Test]
        public void GetPublisherPerCountry()
        {
            Assert.That(repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()))
                .SequenceEqual(logic.GetPublisherPerCountry()));
        }

        [Test]
        public void GetPublishersTest()
        {
            Assert.That(repository.Releases.ReadAll()
                .Select(r => r.Publisher)
                .Distinct().SequenceEqual(logic.GetPublishers()));
        }

        [Test]
        public void GetReleasePerCountry()
        {
            Assert.That(repository.Releases.ReadAll()
                .Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .GroupBy(r => r.Country)
                .Select(g => new ReleasePerCountryDTO(g.Key, g.Count()))
                .SequenceEqual(logic.GetReleasePerCountry()));
        }

        [Test]
        public void GetReleasePerYear()
        {
            Assert.That(repository.Releases.ReadAll().Where(r => r.ReleaseYear.HasValue)
                .GroupBy(r => r.ReleaseYear)
                .Select(g => new ReleasePerYearDTO(g.Key.Value, g.Count()))
                .SequenceEqual(logic.GetReleasePerYear()));
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            repository = new FakeMusicRepository();
            logic = new ReleaseLogic(repository);
        }

        [Test]
        public void ReadReleaseTest()
        {
            int negId = -1;
            int nonExistId = 1321131;

            Assert.Throws<ReadException>(() => logic.ReadRelease(negId));
            Assert.Throws<ReadException>(() => logic.ReadRelease(nonExistId));

            Assert.DoesNotThrow(() => logic.ReadRelease(10303083));
        }

        [SetUp]
        public void Setup()
        {
            repository.Reset();
        }

        [Test]
        public void UpdateReleaseTest()
        {
            int negReleaseId = -1;
            int nonExistReleaseId = 124214124;
            int okReleaseId = 10725053;
            int negAlbumId = -1;
            int nonExistAlbumId = 21515123;
            int okAlbumId = 796235;
            string countryLong = new string('x', 256);
            string okCountry = "Test";
            string publisherLong = new string('x', 256);
            string okPublisher = "Test";
            int year1 = 1321;
            int year2 = 2333;
            int okYear = 1950;

            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = negReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = nonExistReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = negAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = nonExistAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = okAlbumId,
                Country = countryLong,
                Publisher = okPublisher,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = publisherLong,
                ReleaseYear = okYear
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = year1
            }));
            Assert.Throws<UpdateException>(() => logic.UpdateRelease(new Release()
            {
                Id = okReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = year2
            }));

            var okRelease = new Release()
            {
                Id = okReleaseId,
                AlbumId = okAlbumId,
                Country = okCountry,
                Publisher = okPublisher,
                ReleaseYear = okYear
            };
            Assert.DoesNotThrow(() => logic.UpdateRelease(okRelease));
            Assert.That(repository.Releases.Read(okRelease.Id).Country == "Test");
        }
    }
}