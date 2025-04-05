namespace MyAirlines.ViewModels
{
    public class FlightVM
    {
        public ArrivalPlaceVM? ArrivalPlace { get; set; }
        public DeparturePlaceVM? DeparturePlace { get; set; }
        public TransferPlaceVM? TransferPlace { get; set; }
    }
}
