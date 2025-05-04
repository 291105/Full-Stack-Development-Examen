using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Booking
{
    public int BookingId { get; set; }

    public double TotalPricePerBooking { get; set; }

    public string? UserId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual AspNetUser? User { get; set; }
}
