using System;
using System.Collections.Generic;
using DemoEkzApi.Model;

namespace DemoEkzApi;

public partial class Роли
{
    public int Id { get; set; }

    public string Название { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
