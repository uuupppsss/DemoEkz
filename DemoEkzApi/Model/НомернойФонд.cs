using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class НомернойФонд
{
    public string? Этаж { get; set; }

    public int Номер { get; set; }

    public string? Категория { get; set; }

    public virtual ICollection<Cleaning> Cleanings { get; set; } = new List<Cleaning>();

    public virtual ICollection<GuestsRegister> GuestsRegisters { get; set; } = new List<GuestsRegister>();

    public virtual ICollection<Otchet> Otchets { get; set; } = new List<Otchet>();
}
