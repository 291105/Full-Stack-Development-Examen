using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class FlightStop
{
    public int StopOrder { get; set; }

    public int TransferId { get; set; }

    public int FlightId { get; set; }

    public virtual Flight Flight { get; set; } = null!;

    public virtual TransferPlace Transfer { get; set; } = null!;
}
