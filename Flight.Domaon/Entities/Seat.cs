using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Seat
{
    public int SeatId { get; set; }

    public string? SeatNumber { get; set; }

    public int AircraftId { get; set; }

    public int ClassId { get; set; }

    public bool? IsOccupied { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
