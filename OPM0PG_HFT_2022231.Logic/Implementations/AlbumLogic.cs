using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Support;
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

        private void ValidateTrackAtCreating(Track track, out Part part)
        {
            Validator<Track>.Validate(track.PartId);
            Validator<Track>.Validate(track.Title);
            CheckKeyExists(repository.Parts, track.PartId);
            part = repository.Parts.Read(track.PartId);
        }

        private void ValidateTrackAtUpdating(Track track, out Part part)
        {
            Validator<Track>.Validate(track.PartId);
            Validator<Track>.Validate(track.Title);
            CheckKeyExists(repository.Parts, track.PartId);
            part = repository.Parts.Read(track.PartId);

            int lastPosition = part.Tracks.Count;
            if (track.Position < 1 || lastPosition < track.Position)
            {
                track.Position = lastPosition;
            }
        }

        private void ValidatePartAtCreating(Part part, out Album album)
        {
            Validator<Part>.Validate(part.AlbumId);
            CheckKeyExists(repository.Albums, part.AlbumId);
            album = repository.Albums.Read(part.AlbumId);
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
            Validator<Part>.Validate(part.Id);
            Validator<Part>.Validate(part.Title);
            CheckKeyExists(repository.Albums, part.AlbumId);
            album = repository.Albums.Read(part.AlbumId);

            int lastPosition = album.Parts.Count;
            if (part.Position < 1 || lastPosition < part.Position)
            {
                part.Position = lastPosition;
            }
        }

        public void CreateAlbum(Album album)
        {
            if (album is null)
            {
                throw new ArgumentNullException(nameof(album));
            }

            try
            {
                Validator<Album>.Validate(album.Title);
                Validator<Album>.Validate(album.Year);
                repository.Albums.Create(album);
            }
            catch (Exception ex)
            {
                throw new CreateException(album, ex);
            }
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

                repository.Tracks.ChainActions()
                .UpdateWithoutSave(part.Tracks.Where(t => t.Position >= track.Position).OrderByDescending(t => t.Position), t => t.Position++)
                .CreateWithoutSave(track)
                .Save();
            }
            catch (Exception ex)
            {
                throw new CreateException(track, ex);
            }
        }

        public IEnumerable<Album> ReadAllAlbum()
        {
            return repository.Albums.ReadAll();
        }

        public Album ReadAlbum(int albumId)
        {
            try
            {
                Validator<Album>.Validate(albumId, nameof(Album.Id));
                CheckKeyExists(repository.Albums, albumId);
                return repository.Albums.Read(albumId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Album), ex, albumId);
            }
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

                repository.Parts.ChainActions()
                   .UpdateWithoutSave(album.Parts.Where(p => p.Position >= part.Position).OrderByDescending(t => t.Position), t => t.Position++)
                   .CreateWithoutSave(part)
                   .Save();
            }
            catch (Exception ex) { throw new CreateException(part, ex); }
        }

        public IEnumerable<Part> ReadAllPart()
        {
            return repository.Parts.ReadAll();
        }

        public Part ReadPart(int partId)
        {
            try
            {
                Validator<Part>.Validate(partId, nameof(Part.Id));
                CheckKeyExists(repository.Parts, partId);
                return repository.Parts.Read(partId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Part), ex, partId);
            }
        }

        public Part ReadPartByPosition(int albumId, int position)
        {
            try
            {
                Validator<Part>.Validate(albumId);
                Validator<Part>.Validate(position);
                CheckKeyExists(repository.Albums, albumId);
                Part part = ReadAlbum(albumId).Parts.FirstOrDefault(p => p.Position == position);
                if (part == null) throw new ArgumentException($" '{position}' position of part doesn't exist!");
                return part;
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Part), ex, albumId);
            }
        }

        public Track ReadTrackByPosition(int partId, int position)
        {
            try
            {
                Validator<Track>.Validate(partId);
                Validator<Track>.Validate(position);
                CheckKeyExists(repository.Parts, partId);
                Track track = repository.Parts.Read(partId).Tracks.FirstOrDefault(t => t.Position == position);
                if (track == null) throw new ArgumentException($"'{position}' position of track doesn't exist!");
                return track;
            }
            catch (Exception ex) { throw new ReadException(typeof(Track), ex, partId); }
        }

        public Track ReadTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            return ReadTrackByPosition(ReadPartByPosition(albumId, partPosition).Id, trackPosition);
        }

        public IEnumerable<Track> ReadAllTrack()
        {
            return repository.Tracks.ReadAll();
        }

        public Track ReadTrack(int trackId)
        {
            try
            {
                Validator<Track>.Validate(trackId, nameof(Track.Id));
                CheckKeyExists(repository.Tracks, trackId);
                return repository.Tracks.Read(trackId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Track), ex, trackId);
            }
        }

        public void UpdateAlbum(Album album)
        {
            if (album is null)
            {
                throw new ArgumentNullException(nameof(album));
            }

            try
            {
                Validator<Album>.Validate(album.Id);
                Validator<Album>.Validate(album.Title);
                Validator<Album>.Validate(album.Year);
                CheckKeyExists(repository.Albums, album.Id);
                repository.Albums.Update(album);
            }
            catch (Exception ex)
            {
                throw new UpdateException(album, ex);
            }
        }

        private void InsertTemplate<TEntity, TParent>(IRepository<TEntity> repository,
                                                    TEntity entity, TEntity old, TParent parent,
                                                    Func<TParent, IEnumerable<TEntity>> siblingSelector, Func<TEntity, int> positionGet, Action<TEntity, int> positionSet)
            where TEntity : class, IEntity
            where TParent : class, IEntity
        {
            if (positionGet(old) != positionGet(entity))
            {
                if (positionGet(old) < positionGet(entity))
                {
                    InsertForwardTemplate(repository, entity, old, parent, siblingSelector, positionGet, positionSet);
                }
                else
                {
                    InsertBackwardTemplate(repository, entity, old, parent, siblingSelector, positionGet, positionSet);
                }
            }
            else
            {
                repository.Update(entity);
            }
        }

        private void InsertForwardTemplate<TEntity, TParent>(IRepository<TEntity> repository,
                                                    TEntity entity, TEntity old, TParent parent,
                                                    Func<TParent, IEnumerable<TEntity>> siblingSelector, Func<TEntity, int> GetPos, Action<TEntity, int> SetPos)
            where TEntity : class, IEntity
            where TParent : class, IEntity
        {
            repository.ChainActions()
                .UpdateWithoutSave(siblingSelector(parent)
                .Where(e => GetPos(e) > GetPos(old) && GetPos(e) <= GetPos(entity) && GetPos(e) != GetPos(old))
                .OrderBy(e => GetPos), e => SetPos(e, GetPos(e) - 1))
                .UpdateWithoutSave(entity)
                .Save();
        }

        private void InsertBackwardTemplate<TEntity, TParent>(IRepository<TEntity> repository,
                                                   TEntity entity, TEntity old, TParent parent,
                                                   Func<TParent, IEnumerable<TEntity>> siblingSelector, Func<TEntity, int> positionGet, Action<TEntity, int> positionSet)
           where TEntity : class, IEntity
           where TParent : class, IEntity
        {
            repository.ChainActions()
                .UpdateWithoutSave(siblingSelector(parent)
                .Where(e => positionGet(e) < positionGet(old) && positionGet(e) >= positionGet(entity) && positionGet(e) != positionGet(old))
                .OrderByDescending(e => positionGet), e => positionSet(e, positionGet(e) + 1))
                .UpdateWithoutSave(entity)
                .Save();
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
                InsertTemplate(repository.Parts, part, old, album, (a) => a.Parts, p => p.Position, (p, i) => p.Position = i);
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
                InsertTemplate(repository.Tracks, track, old, part, (p) => p.Tracks, p => p.Position, (t, i) => t.Position = i);
            }
            catch (Exception ex)
            {
                throw new UpdateException(track, ex);
            }
        }

        public void DeleteAlbum(int albumId)
        {
            try
            {
                Validator<Album>.Validate(albumId, nameof(Album.Id));
                CheckKeyExists(repository.Albums, albumId);
                repository.Albums.Delete(albumId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Album), ex, albumId);
            }
        }

        public void DeletePart(int partId)
        {
            try
            {
                Validator<Part>.Validate(partId, nameof(Part.Id));
                Part part = ReadPart(partId);

                repository.Parts.ChainActions()
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

                repository.Tracks.ChainActions()
                    .UpdateWithoutSave(track.Part.Tracks.OrderBy(t => t.Position).Where(t => t.Position > track.Position), t => t.Position--)
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
                Validator<Part>.Validate(partId, nameof(Part.Id));
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

        public TimeSpan GetTotalDurationOfPart(int partId)
        {
            try
            {
                Validator<Part>.Validate(partId, nameof(Part.Id));
                CheckKeyExists(repository.Parts, partId, nameof(Part.Id));
                return TimeSpan.FromMinutes(repository.Parts.Read(partId)
                                           .Tracks.Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Part), ex, partId);
            }
        }

        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            try
            {
                Validator<Album>.Validate(albumId, nameof(Album.Id));
                CheckKeyExists(repository.Albums, albumId, nameof(Album.Id));
                return TimeSpan.FromMinutes(repository.Albums.Read(albumId)
                                           .Parts.SelectMany(p => p.Tracks)
                                           .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Album), ex, albumId);
            }
        }

        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return repository.Albums.ReadAll()
                .GroupBy(a => a.Year)
                .Select(g => new AlbumPerYearDTO(g.Key, g.Count()));
        }
    }
}