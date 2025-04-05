using Microsoft.AspNetCore.Mvc;

namespace tst.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : Controller
    {
        
        //private readonly IService<Airport> _service;
        //private readonly IMapper _mapper;

        //public AirportController(IMapper mapper, IService<Airport> service)
        //{
        //    _mapper = mapper;
        //    _service = service;
        //}

        //// Ophalen van alle luchthavens
        //[HttpGet, Authorize]
        //public async Task<ActionResult<IEnumerable<AirportVM>>> Get()
        //{
        //    try
        //    {
        //        var airports = await _service.GetAllAsync();
        //        var airportVMs = _mapper.Map<List<AirportVM>>(airports);

        //        return Ok(airportVMs);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { error = ex.Message });
        //    }
        //}
        
    }

}
