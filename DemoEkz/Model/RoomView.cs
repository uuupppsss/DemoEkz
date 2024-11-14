using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEkz.Model
{
    public class RoomView
    {
        public string? Floor { get; set; }

        public int Room_id { get; set; }

        public string? Category { get; set; }

        public string? Status { get; set; }

        public decimal Price { get; set; }
    }
}
