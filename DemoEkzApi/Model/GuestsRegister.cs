using System;
using System.Collections.Generic;
using DemoEkzApi.Model;

namespace DemoEkzApi;

public partial class GuestsRegister
{
    public int Id { get; set; }

    public int GuestId { get; set; }

    public int RoomId { get; set; }

    public DateTime EntryDate { get; set; }

    public DateTime? LeavingDate { get; set; }

    public string Receipt { get; set; } = null!;

    public bool IsPaid { get; set; }

    public decimal Price { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual НомернойФонд Room { get; set; } = null!;
}
