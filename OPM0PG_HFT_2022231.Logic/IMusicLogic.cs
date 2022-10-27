using OPM0PG_HFT_2022231.Models.DataTransferObjects;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IMusicLogic
    {
        AlbumSummaryDTO GetAlbumSummary(int albumId);
        ArtistSummaryDTO GetArtistSummary(int artistId);
        StatisticsDTO GetStatistics();
    }
}