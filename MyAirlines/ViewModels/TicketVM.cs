using MyAirlines.Controllers.API;

namespace MyAirlines.ViewModels
{
    public class TicketVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int NationalRegisterNumber { get; set; }
        public int Price { get; set; }
        public int SeatNumber { get; set; }
        public MealVM? Meal { get; set; }
        public ClassVM? Class { get; set; }
        public SeasonVM? Season { get; set; }
        public AircraftVM? Aircraft { get; set; }
        
    }
}
