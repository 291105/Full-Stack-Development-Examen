using FlightProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyAirlines.ViewModels
{
    public class FlightSearchVM
    {
        public int? SelectedDepartureId { get; set; }
        public int? SelectedArrivalId { get; set; }
        public int? SelectedClassId { get; set; }
        public int AantalPersonenNodig { get; set; }
        public DateOnly? DepartureDate { get; set; }

        public List<FlightGroupVM> FlightGroups { get; set; } = new List<FlightGroupVM>();

        public SelectList AirportDeparture { get; set; }
        public SelectList AirportArrival { get; set; }
        public SelectList Class { get; set; }

        public DateTime MinDepartureTime { get; set; }
        public DateTime MaxDepartureTime { get; set; }


    }
}

