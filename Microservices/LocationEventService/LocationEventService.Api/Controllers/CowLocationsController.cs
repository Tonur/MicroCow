using LocationEventService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationEventService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowLocationsController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] CowLocation cowLocation)
        {
            //_context.Upsert(cowLocation);
        }
    }
}
