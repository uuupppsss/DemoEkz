using DemoEkzApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoEkzApi.Controllers
{
    [Authorize(Roles ="admin")]
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
        public async Task<ActionResult<List<CleaningDTO>>> GetCleaningsList()
        {
            List<CleaningDTO> result = new List<CleaningDTO>();
            foreach (var c in context.Cleanings)
            {
                result.Add(new CleaningDTO()
                {
                    Id= c.Id,
                    Cleaner=c.Cleaner,
                    RoomId=c.RoomId,
                    Date=c.Date,
                    IsDone=c.IsDone,
                });
            }
            return Ok(result);
        }

        [HttpPost("CreateNewCleaning")]
        public async Task<ActionResult> CreateNewCleaning(CleaningDTO cleaning)
        {
            if (cleaning == null)
                return BadRequest("Invalid data");
            Cleaning cleaningDTO = new Cleaning()
            {
                Id = cleaning.Id,
                Cleaner = cleaning.Cleaner,
                RoomId = cleaning.RoomId,
                Date = cleaning.Date,
                IsDone = cleaning.IsDone,
                Room=context.НомернойФондs.FirstOrDefault(r=>r.Номер==cleaning.RoomId)
            };
            if (cleaningDTO.Room == null)
                return BadRequest("Invalid data");
            context.Cleanings.Add(cleaningDTO);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("RemoveCleaning")]
        public async Task<ActionResult> RemoveCleaning(int id)
        {
            if (id == 0)
                return BadRequest("Invalid data");
            Cleaning cleaning1 = context.Cleanings.FirstOrDefault(c => c.Id == id);
            if (cleaning1 == null)
                return BadRequest("Cleaning not found");
            context.Cleanings.Remove(cleaning1);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("UpdateCleaning")]
        public async Task<ActionResult> UpdateCleaning(CleaningDTO cleaning)
        {
            if (cleaning == null)
                return BadRequest("Invalid data");
            Cleaning cleaning1 = context.Cleanings.FirstOrDefault(c => c.Id == cleaning.Id);
            if (cleaning1 == null)
                return BadRequest("Cleaning not found");
            Cleaning cleaningDTO = new Cleaning()
            {
                Id = cleaning.Id,
                Cleaner = cleaning.Cleaner,
                RoomId = cleaning.RoomId,
                Date = cleaning.Date,
                IsDone = cleaning.IsDone,
                Room = context.НомернойФондs.FirstOrDefault(r => r.Номер == cleaning.RoomId)
            };
            if (cleaningDTO.Room == null)
                return BadRequest("Invalid data");
            context.Cleanings.Update(cleaningDTO);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
