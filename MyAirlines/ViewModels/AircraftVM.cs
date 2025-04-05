namespace MyAirlines.ViewModels
{
    public class AircraftVM
    {
        public string? Name { get; set; }
        public int EconomySeats { get; set; }
        public int BusinessSeats { get; set; }
        public int FirstClassSeats { get; set; }
        public double EconomyPrice { get; set; }
        public double BusinessPrice { get; set; }
        public double FirstClassPrice { get; set; }
        public int AvailableSeats { get; set; }
    }
}
