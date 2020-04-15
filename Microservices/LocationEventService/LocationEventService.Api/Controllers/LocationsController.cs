using System;
using LocationEventService.Application.Services;
using LocationEventService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationEventService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly EventService _eventService;

        public LocationsController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Location location)
        {
            try
            {
                _eventService.Upsert(location);
                return Ok(location);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }
    }
}
