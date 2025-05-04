using FlightProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace MyAirlines.ViewModels
{
    public class BookingVM
    {
        //vlucht
        public List<FlightVM> Flight { get; set; }

        public List<PassengerVM> Passengers { get; set; }

        // Prijsberekening
        public double TotalPricePerBooking { get; set; }
    }
    //beetje een ticket
    public class PassengerVM
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string NationalRegisterNumber { get; set; }

        public int SelectedClassID { get; set; }
        public ClassVM Class { get; set; }
        [Required]
        public int? SelectedMealID { get; set; }  
        public List<MealVM> Meals { get; set; }
        
        public string SeatNumber { get; set; }
        public double TicketPrice { get; set; }
    }

}
