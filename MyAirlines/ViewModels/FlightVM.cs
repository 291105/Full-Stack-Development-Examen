namespace MyAirlines.ViewModels
{
    public class FlightVM
    {
       public int FlightId { get; set; }
        public String DeparturePlace { get; set; }
        public String ArrivalPlace { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public int AircraftId { get; set; }
        //er moet nog een lijst komen van tussenstops hier
        
    }
}
