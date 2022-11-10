using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class MediaLogic : BaseLogic, IMediaLogic
    {
        public MediaLogic(IMusicRepository repository) : base(repository)
        {
        }

        public void CreateAlbumMedia(AlbumMedia albumMedia)
        {
            CreateMedia<AlbumMedia, Album>(albumMedia, m => m.AlbumId);
        }

        public void CreateArtistMedia(ArtistMedia artistMedia)
        {
            CreateMedia<ArtistMedia, Artist>(artistMedia, m => m.ArtistId);
        }

        public void DeleteAlbumMedia(int albumMediaId)
        {
            DeleteMedia<AlbumMedia>(albumMediaId);
        }

        public void DeleteArtistMedia(int artistMediaId)
        {
            DeleteMedia<ArtistMedia>(artistMediaId);
        }

        public AlbumMedia ReadAlbumMedia(int albumMediaId)
        {
            return ReadEntityWithSimpleNumericKey<AlbumMedia>(albumMediaId);
        }

        public IEnumerable<AlbumMedia> ReadAllAlbumMedia()
        {
            return repository.ReadAll<AlbumMedia>();
        }

        public IEnumerable<ArtistMedia> ReadAllArtistMedia()
        {
            return repository.ReadAll<ArtistMedia>();
        }

        public ArtistMedia ReadArtistMedia(int artistMediaId)
        {
            return ReadEntityWithSimpleNumericKey<ArtistMedia>(artistMediaId);
        }

        public void UpdateAlbumMedia(AlbumMedia albumMedia)
        {
            UpdateMedia<AlbumMedia, Album>(albumMedia, m => m.AlbumId);
        }

        public void UpdateArtistMedia(ArtistMedia artistMedia)
        {
            UpdateMedia<ArtistMedia, Artist>(artistMedia, m => m.ArtistId);
        }

        private void CreateMedia<TMedia, TForeign>(TMedia media, Func<TMedia, int> foreignKeySelector)
           where TMedia : Media
           where TForeign : class, IEntity
        {
            CreateEntity(() =>
            {
                media.Id = 0;
                Validator<Media>.Validate(media.Uri);
                Validator<TForeign>.Validate(foreignKeySelector(media), "id");
                CheckKeyExists<TForeign>(foreignKeySelector(media));
                if (media.Main)
                {
                    TurnOffPreviousMain(media);
                }
            }, media);
        }

        private void DeleteMedia<TMedia>(int id)
         where TMedia : Media
        {
            DeleteEntity<TMedia>(() =>
            {
                TMedia media = repository.Read<TMedia>(id);
                if (media.Main)
                {
                    TurnOnNextMain(media);
                }
            }, id);
        }

        private void TurnOffPreviousMain<TMedia>(TMedia media)
           where TMedia : Media
        {
            var main = repository.ReadAll<TMedia>()
             .FirstOrDefault(m => m.Main && m.MediaType == media.MediaType);

            if (main != null)
            {
                main.Main = false;
                repository.Update(main);
            }
        }

        private void TurnOnNextMain<TMedia>(TMedia media)
                                                                                                                   where TMedia : Media
        {
            TMedia main = repository.ReadAll<TMedia>().FirstOrDefault(m => !m.Main && m.MediaType == media.MediaType);
            if (main != null)
            {
                main.Main = true;
                repository.Update(main);
            }
        }

        private void UpdateMedia<TMedia, TForeign>(TMedia media, Func<TMedia, int> foreignKeySelector)
           where TMedia : Media
           where TForeign : class, IEntity
        {
            UpdateEntity(() =>
            {
                Validator<Media>.Validate(media.Uri);
                Validator<TForeign>.Validate(foreignKeySelector(media), "id");
                CheckKeyExists<TForeign>(foreignKeySelector(media));
                if (media.Main)
                {
                    TurnOffPreviousMain(media);
                }
            }, media);
        }
    }
}