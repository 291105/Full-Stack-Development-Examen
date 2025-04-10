using System;
using System.Collections.Generic;

namespace FlightProject.Domain.EntitiesDB;

public partial class Ticket
{
    public int TicketId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string NationalRegisterNumber { get; set; } = null!;

    public double Price { get; set; }

    public string SeatNumber { get; set; } = null!;

    public int MealId { get; set; }

    public int ClassId { get; set; }

    public int SeasonId { get; set; }

    public int AircraftId { get; set; }

    public int FlightId { get; set; }

    public virtual Aircraft Aircraft { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Class Class { get; set; } = null!;

    public virtual Flight Flight { get; set; } = null!;

    public virtual Meal Meal { get; set; } = null!;

    public virtual Season Season { get; set; } = null!;
}
