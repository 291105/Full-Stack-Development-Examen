using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Place
{
    public int PlaceId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ArrivalPlace> ArrivalPlaces { get; set; } = new List<ArrivalPlace>();

    public virtual ICollection<DeparturePlace> DeparturePlaces { get; set; } = new List<DeparturePlace>();

    public virtual ICollection<TransferPlace> TransferPlaces { get; set; } = new List<TransferPlace>();
}
