using System;
using System.Collections.Generic;

namespace FlightProject.Domain.Entities;

public partial class Airport
{
    public int AirportId { get; set; }

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; } = null!;

    public virtual ICollection<Flight> FlightArrivalAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartureAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Route> RouteArrivalAirports { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteDepartureAirports { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteTransferAirportId1Navigations { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteTransferAirportId2Navigations { get; set; } = new List<Route>();
}
