using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class Guest
{
    public int Id { get; set; }

    public string FirstnameName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? SecondName { get; set; }

    public virtual ICollection<GuestsRegister> GuestsRegisters { get; set; } = new List<GuestsRegister>();
}
