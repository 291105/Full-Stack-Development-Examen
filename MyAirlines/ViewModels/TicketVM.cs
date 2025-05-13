using MyAirlines.Controllers.API;

namespace MyAirlines.ViewModels
{
    public class TicketVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int NationalRegisterNumber { get; set; }
        public int Price { get; set; }
        public string SeatNumber { get; set; }
        public string MealName { get; set; }
        public string ClassName { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        
        
        
    }
}
