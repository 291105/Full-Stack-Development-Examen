using System;
using System.Collections.Generic;

namespace FlightProject.Domain.EntitiesDB;

public partial class Season
{
    public int SeasonId { get; set; }

    public double Rate { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
