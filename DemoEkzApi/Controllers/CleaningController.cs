using DemoEkzApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoEkzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CleaningController : ControllerBase
    {
        readonly User05Context context;
        public CleaningController(User05Context context)
        {
            this.context = context;
        }

        [HttpGet("GetCleaningsList")]
        public async Task<ActionResult<List<Cleaning>>> GetCleaningsList()
        {
            List<Cleaning> list = [.. context.Cleanings];
            return Ok(list); 
        }

        [HttpPost("CreateNewCleaning")]
        public async Task<ActionResult> CreateNewCleaning(Cleaning cleaning)
        {
            if (cleaning == null)
                return BadRequest("Invalid data");
            context.Cleanings.Add(cleaning);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("RemoveCleaning")]
        public async Task<ActionResult> RemoveCleaning(Cleaning cleaning)
        {
            if (cleaning == null)
                return BadRequest("Invalid data");
            Cleaning cleaning1 = context.Cleanings.FirstOrDefault(c => c.Id == cleaning.Id);
            if (cleaning1 == null)
                return BadRequest("Cleaning not found");
            context.Cleanings.Remove(cleaning1);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateCleaning")]
        public async Task<ActionResult> UpdateCleaning(Cleaning cleaning)
        {
            if (cleaning == null)
                return BadRequest("Invalid data");
            Cleaning cleaning1 = context.Cleanings.FirstOrDefault(c => c.Id == cleaning.Id);
            if (cleaning1 == null)
                return BadRequest("Cleaning not found");
            context.Cleanings.Update(cleaning);
            context.SaveChanges();
            return Ok();
        }
    }
}
