using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class Otchet
{
    public int Номер { get; set; }

    public string Статус { get; set; } = null!;

    public int Id { get; set; }

    public virtual НомернойФонд НомерNavigation { get; set; } = null!;
}
