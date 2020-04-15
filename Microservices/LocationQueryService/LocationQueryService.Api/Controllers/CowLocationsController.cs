using System;
using System.Threading.Tasks;
using LocationQueryService.Application.Service;
using LocationQueryService.Data.Contexts;
using LocationQueryService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationQueryService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CowLocationsController : ControllerBase
    {
        private readonly QueryService _queryService;

        public CowLocationsController(QueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpPost]
        public async Task<IActionResult> Report(string earTag)
        {
            try
            {
                var location = await _queryService.Report(earTag);
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
            
        }
    }
}
