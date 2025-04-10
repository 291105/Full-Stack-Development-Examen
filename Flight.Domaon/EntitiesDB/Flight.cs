using System;
using System.Collections.Generic;

namespace FlightProject.Domain.EntitiesDB;
public partial class Flight
{
    public int FlightId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int ArrivalId { get; set; }

    public int DepartureId { get; set; }

    public virtual ArrivalPlace Arrival { get; set; } = null!;

    public virtual DeparturePlace Departure { get; set; } = null!;

    public virtual ICollection<FlightStop> FlightStops { get; set; } = new List<FlightStop>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
