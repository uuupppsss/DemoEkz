using DemoEkzApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace DemoEkzApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationServiceController : ControllerBase
    {
        readonly User05Context context;
        public ReservationServiceController(User05Context context)
        {
            this.context = context;
        }

        [HttpPost("CreateNewReservation")]
        public async Task<ActionResult> CreateNewReservation(GuestsRegister guestsRegister)
        {
            if (guestsRegister == null) 
                return BadRequest("Invalid reservation");
            context.GuestsRegisters.Add(guestsRegister);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("UpdateReservation")]
        public async Task<ActionResult> UpdateReservation(GuestsRegister guestsRegister)
        {
            if (guestsRegister==null)
                return BadRequest("Invalid reservation");
            context.GuestsRegisters.Update(guestsRegister);
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("RemoveReservation")]
        public async Task<ActionResult> RemoveReservation(GuestsRegister guestsRegister)
        {
            if (guestsRegister == null)
                return BadRequest("Invalid reservation");
            GuestsRegister guestsRegister1 = context.GuestsRegisters.FirstOrDefault(g=>g.Id == guestsRegister.Id);
            if (guestsRegister1 == null)
                return BadRequest("Reservation not found");
            context.GuestsRegisters.Remove(guestsRegister1);
            context.SaveChanges();
            return Ok();
        }
    }
}
