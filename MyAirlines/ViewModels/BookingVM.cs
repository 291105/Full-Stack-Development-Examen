using FlightProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace MyAirlines.ViewModels
{
    public class BookingVM
    {
        //vlucht
        public int FlightId { get; set; }

        public List<PassengerVM> Passengers { get; set; }

        // Prijsberekening
        public float TotalPricePerBooking { get; set; }
    }

    public class PassengerVM
    {
        // Persoonlijke gegevens
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalRegisterNumber { get; set; }

        // Opties, maaltijd en class
        public int SelectedClassID { get; set; }  
        public int SelectedMealID { get; set; }  

        // Dropdowns
        public IEnumerable<SelectListItem>? AvailableMeals { get; set; }
        public IEnumerable<SelectListItem> AvailableClasses { get; set; }
    }

}
