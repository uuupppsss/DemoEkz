﻿using DemoEkzApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoEkzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        readonly User05Context context;
        public RoomsController(User05Context context)
        {
            this.context = context;
        }

        [HttpGet("GetRoomsList")]
        public async Task<ActionResult<List<RoomDTO>>> GetRoomsList()
        {
            List<RoomDTO> result = new List<RoomDTO>();
            foreach (var r in context.НомернойФондs)
            {
                result.Add(new RoomDTO
                {
                    Floor = r.Этаж,
                    Num = r.Номер,
                    Category = r.Категория
                });
            }
            return Ok(result);
        }

        [HttpGet("GetRoomsOtchetList")]
        public async Task<ActionResult<List<OtchetDTO>>> GetRoomsOtchetList()
        {
            List<OtchetDTO> result = new List<OtchetDTO>();
            foreach (var o in context.Otchets)
            {
                result.Add(new OtchetDTO
                {
                    Id = o.Id,
                    Room_id = o.Номер,
                    Status = o.Статус,
                    Price = o.Price
                });
            }
            return Ok(result);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("UpdateRoomStatuses")]
        public async Task<ActionResult> UpdateRoomStatuses()
        {
            DateTime today = DateTime.Today;
            foreach (var booking in context.GuestsRegisters)
            {
                if (booking.LeavingDate == today)
                {
                    var otchet = context.Otchets.FirstOrDefault(o => o.Номер == booking.RoomId);
                    if (otchet != null&&otchet.Статус=="Занят")
                    {
                        otchet.Статус = "Грязный";
                        context.Otchets.Update(otchet);
                    }
                }
            }
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
