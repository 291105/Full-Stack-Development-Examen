namespace FlightProject.Domain.Entities
{
    public class HotelSearchResult
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public double StarRating { get; set; }
        public string HotelUrl { get; set; } = string.Empty;

    }
}
