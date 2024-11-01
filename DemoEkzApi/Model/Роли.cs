using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class Роли
{
    public int Id { get; set; }

    public string Название { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
