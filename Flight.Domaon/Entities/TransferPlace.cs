using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class TransferPlace
{
    public int TransferId { get; set; }

    public int PlaceId { get; set; }

    public virtual ICollection<FlightStop> FlightStops { get; set; } = new List<FlightStop>();

    public virtual Place Place { get; set; } = null!;
}
