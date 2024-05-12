using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeteoritesWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeteoritesController : ControllerBase
    {
        private readonly IMeteoriteServices _meteoriteServices;
        public MeteoritesController(IMeteoriteServices meteoriteServices)
        {
            _meteoriteServices = meteoriteServices;
        }

        /// <summary>
        /// Get meteorite info
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Route("GetMeteorites")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MeteoritesFiltersReq req)
        {
            var meteorites = await _meteoriteServices.GetFiltered(req);
            return Ok(meteorites);
        }

        /// <summary>
        /// Get all classes Meteorites
        /// </summary>
        /// <returns></returns>
        [Route("GetMeteoritesClasses")]
        [HttpGet]
        public async Task<IActionResult> GetAllClassesMeteorites()
        {
            return Ok(await _meteoriteServices.GetAllClassesAsync());
        }
    }
}
