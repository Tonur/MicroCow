using MasterDataService.Api.Data;
using MasterDataService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CowController : ControllerBase
    {
        private readonly MasterDataServiceContext _context;

        public CowController(MasterDataServiceContext context)
        {
            _context = context;
        }

        [HttpPost]
        public void Create([FromBody] Cow cow)
        {
            _context.Upsert(cow);
        }
    }
}
