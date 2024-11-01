using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class Cleaning
{
    public int Id { get; set; }

    public string Cleaner { get; set; } = null!;

    public int RoomId { get; set; }

    public DateTime Date { get; set; }

    public bool IsDone { get; set; }

    public virtual НомернойФонд Room { get; set; } = null!;
}
