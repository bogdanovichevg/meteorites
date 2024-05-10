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
        public async Task<IActionResult> Get([FromQuery] ReqFilterMeteorites req)
        {
            var meteorites = await _meteoriteServices.GetByFiltersAsync(req);
            return Ok(meteorites);
        }

        /// <summary>
        /// Get all classes Meteorites
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [Route("GetMeteoritesClasses")]
        [HttpGet]
        public async Task<IActionResult> GetAllClassesMeteorites()
        {
            return Ok(await _meteoriteServices.GetAllClassesAsync());
        }
    }
}
