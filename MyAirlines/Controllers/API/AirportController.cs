using AutoMapper;
using FlightProject.Domain.Entities;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.ViewModels;

namespace tst.Controllers.API
{
    [Route("api/luchthavens")]
    [ApiController]
    public class AirportController : Controller
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService)
        {
            _airportService = airportService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airport>>> Get()
        {
            try
            {
                var airports = await _airportService.GetAllAsync();
                

                return Ok(airports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        /*private readonly IService<Place> _service;
        private readonly IMapper _mapper;

        public AirportController(IMapper mapper, IService<Place> service)
        {
            _mapper = mapper;
            _service = service;
        }

        //// Ophalen van alle luchthavens
        [HttpGet/*, Authorize*//*]
        public async Task<ActionResult<IEnumerable<PlaceVM>>> Get()
        {
            try
            {
                var airports = await _service.GetAllAsync();
                var airportVMs = _mapper.Map<List<PlaceVM>>(airports);

                return Ok(airportVMs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }*/


    }

}
