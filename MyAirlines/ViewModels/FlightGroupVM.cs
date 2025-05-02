namespace MyAirlines.ViewModels
{
    public class FlightGroupVM
    {
        public List<FlightVM> Flights { get; set; }
        public TimeSpan TotalTravelTime { get; set; }
    }
}
