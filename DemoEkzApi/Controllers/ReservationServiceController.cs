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
        public async Task<ActionResult> CreateNewReservation(GuestRegisterDTO guestsRegister)
        {
            if (guestsRegister == null)
                return BadRequest("Invalid reservation");
            GuestsRegister reservation = new GuestsRegister()
            {
                GuestId = guestsRegister.GuestId,
                RoomId = guestsRegister.RoomId,
                EntryDate = guestsRegister.EntryDate,
                LeavingDate = guestsRegister.LeavingDate,
                Price = guestsRegister.Price,
                IsPaid = guestsRegister.IsPaid,
                Receipt = guestsRegister.Receipt,
                Guest = context.Guests.FirstOrDefault(g => g.Id == guestsRegister.GuestId),
                Room=context.НомернойФондs.FirstOrDefault(r=>r.Номер==guestsRegister.RoomId)
            };
            if (reservation.Guest == null || reservation.Room == null)
                return BadRequest("Invalid data");
            context.GuestsRegisters.Add(reservation);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("UpdateReservation")]
        public async Task<ActionResult> UpdateReservation(GuestRegisterDTO guestsRegister)
        {
            if (guestsRegister == null)
                return BadRequest("Invalid reservation");
            GuestsRegister guestsRegister1 = context.GuestsRegisters.FirstOrDefault(x => x.Id == guestsRegister.Id);
            if (guestsRegister1 == null)
                return BadRequest("Reservation not found");
            GuestsRegister reservation = new GuestsRegister()
            {
                GuestId = guestsRegister.GuestId,
                RoomId = guestsRegister.RoomId,
                EntryDate = guestsRegister.EntryDate,
                LeavingDate = guestsRegister.LeavingDate,
                Price = guestsRegister.Price,
                IsPaid = guestsRegister.IsPaid,
                Receipt = guestsRegister.Receipt,
                Guest = context.Guests.FirstOrDefault(g => g.Id == guestsRegister.GuestId),
                Room = context.НомернойФондs.FirstOrDefault(r => r.Номер == guestsRegister.RoomId)
            };
            if (reservation.Guest == null || reservation.Room == null)
                return BadRequest("Invalid data");
            context.GuestsRegisters.Update(reservation);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("RemoveReservation")]
        public async Task<ActionResult> RemoveReservation(int id)
        {
            if (id == 0)
                return BadRequest("Invalid reservation");
            GuestsRegister guestsRegister1 = context.GuestsRegisters.FirstOrDefault(g => g.Id == id);
            if (guestsRegister1 == null)
                return BadRequest("Reservation not found");
            context.GuestsRegisters.Remove(guestsRegister1);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetReservationsList")]
        public async Task<ActionResult<List<GuestRegisterDTO>>> GetReservationsList()
        {
            List<GuestRegisterDTO> result= new List<GuestRegisterDTO>();
            foreach (var r in context.GuestsRegisters)
            {
                result.Add(new GuestRegisterDTO()
                {
                    Id= r.Id,
                    RoomId= r.RoomId,
                    GuestId= r.GuestId,
                    EntryDate= r.EntryDate,
                    LeavingDate= r.LeavingDate,
                    Receipt= r.Receipt,
                    Price= r.Price,
                    IsPaid= r.IsPaid
                });
            }
            return Ok(result);

        }
    }
}
