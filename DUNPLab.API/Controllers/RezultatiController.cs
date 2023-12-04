using DUNPLab.API.Infrastructure;
using DUNPLab.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DUNPLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RezultatiController : ControllerBase
    {
        private readonly DunpContext context;

        public RezultatiController(DunpContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<List<RezultatOdMasine>> GetAll()
        {
            return await context.RezultatiOdMasine
                .Include(rm => rm.SupstanceRezultati)
                .ToListAsync();
        }
    }
}
