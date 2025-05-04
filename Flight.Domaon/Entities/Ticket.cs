using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

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

    public int? BookingId { get; set; }

    public string? Departure { get; set; }

    public string? Arrival { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<FlightTicket> FlightTickets { get; set; } = new List<FlightTicket>();

    public virtual Meal Meal { get; set; } = null!;

    public virtual Season Season { get; set; } = null!;
}
