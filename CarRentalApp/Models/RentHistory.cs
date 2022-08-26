namespace CarRentalApp.Models
{
    public class RentHistory
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}
