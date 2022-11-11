using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Utility;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class AlbumLogic : BaseLogic, IAlbumLogic
    {
        public AlbumLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public void CreateAlbum(Album album)
        {
            CreateEntity(() =>
            {
                Validator<Album>.Validate(album.Title);
                Validator<Album>.Validate(album.Year);
            }, album);
        }

        public void CreatePart(Part part)
        {
            if (part is null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            try
            {
                ValidatePartAtCreating(part, out Album album);
                part.Id = 0;

                repository.ChainActions<Part>()
                   .UpdateWithoutSave(album.Parts.Where(p => p.Position >= part.Position).OrderByDescending(t => t.Position), t => t.Position++)
                   .CreateWithoutSave(part)
                   .Save();
            }
            catch (Exception ex) { throw new CreateException(part, ex); }
        }

        public void CreateTrack(Track track)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            try
            {
                ValidateTrackAtCreating(track, out Part part);
                track.Id = 0;
                int lastPosition = part.Tracks.Count + 1;
                if (track.Position < 1 || lastPosition < track.Position)
                {
                    track.Position = lastPosition;
                }

                repository.ChainActions<Track>()
                .UpdateWithoutSave(part.Tracks.Where(t => t.Position >= track.Position).OrderByDescending(t => t.Position), t => t.Position++)
                .CreateWithoutSave(track)
                .Save();
            }
            catch (Exception ex)
            {
                throw new CreateException(track, ex);
            }
        }

        public void DeleteAlbum(int albumId)
        {
            DeleteEntityWithSimpleNumericKey<Album>(albumId);
        }

        public void DeletePart(int partId)
        {
            try
            {
                Validator<Part>.Validate(partId, nameof(Part.Id));
                Part part = ReadPart(partId);

                repository.ChainActions<Part>()
                .UpdateWithoutSave(part.Album.Parts.OrderBy(p => p.Position > part.Position).Where(p => p.Position > part.Position), (p) => p.Position--)
                .DeleteWithoutSave(part)
                .Save();
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Part), ex, partId);
            }
        }

        public void DeletePartByPosition(int albumId, int position)
        {
            try
            {
                Validator<Album>.Validate(albumId, nameof(Album.Id));
                Validator<Part>.Validate(position);
                DeletePart(ReadPartByPosition(albumId, position).Id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Part), ex);
            }
        }

        public void DeleteTrack(int trackId)
        {
            try
            {
                Validator<Track>.Validate(trackId, nameof(Track.Id));
                Track track = ReadTrack(trackId);

                repository.ChainActions<Track>()
                    .UpdateWithoutSave(track.Part.Tracks.OrderBy(t => t.Position)
                        .Where(t => t.Position > track.Position), t => t.Position--)
                    .DeleteWithoutSave(track)
                    .Save();
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Track), ex, trackId);
            }
        }

        public void DeleteTrackByPosition(int partId, int position)
        {
            try
            {
                Validator<Track>.Validate(position);
                DeleteTrack(ReadTrackByPosition(partId, position).Id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Part), ex);
            }
        }

        public void DeleteTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            try
            {
                Validator<Album>.Validate(albumId, nameof(Album.Id));
                Validator<Part>.Validate(partPosition, nameof(Part.Position));
                DeleteTrack(ReadTrackByPosition(albumId, partPosition, trackPosition).Id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Part), ex);
            }
        }

        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return repository.ReadAll<Album>()
                .GroupBy(a => a.Year)
                .Select(g => new AlbumPerYearDTO(g.Key, g.Count()));
        }

        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            return QueryRead(() =>
            {
                CheckKeyExists<Album>(albumId);
                return TimeSpan.FromMinutes(repository.Read<Album>(albumId)
                                           .Parts.SelectMany(p => p.Tracks)
                                           .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
            });
        }

        public TimeSpan GetTotalDurationOfPart(int partId)
        {
            return QueryRead(() =>
            {
                CheckKeyExists<Part>(partId);
                return TimeSpan.FromMinutes(repository.Read<Part>(partId)
                                             .Tracks.Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
            });
        }

        public Album ReadAlbum(int albumId)
        {
            return ReadEntityWithSimpleNumericKey<Album>(albumId);
        }

        public IEnumerable<Album> ReadAllAlbum()
        {
            return repository.ReadAll<Album>();
        }

        public IEnumerable<Part> ReadAllPart()
        {
            return repository.ReadAll<Part>();
        }

        public IEnumerable<Track> ReadAllTrack()
        {
            return repository.ReadAll<Track>();
        }

        public Part ReadPart(int partId)
        {
            return ReadEntityWithSimpleNumericKey<Part>(partId);
        }

        public Part ReadPartByPosition(int albumId, int position)
        {
            try
            {
                CheckKeyExists<Album>(albumId);
                Validator<Part>.Validate(position);
                Part part = ReadAlbum(albumId).Parts.FirstOrDefault(p => p.Position == position);
                if (part == null) throw new ArgumentException($" '{position}' position of part doesn't exist!");
                return part;
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Part), ex, albumId);
            }
        }

        public Track ReadTrack(int trackId)
        {
            return ReadEntityWithSimpleNumericKey<Track>(trackId);
        }

        public Track ReadTrackByPosition(int partId, int position)
        {
            try
            {
                CheckKeyExists<Part>(partId);
                Validator<Track>.Validate(position);
                Track track = repository.Read<Part>(partId).Tracks.FirstOrDefault(t => t.Position == position);
                if (track == null) throw new ArgumentException($"'{position}' position of track doesn't exist!");
                return track;
            }
            catch (Exception ex) { throw new ReadException(typeof(Track), ex, partId); }
        }

        public Track ReadTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            return ReadTrackByPosition(ReadPartByPosition(albumId, partPosition).Id, trackPosition);
        }

        public void UpdateAlbum(Album album)
        {
            UpdateEntity(() =>
            {
                Validator<Album>.Validate(album.Title);
                Validator<Album>.Validate(album.Year);
            }, album);
        }

        public void UpdatePart(Part part)
        {
            if (part is null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            try
            {
                ValidatePartAtUpdating(part, out Album album);
                Part old = ReadPart(part.Id);
                InsertTemplate(part, old, album, (a) => a.Parts, p => p.Position, (p, i) => p.Position = i);
            }
            catch (Exception ex)
            {
                throw new UpdateException(part, ex);
            }
        }

        public void UpdateTrack(Track track)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            try
            {
                ValidateTrackAtUpdating(track, out Part part);
                Validator<Track>.Validate(track.Id);
                Track old = ReadTrack(track.Id);
                InsertTemplate(track, old, part, (p) => p.Tracks, p => p.Position, (t, i) => t.Position = i);
            }
            catch (Exception ex)
            {
                throw new UpdateException(track, ex);
            }
        }

        private void InsertBackwardTemplate<TEntity, TParent>(
                                                   TEntity entity, TEntity old, TParent parent,
                                                   Func<TParent, IEnumerable<TEntity>> siblingSelector,
                                                   Func<TEntity, int> positionGet, Action<TEntity, int> positionSet)
           where TEntity : class, IEntity
           where TParent : class, IEntity
        {
            repository.ChainActions<TEntity>()
            .UpdateWithoutSave(siblingSelector(parent)
            .Where(e => positionGet(e) < positionGet(old) && positionGet(e) >= positionGet(entity) && positionGet(e) != positionGet(old))
            .OrderByDescending(e => positionGet), e => positionSet(e, positionGet(e) + 1))
            .UpdateWithoutSave(entity)
            .Save();
        }

        private void InsertForwardTemplate<TEntity, TParent>(
                                                    TEntity entity, TEntity old, TParent parent,
                                                    Func<TParent, IEnumerable<TEntity>> siblingSelector, Func<TEntity, int> GetPos, Action<TEntity, int> SetPos)
            where TEntity : class, IEntity
            where TParent : class, IEntity
        {
            repository.ChainActions<TEntity>()
            .UpdateWithoutSave(siblingSelector(parent)
            .Where(e => GetPos(e) > GetPos(old) && GetPos(e) <= GetPos(entity) && GetPos(e) != GetPos(old))
            .OrderBy(e => GetPos), e => SetPos(e, GetPos(e) - 1))
            .UpdateWithoutSave(entity)
            .Save();
        }

        private void InsertTemplate<TEntity, TParent>(
                                                    TEntity entity, TEntity old, TParent parent,
                                                    Func<TParent, IEnumerable<TEntity>> siblingSelector, Func<TEntity, int> positionGet, Action<TEntity, int> positionSet)
            where TEntity : class, IEntity
            where TParent : class, IEntity
        {
            if (positionGet(old) != positionGet(entity))
            {
                if (positionGet(old) < positionGet(entity))
                {
                    InsertForwardTemplate(entity, old, parent, siblingSelector, positionGet, positionSet);
                }
                else
                {
                    InsertBackwardTemplate(entity, old, parent, siblingSelector, positionGet, positionSet);
                }
            }
            else
            {
                repository.Update(entity);
            }
        }

        private void ValidatePartAtCreating(Part part, out Album album)
        {
            CheckKeyExists<Album>(part.AlbumId);
            album = repository.Read<Album>(part.AlbumId);
            if (string.IsNullOrWhiteSpace(part.Title)) part.Title = album.Title;
            Validator<Part>.Validate(part.Title);

            int lastPosition = album.Parts.Count + 1;
            if (part.Position < 1 || lastPosition < part.Position)
            {
                part.Position = lastPosition;
            }
        }

        private void ValidatePartAtUpdating(Part part, out Album album)
        {
            Validator<Part>.Validate(part.Title);
            CheckKeyExists<Album>(part.AlbumId);
            album = repository.Read<Album>(part.AlbumId);

            int lastPosition = album.Parts.Count;
            if (part.Position < 1 || lastPosition < part.Position)
            {
                part.Position = lastPosition;
            }
        }

        private void ValidateTrackAtCreating(Track track, out Part part)
        {
            Validator<Track>.Validate(track.Title);
            CheckKeyExists<Part>(track.PartId);
            part = repository.Read<Part>(track.PartId);
        }

        private void ValidateTrackAtUpdating(Track track, out Part part)
        {
            Validator<Track>.Validate(track.Title);
            CheckKeyExists<Part>(track.PartId);
            part = repository.Read<Part>(track.PartId);

            int lastPosition = part.Tracks.Count;
            if (track.Position < 1 || lastPosition < track.Position)
            {
                track.Position = lastPosition;
            }
        }
    }
}