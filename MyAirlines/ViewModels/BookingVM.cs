using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace MyAirlines.ViewModels
{
    public class BookingVM
    {
        public TicketVM? ticket {  get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAdress { get; set; }
        public int PageNumber;

    }
}
