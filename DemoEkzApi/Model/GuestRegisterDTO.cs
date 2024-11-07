namespace DemoEkzApi.Model
{
    public class GuestRegisterDTO
    {
        public int Id { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime? LeavingDate { get; set; }

        public string Receipt { get; set; } = null!;

        public bool IsPaid { get; set; }

        public decimal Price { get; set; }
    }
}
