using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Flight
{
    public int FlightId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int AircraftAircraftId { get; set; }

    public int DepartureAirportId { get; set; }

    public int ArrivalAirportId { get; set; }

    public TimeOnly? Duration { get; set; }

    public virtual Aircraft AircraftAircraft { get; set; } = null!;

    public virtual Airport ArrivalAirport { get; set; } = null!;

    public virtual Airport DepartureAirport { get; set; } = null!;

    public virtual ICollection<FlightTicket> FlightTickets { get; set; } = new List<FlightTicket>();
}
