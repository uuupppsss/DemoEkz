namespace DemoEkz.Model
{
    public class UserDTO
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string Password { get; set; } = null!;

        public string Login { get; set; } = null!;

        public bool IsAutorized { get; set; }
    }
}
