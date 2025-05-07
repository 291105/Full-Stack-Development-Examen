using FlightProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyAirlines.ViewModels
{
    public class FlightSearchVM
    {
        [Required(ErrorMessage = "No departure city selected")]
        public int? SelectedDepartureId { get; set; }
        [Required(ErrorMessage = "No arrival city selected")]
        public int? SelectedArrivalId { get; set; }
        [Required(ErrorMessage = "No class selected")]
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

