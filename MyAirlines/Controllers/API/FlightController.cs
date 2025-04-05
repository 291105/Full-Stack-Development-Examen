using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyAirlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : Controller
    {
        //private readonly IService<Flight> _service;
        //private readonly IMapper _mapper;

        //public FlightController(IMapper mapper, IService<Flight> service)
        //{
        //    _mapper = mapper;
        //    _service = service;
        //}

        //// Ophalen van vertrek & aankomstluchthaven
        //[HttpGet, Authorize]
        //public async Task<ActionResult<IEnumerable<FlightVM>>> Get([FromQuery] int departureAirportId, [FromQuery] int arrivalAirportId)
        //{
        //    if (departureAirportId == 0 || arrivalAirportId == 0)
        //    {
        //        return BadRequest("Beide luchthaven IDs zijn verplicht.");
        //    }

        //    try
        //    {
        //        var flights = await _service.GetAllAsync();
        //        var flightVMs = _mapper.Map<List<FlightVM>>(flights);
        //        return Ok(flightVMs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = ex.Message });
        //    }
        //}
    }
}
