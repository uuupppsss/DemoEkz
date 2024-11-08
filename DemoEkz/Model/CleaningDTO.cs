using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoEkz.Model
{
    public class CleaningDTO
    {
        public int Id { get; set; }

        public string Cleaner { get; set; } = null!;

        public int RoomId { get; set; }

        public DateTime Date { get; set; }

        public bool IsDone { get; set; }
    }
}
