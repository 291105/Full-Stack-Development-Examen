using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class DeparturePlace
{
    public int DepartureId { get; set; }

    public int PlaceId { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual Place Place { get; set; } = null!;
}
