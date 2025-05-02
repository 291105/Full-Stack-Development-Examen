using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public int TicketId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public double TotalPricePerBooking { get; set; }

    public virtual Ticket Ticket { get; set; } = null!;
}
