﻿using OPM0PG_HFT_2022231.Logic.Internals;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class MusicLogic : IMusicLogic
    {
        private IAlbumLogic album;
        private IArtistLogic artist;
        private IGenreLogic genre;
        private IContributionLogic contribution;
        private IReleaseLogic release;

        public MusicLogic(IAlbumLogic album, IArtistLogic artist, IGenreLogic genre, IContributionLogic contribution, IReleaseLogic release)
        {
            this.album = album;
            this.artist = artist;
            this.genre = genre;
            this.contribution = contribution;
            this.release = release;
        }

        public AlbumSummaryDTO GetAlbumSummary(int albumId)
        {
            Validator.ValidatePositiveNumber(albumId);

            Album al = album.ReadAlbum(albumId);

            var parts = al.Parts.OrderBy(p => p.Position).Select(p =>
                    new PartDTO(p.Id, p.Position, p.Title, album.GetTotalDurationOfPart(p.Id),
                        p.Tracks.OrderBy(p => p.Position).Select(t => new TrackDTO(t.Id, t.Position, t.Title, t.Duration))));

            var genres = genre.ReadAllAlbumGenre()
                         .Where(g => g.AlbumId == albumId)
                         .Select(ag => ag.Genre);
            var releases = release.ReadAllRelease()
                           .Where(r => r.AlbumId == al.Id)
                           .Select(r => new ReleaseDTO(r.Id, r.Publisher, r.Country, r.ReleaseYear));
            var contributors = contribution.ReadAllContributions()
                              .Where(c => c.AlbumId == al.Id)
                              .Select(c => new ContributorDTO(c.ArtistId, c.Artist.Name));

            return new AlbumSummaryDTO(al.Id, al.Title, al.Year, parts, genres, releases, contributors);
        }

        public ArtistSummaryDTO GetArtistSummary(int artistId)
        {
            Validator.ValidatePositiveNumber(artistId);
            var art = artist.ReadArtist(artistId);
            var albums = contribution.ReadAllContributions()
                         .Where(c => artistId == c.ArtistId)
                         .Select(a => new AlbumDTO
                         (a.AlbumId, a.Album.Title, a.Album.Year,
                         album.GetTotalDurationOfAlbum(a.AlbumId),
                         album.ReadAllTrack().Where(t => t.Part.AlbumId == a.AlbumId).Count()));

            var bands = artist.ReadAllMembership().Where(m => m.MemberId == art.Id)
                    .Select(m => new BandsDTO(m.BandId, m.Band.Name, m.Active));

            var genres = genre.ReadAllArtistGenre()
                           .Where(g => g.Artist.Id == artistId).Select(g => g.Genre);

            var members = artist.ReadAllMembership()
                              .Where(m => m.BandId == artistId)
                              .Select(m => new MemberDTO(m.MemberId, m.Member.Name, m.Active));

            return new ArtistSummaryDTO(artistId, art.Name, genres, members, bands, albums);
        }

        public StatisticsDTO GetStatistics()
        {
            int numArtists = artist.ReadAllArtist().Count();
            int numGenres = genre.GetGenres().Count();
            int numAlbums = album.ReadAllAlbum().Count();
            int numTracks = album.ReadAllTrack().Count();
            int numReleases = release.ReadAllRelease().Count();
            int numPublishers = release.GetPublishers().Count();
            return new StatisticsDTO(numArtists, numGenres, numAlbums, numTracks, numReleases, numPublishers);
        }
    }
}