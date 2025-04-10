using System;
using System.Collections.Generic;

namespace FlightProject.Domain.EntitiesDB;

public partial class Aircraft
{
    public int AircraftId { get; set; }

    public string Name { get; set; } = null!;

    public int EconomySeats { get; set; }

    public int BusinessSeats { get; set; }

    public int FirstClassSeats { get; set; }

    public double EconomyPrice { get; set; }

    public double BusinessPrice { get; set; }

    public double FirstClassPrice { get; set; }

    public int AvailableSeats { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
