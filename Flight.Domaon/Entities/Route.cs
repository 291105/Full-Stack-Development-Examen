using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Route
{
    public int RouteId { get; set; }

    public int DepartureAirportId { get; set; }

    public int ArrivalAirportId { get; set; }

    public int? TransferAirportId1 { get; set; }

    public int? TransferAirportId2 { get; set; }

    public virtual Airport ArrivalAirport { get; set; } = null!;

    public virtual Airport DepartureAirport { get; set; } = null!;

    public virtual Airport? TransferAirportId1Navigation { get; set; }

    public virtual Airport? TransferAirportId2Navigation { get; set; }
}
