using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Models.DataTransferObjects
{
    public record AlbumSummaryDTO(int Id, string Title, int Year,
                        IEnumerable<PartDTO> Parts,
                        IEnumerable<string> Genres,
                        IEnumerable<ReleaseDTO> Releases,
                        IEnumerable<ContributorDTO> Contributors,
                        IEnumerable<MediaDTO> Media);

    public record MediaDTO(int Id, MediaType MediaType, bool Main, string Uri);
    public record ContributorDTO(int Id, string Name);
    public record PartDTO(int Id, int Position, string Title, TimeSpan Duration, IEnumerable<TrackDTO> Tracks);
    public record TrackDTO(int Id, int Position, string Title, TimeSpan? Duration);
    public record ReleaseDTO(int Id, string Publisher, string Country, int? ReleaseYear);
    public record AlbumPerGenreDTO(string Genre, int NumberOfAlbums);
    public record PublisherPerCountryDTO(string Country, int NumberOfPublishers);
    public record ReleasePerCountryDTO(string Country, int NumberOfReleases);
    public record ReleasePerYearDTO(int Year, int NumberOfReleases);
    public record AlbumPerYearDTO(int Year, int NumberOfAlbums);
    public record ArtistSummaryDTO(int Id, string Name,
                                   IEnumerable<string> Genres,
                                   IEnumerable<MemberDTO> Members,
                                   IEnumerable<BandsDTO> Bands,
                                   IEnumerable<AlbumDTO> Albums,
                                   IEnumerable<MediaDTO> Media);
    public record ArtistGenreDTO(Artist Artist, string Genre);
    public record ArtistPerGenreDTO(string Genre, int NumberOfArtists);

    public record AlbumDTO(int Id, string Title, int Year, TimeSpan TotalDuration, int NumberOfTracks);
    public record MemberDTO(int Id, string Name, bool Active);
    public record BandsDTO(int Id, string Name, bool IsMemberActive);
    public record StatisticsDTO(int NumberOfArtists, int NumberOfGenres,
                                int NumberOfAlbums, int NumberOfTracks,
                                int NumberOfReleases, int NumberOfPublishers);
    public record GenreStatDTO(string Genre, int NumberOfArtist, int NumberOfAlbums);
    public record CountryStatDTO(string Country, int NumberOfPublishers, int NumberOfReleases);
    public record YearStatDTO(int Year, int NumberOfAlbums, int NumberOfReleases);

    public enum HttpMethodType
    {
        None, GET, PUT, POST, DELETE
    }

    public record ApiInterfaceMapDTO(IEnumerable<ApiControllerDTO> Controllers);
    public record ApiControllerDTO(string Name, IEnumerable<ApiMethodDTO> Methods);
    public record ApiParameterDTO(string Name, string AssemblyQTypeName);
    public record ApiMethodDTO(string Name,
                               string RequestUri,
                               string AssemblyQReturnType,
                               HttpMethodType MethodType,
                               IEnumerable<ApiParameterDTO> Parameters);
}