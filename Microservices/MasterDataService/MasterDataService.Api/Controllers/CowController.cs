using System;
using MasterDataService.Application.Service;
using MasterDataService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataService.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CowController : ControllerBase
    {
        private readonly DataService _dataService;

        public CowController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cow cow)
        {
            try
            {
                _dataService.Upsert(cow);
                return Ok(cow);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }

        [HttpGet]
        public IActionResult Read([FromQuery] string earTag)
        {
            try
            {
                var cow = _dataService.Read(earTag);
                return Ok(cow);
            }
            catch (Exception e)
            {
                return StatusCode(503, e.Message);
            }
        }
    }
}
