using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class FlightTicket
{
    public int TicketId { get; set; }

    public int FlightId { get; set; }

    public int FlightTicketId { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;
}
