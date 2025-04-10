using FlightProject.Domain.EntitiesDB;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyAirlines.ViewModels
{
    public class FlightSearchVM
    {
        public IEnumerable<SelectListItem>? AirportDeparture { get; set; }  // Lijst van luchthavens
        
        public IEnumerable<SelectListItem>? AirportArrival { get; set; }
        public int? SelectedDepartureId { get; set; }  // Geselecteerde vertrek luchthaven
        public int? SelectedArrivalId { get; set; }     // Geselecteerde aankomst luchthaven
        public IEnumerable<FlightVM> Flights { get; set; }            // Lijst van vluchten
        public DateTime? DepartureDate { get; set; } //datum van vertrek
    }
}

