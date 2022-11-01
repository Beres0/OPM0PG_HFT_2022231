using OPM0PG_HFT_2022231.Models;

using OPM0PG_HFT_2022231.Repository;
using OPM0PG_HFT_2022231.Repository.Xml;
using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class FakeMusicRepository : IMusicRepository
    {
        public IRepository<Album> Albums { get; }
        public IRepository<Artist> Artists { get; }
        public IRepository<Contribution> Contributions { get; }
        public IRepository<AlbumGenre> Genres { get; }
        public IRepository<Membership> Memberships { get; }
        public IRepository<Part> Parts { get; }
        public IRepository<Release> Releases { get; }
        public IRepository<Track> Tracks { get; }

        public FakeMusicRepository()
        {
            Albums = new FakeRepository<Album>(ReadTestSeed<Album>());
            Artists = new FakeRepository<Artist>(ReadTestSeed<Artist>());
            Contributions = new FakeRepository<Contribution>(ReadTestSeed<Contribution>());
            Genres = new FakeRepository<AlbumGenre>(ReadTestSeed<AlbumGenre>());
            Memberships = new FakeRepository<Membership>(ReadTestSeed<Membership>());
            Parts = new FakeRepository<Part>(ReadTestSeed<Part>());
            Releases = new FakeRepository<Release>(ReadTestSeed<Release>());
            Tracks = new FakeRepository<Track>(ReadTestSeed<Track>());
            RefreshAllNavigationProperties();
        }

        private IEnumerable<TEntity> ReadTestSeed<TEntity>()
            where TEntity : class, IEntity
        {
            return new XmlSerializer<List<TEntity>>().Deserialize($"FakeRepository/FakeSeeds/Fake{typeof(TEntity).Name}Seed.xml");
        }

        private void ResetRepository<TEntity>(IRepository<TEntity> repository)
            where TEntity : class, IEntity
        {
            ((FakeRepository<TEntity>)repository).Reset();
        }

        public void Reset()
        {
            ResetRepository(Albums);
            ResetRepository(Artists);
            ResetRepository(Contributions);
            ResetRepository(Genres);
            ResetRepository(Memberships);
            ResetRepository(Parts);
            ResetRepository(Releases);
            ResetRepository(Tracks);

            RefreshAllNavigationProperties();
        }

        private void RefreshNavigationPropertiesTemplate<TChild, TParent>
            (IRepository<TChild> childRepository,
            Func<TChild, object[]> parentKeySelector,
            Action<TChild, TParent> parentSetter,
            Func<TParent, ICollection<TChild>> childrenSelector,
            IRepository<TParent> parentRepository)
            where TChild : class, IEntity
            where TParent : class, IEntity
        {
            foreach (var child in childRepository.ReadAll())
            {
                var parent = parentRepository.Read(parentKeySelector(child));
                childrenSelector(parent).Clear();
            }

            foreach (var child in childRepository.ReadAll())
            {
                var parent = parentRepository.Read(parentKeySelector(child));
                parentSetter(child, parent);

                var children = childrenSelector(parent);
                if (!children.Contains(child))
                {
                    children.Add(child);
                }
            }
        }

        private object[] GetKey(params object[] props)
        {
            return props;
        }

        public void RefreshTrackNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Tracks,
            (t) => GetKey(t.PartId),
            (t, p) => t.Part = p,
            (p) => p.Tracks,
            Parts);
        }

        public void RefreshPartNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Parts,
            (p) => GetKey(p.AlbumId),
            (p, a) => p.Album = a,
            (a) => a.Parts,
            Albums);
        }

        public void RefreshMembershipNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Memberships,
            (m) => GetKey(m.BandId),
            (m, b) => m.Band = b,
            (b) => b.Members,
            Artists);

            RefreshNavigationPropertiesTemplate
            (Memberships,
            (m) => GetKey(m.MemberId),
            (m, b) => m.Member = b,
            (b) => b.Bands,
            Artists);
        }

        public void RefreshContributionNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Contributions,
            (c) => GetKey(c.ArtistId),
            (c, a) => c.Artist = a,
            (a) => a.ContributedAlbums,
            Artists);

            RefreshNavigationPropertiesTemplate
            (Contributions,
            (c) => GetKey(c.AlbumId),
            (c, a) => c.Album = a,
            (a) => a.Contributions,
            Albums);
        }

        public void RefreshAlbumGenreNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Genres,
            (g) => GetKey(g.AlbumId),
            (g, a) => g.Album = a,
            (g) => g.Genres,
            Albums);
        }

        public void RefreshReleaseNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate
            (Releases,
            (r) => GetKey(r.AlbumId),
            (r, a) => r.Album = a,
            (r) => r.Releases,
            Albums);
        }

        public void RefreshAllNavigationProperties()
        {
            RefreshAlbumGenreNavigationProperties();
            RefreshContributionNavigationProperties();
            RefreshMembershipNavigationProperties();
            RefreshPartNavigationProperties();
            RefreshReleaseNavigationProperties();
            RefreshTrackNavigationProperties();
        }
    }
}