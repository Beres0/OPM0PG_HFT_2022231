using OPM0PG_HFT_2022231.Logic.Internals;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
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

        private void ValidateAlbum(Album album)
        {
            if (album is null)
            {
                throw new ArgumentNullException(nameof(album));
            }
            Validator.ValidateRequiredText(album.Title);
            Validator.ValidateYear(album.Year);
        }

        private void ValidateTrackAtCreating(Track track, out Part part)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }

            Validator.ValidatePositiveNumber(track.PartId);
            Validator.ValidateRequiredText(track.Title);

            part = repository.Parts.Read(track.PartId);

            int lastPosition = part.Tracks.Count + 1;
            if (track.Position < 1 || lastPosition < track.Position)
            {
                part.Position = lastPosition;
            }
        }

        private void ValidateTrackAtUpdating(Track track, out Part part)
        {
            if (track is null)
            {
                throw new ArgumentNullException(nameof(track));
            }
            Validator.ValidatePositiveNumber(track.PartId);
            Validator.ValidateRequiredText(track.Title);
            part = repository.Parts.Read(track.PartId);

            int lastPosition = part.Tracks.Count;
            if (track.Position < 1 || lastPosition < track.Position)
            {
                part.Position = lastPosition;
            }
        }

        private void ValidatePartAtCreating(Part part, out Album album)
        {
            if (part is null)
            {
                throw new ArgumentNullException(nameof(part));
            }
            Validator.ValidatePositiveNumber(part.AlbumId);

            album = repository.Albums.Read(part.AlbumId);
            if (string.IsNullOrWhiteSpace(part.Title)) part.Title = album.Title;
            Validator.ValidateRequiredText(part.Title);

            int lastPosition = album.Parts.Count + 1;
            if (part.Position < 1 || lastPosition < part.Position)
            {
                part.Position = lastPosition;
            }
        }

        private void ValidatePartAtUpdating(Part part, out Album album)
        {
            if (part is null)
            {
                throw new ArgumentNullException(nameof(part));
            }
            Validator.ValidatePositiveNumber(part.Id);
            Validator.ValidateRequiredText(part.Title);

            album = repository.Albums.Read(part.AlbumId);

            int lastPosition = album.Parts.Count;
            if (part.Position < 1 || lastPosition < part.Position)
            {
                part.Position = lastPosition;
            }
        }

        public void CreateAlbum(Album album)
        {
            ValidateAlbum(album);
            album.Id = 0;
            repository.Albums.Create(album);
        }

        public void CreatePart(Part part)
        {
            part.Id = 0;
            ValidatePartAtCreating(part, out Album album);

            repository.Parts.ChainActions()
               .UpdateWithoutSave(album.Parts.Where(p => p.Position >= part.Position).OrderByDescending(t => t.Position), t => t.Position++)
               .CreateWithoutSave(part)
               .Save();
        }

        public void CreateTrack(Track track)
        {
            track.Id = 0;
            ValidateTrackAtCreating(track, out Part part);

            repository.Tracks.ChainActions()
                .UpdateWithoutSave(part.Tracks.Where(t => t.Position >= track.Position).OrderByDescending(t => t.Position), t => t.Position++)
                .CreateWithoutSave(track)
                .Save();
        }

        public IEnumerable<Album> ReadAllAlbum()
        {
            return repository.Albums.ReadAll();
        }

        public Album ReadAlbum(int albumId)
        {
            Validator.ValidatePositiveNumber(albumId);
            return repository.Albums.Read(albumId);
        }

        public IEnumerable<Part> ReadAllPart()
        {
            return repository.Parts.ReadAll();
        }

        public Part ReadPart(int partId)
        {
            Validator.ValidatePositiveNumber(partId);
            return repository.Parts.Read(partId);
        }

        public Part ReadPartByPosition(int albumId, int position)
        {
            Validator.ValidatePositiveNumber(albumId);
            Validator.ValidatePositiveNumber(position);
            Part part = ReadAlbum(albumId).Parts.FirstOrDefault(p => p.Position == position);
            if (part == null) throw new ArgumentException($" '{position}' position of part doesn't exist!");
            return part;
        }

        public Track ReadTrackByPosition(int partId, int position)
        {
            Validator.ValidatePositiveNumber(partId);
            Validator.ValidatePositiveNumber(position);
            Track track = ReadPart(partId).Tracks.FirstOrDefault(t => t.Position == position);
            if (track == null) throw new ArgumentException($"'{position}' position of track doesn't exist!");
            return track;
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
            Validator.ValidatePositiveNumber(trackId);

            return repository.Tracks.Read(trackId);
        }

        public void UpdateAlbum(Album album)
        {
            ValidateAlbum(album);
            repository.Albums.Update(album);
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
            ValidatePartAtUpdating(part, out Album album);
            Part old = repository.Parts.Read(part.Id);
            InsertTemplate(repository.Parts, part, old, album, (a) => a.Parts, p => p.Position, (p, i) => p.Position = i);
        }

        public void UpdateTrack(Track track)
        {
            ValidateTrackAtUpdating(track, out Part part);
            Validator.ValidatePositiveNumber(track.Id);
            Track old = repository.Tracks.Read(track.Id);
            InsertTemplate(repository.Tracks, track, old, part, (p) => p.Tracks, p => p.Position, (t, i) => t.Position = i);
        }

        public void DeleteAlbum(int albumid)
        {
            Validator.ValidatePositiveNumber(albumid);
            repository.Albums.Delete(albumid);
        }

        public void DeletePart(int partId)
        {
            Validator.ValidatePositiveNumber(partId);
            Part part = repository.Parts.Read(partId);

            repository.Parts.ChainActions()
                .UpdateWithoutSave(part.Album.Parts.OrderBy(p => p.Position), (p) => p.Position--)
                .DeleteWithoutSave(part)
                .Save();
        }

        public void DeletePartByPosition(int albumId, int positionId)
        {
            DeletePart(ReadPartByPosition(albumId, positionId).Id);
        }

        public void DeleteTrack(int trackId)
        {
            Validator.ValidatePositiveNumber(trackId);
            Track track = repository.Tracks.Read(trackId);

            repository.Tracks.ChainActions()
                .UpdateWithoutSave(track.Part.Tracks.OrderBy(t => t.Position), t => t.Position--)
                .DeleteWithoutSave(track)
                .Save();
        }

        public void DeleteTrackByPosition(int partId, int position)
        {
            DeleteTrack(ReadTrackByPosition(partId, position).Id);
        }

        public void DeleteTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            DeleteTrack(ReadTrackByPosition(albumId, partPosition, trackPosition).Id);
        }

        public TimeSpan GetTotalDurationOfPart(int partId)
        {
            Validator.ValidatePositiveNumber(partId);
            return TimeSpan.FromMinutes(repository.Parts.Read(partId)
                                       .Tracks.Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }

        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            Validator.ValidatePositiveNumber(albumId);
            return TimeSpan.FromMinutes(repository.Albums.Read(albumId)
                                       .Parts.SelectMany(p => p.Tracks)
                                       .Sum(t => t.Duration.HasValue ? t.Duration.Value.Minutes : 0));
        }

        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return repository.Albums.ReadAll()
                .GroupBy(a => a.Year)
                .Select(g => new AlbumPerYearDTO(g.Key, g.Count()));
        }
    }
}