using System;
using System.Collections.Generic;

namespace DemoEkzApi.Model;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Password { get; set; } = null!;

    public string Login { get; set; } = null!;

    public bool IsAutorized { get; set; }

    public virtual Роли Role { get; set; } = null!;
}
