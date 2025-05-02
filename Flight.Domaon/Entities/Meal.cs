using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Meal
{
    public int MealId { get; set; }

    public string Name { get; set; } = null!;

    public string Kind { get; set; } = null!;

    public double Price { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
