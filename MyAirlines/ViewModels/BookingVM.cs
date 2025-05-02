using FlightProject.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string NationalRegisterNumber { get; set; }

        
        public ClassVM Class { get; set; }  
        public int SelectedMealID { get; set; }  
        public List<MealVM> Meals { get; set; }
        
        public string SeatNumber { get; set; }
        public double TicketPrice { get; set; }
    }

}
