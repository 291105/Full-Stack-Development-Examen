using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyAirlines.ViewModels
{
    public class FlightsByAirportVM
    {
       
        public IEnumerable<SelectListItem>? Airports { get; set; }
        public IEnumerable<FlightVM> Flights { get; set; }
    }
}
