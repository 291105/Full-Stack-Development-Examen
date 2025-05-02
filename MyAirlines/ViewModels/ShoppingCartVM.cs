namespace MyAirlines.ViewModels
{
    public class ShoppingCartVM
    {
        public List<CartVM>? Carts { get; set; }
        public double ComputeTotalValue() =>
            Carts!.Sum(e => e.Price);
        //berekent de totale prijs nog eens, stel dat er eentje gedelete is dat die automatisch aanpast

    }
    public class CartVM
    {
        public int TicketId { get; set; }
        public string Departure { get; set; } 
        public string Arrival { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string NationalRegisterNumber { get; set; }
        public string ClassName { get; set; }
        public string MealName { get; set; }
        public double Price { get; set; }
        public List<FlightVM> Flights { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
