using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class FakeMusicRepository : IMusicRepository
    {
        private Dictionary<Type, object> repositories;

        public FakeMusicRepository()
        {
            repositories = new Dictionary<Type, object>();
            SetRepository(new FakeRepository<Album>(ReadTestSeed<Album>()));
            SetRepository(new FakeRepository<AlbumGenre>(ReadTestSeed<AlbumGenre>()));
            SetRepository(new FakeRepository<Artist>(ReadTestSeed<Artist>()));
            SetRepository(new FakeRepository<Contribution>(ReadTestSeed<Contribution>()));
            SetRepository(new FakeRepository<Membership>(ReadTestSeed<Membership>()));
            SetRepository(new FakeRepository<Part>(ReadTestSeed<Part>()));
            SetRepository(new FakeRepository<Release>(ReadTestSeed<Release>()));
            SetRepository(new FakeRepository<Track>(ReadTestSeed<Track>()));
            SetRepository(new FakeRepository<AlbumMedia>(ReadTestSeed<AlbumMedia>()));
            SetRepository(new FakeRepository<ArtistMedia>(ReadTestSeed<ArtistMedia>()));
            RefreshAllNavigationProperties();
        }

        public void Create<TEntity>(TEntity entity)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Create(entity);
        }

        public void Delete<TEntity>(params object[] id)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Delete(id);
        }

        public TEntity Read<TEntity>(params object[] id)
           where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().Read(id);
        }

        public IQueryable<TEntity> ReadAll<TEntity>()
        where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().ReadAll();
        }

        public void RefreshAlbumGenreNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<AlbumGenre, Album>
            ((g) => GetKey(g.AlbumId), (g, a) => g.Album = a, (g) => g.Genres);
        }

        public void RefreshAlbumMediaNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<AlbumMedia, Album>
            (m => GetKey(m.AlbumId), (m, a) => m.Album = a, (a) => a.Media);
        }

        public void RefreshAllNavigationProperties()
        {
            RefreshAlbumGenreNavigationProperties();
            RefreshContributionNavigationProperties();
            RefreshMembershipNavigationProperties();
            RefreshPartNavigationProperties();
            RefreshReleaseNavigationProperties();
            RefreshTrackNavigationProperties();
            RefreshAlbumMediaNavigationProperties();
            RefreshArtistMediaNavigationProperties();
        }

        public void RefreshArtistMediaNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<ArtistMedia, Artist>
            (m => GetKey(m.ArtistId), (m, a) => m.Artist = a, (a) => a.Media);
        }

        public void RefreshContributionNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<Contribution, Artist>
            ((c) => GetKey(c.ArtistId), (c, a) => c.Artist = a, (a) => a.ContributedAlbums);

            RefreshNavigationPropertiesTemplate<Contribution, Album>
            ((c) => GetKey(c.AlbumId), (c, a) => c.Album = a, (a) => a.Contributions);
        }

        public void RefreshMembershipNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<Membership, Artist>
            ((m) => GetKey(m.BandId), (m, b) => m.Band = b, (b) => b.Members);

            RefreshNavigationPropertiesTemplate<Membership, Artist>
            ((m) => GetKey(m.MemberId), (m, b) => m.Member = b, (b) => b.Bands);
        }

        public void RefreshPartNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<Part, Album>
            ((p) => GetKey(p.AlbumId), (p, a) => p.Album = a, (a) => a.Parts);
        }

        public void RefreshReleaseNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<Release, Album>
            ((r) => GetKey(r.AlbumId), (r, a) => r.Album = a, (r) => r.Releases);
        }

        public void RefreshTrackNavigationProperties()
        {
            RefreshNavigationPropertiesTemplate<Track, Part>
            ((t) => GetKey(t.PartId), (t, p) => t.Part = p, (p) => p.Tracks);
        }

        public void Reset()
        {
            ResetRepository<Album>();
            ResetRepository<Artist>();
            ResetRepository<Contribution>();
            ResetRepository<AlbumGenre>();
            ResetRepository<Membership>();
            ResetRepository<Part>();
            ResetRepository<Release>();
            ResetRepository<Track>();
            ResetRepository<ArtistMedia>();
            ResetRepository<AlbumMedia>();

            RefreshAllNavigationProperties();
        }

        public void ResetRepository<TEntity>()
                    where TEntity : class, IEntity
        {
            ((FakeRepository<TEntity>)GetRepository<TEntity>()).Reset();
        }

        public bool TryRead<TEntity>(object[] id, out TEntity entity)
           where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().TryRead(id, out entity);
        }

        public void Update<TEntity>(TEntity entity)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Update(entity);
        }

        IRepositoryChainActions<TEntity> IMusicRepository.ChainActions<TEntity>()
        {
            return GetRepository<TEntity>().ChainActions();
        }

        private object[] GetKey(params object[] props)
        {
            return props;
        }

        private IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntity
        {
            return (IRepository<TEntity>)repositories[typeof(TEntity)];
        }

        private IEnumerable<TEntity> ReadTestSeed<TEntity>()
                where TEntity : class, IEntity
        {
            return new XmlSerializer<List<TEntity>>().Deserialize($"FakeRepository/FakeSeeds/Fake{typeof(TEntity).Name}Seed.xml");
        }

        private void RefreshNavigationPropertiesTemplate<TChild, TParent>
            (Func<TChild, object[]> parentKeySelector,
            Action<TChild, TParent> parentSetter,
            Func<TParent, ICollection<TChild>> childrenSelector)
            where TChild : class, IEntity
            where TParent : class, IEntity
        {
            foreach (var child in ReadAll<TChild>())
            {
                var parent = Read<TParent>(parentKeySelector(child));
                childrenSelector(parent).Clear();
            }

            foreach (var child in ReadAll<TChild>())
            {
                var parent = Read<TParent>(parentKeySelector(child));
                parentSetter(child, parent);

                var children = childrenSelector(parent);
                if (!children.Contains(child))
                {
                    children.Add(child);
                }
            }
        }

        private void SetRepository<TEntity>(IRepository<TEntity> repository)
                                                                                                                                                                                        where TEntity : class, IEntity
        {
            repositories[typeof(TEntity)] = repository;
        }
    }
}