using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class ArrivalPlace
{
    public int ArrivalId { get; set; }

    public int PlaceId { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual Place Place { get; set; } = null!;
}
