using AutoMapper;
using FlightProject.Domain.Entities;
using FlightProject.Services;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.ViewModels;

namespace MyAirlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : Controller
    {
        private readonly IFlightService _service;
        private readonly IMapper _mapper;

        public FlightController(IMapper mapper, IFlightService service)
        {
            _mapper = mapper;
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightRouteForApiVM>>> Get([FromQuery] int departureAirportId, [FromQuery] int arrivalAirportId)
        {
            if (departureAirportId == 0 || arrivalAirportId == 0)
            {
                return BadRequest("Beide luchthaven IDs zijn verplicht.");
            }

            try
            {
                var trips = await _service.GetFlightsFromTwoAirports(departureAirportId, arrivalAirportId);

                var flightRoutes = trips.Select(route => new FlightRouteForApiVM
                {
                    Flights = _mapper.Map<List<FlightVM>>(route)
                }).ToList();

                return Ok(flightRoutes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
