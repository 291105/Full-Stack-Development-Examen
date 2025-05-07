using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
