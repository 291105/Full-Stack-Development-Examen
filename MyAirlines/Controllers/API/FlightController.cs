using AutoMapper;
using FlightProject.Domain.EntitiesDB;
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

        // Ophalen van vluchten tussen vertrek & aankomstluchthaven
        [HttpGet, /*Authorize*/]
        public async Task<ActionResult<IEnumerable<FlightsByAirportVM>>> Get([FromQuery] int departureAirportId, [FromQuery] int arrivalAirportId)
        {
            if (departureAirportId == 0 || arrivalAirportId == 0)
            {
                return BadRequest("Beide luchthaven IDs zijn verplicht.");
            }

            try
            {
                var flights = await _service.GetFlightsFromTwoAirports(departureAirportId, arrivalAirportId);
                
                FlightsByAirportVM flightsByAirport = new FlightsByAirportVM();
                flightsByAirport.Flights = _mapper.Map<List<FlightVM>>(flights);
                
                
                return Ok(flightsByAirport.Flights);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
