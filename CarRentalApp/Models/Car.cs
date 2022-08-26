using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentalApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Manufacturer { get; set; }
        public string CarModel { get; set; }
        public CarType CarType { get; set; }
        public int ManufacturingYear { get; set; }
        public string Price { get; set; }
        public int Seats { get; set; }
        public Transmission Transmission { get; set; }
        public int Doors { get; set; }
        public string Additional { get; set; }
        public string? CarPhoto { get; set; }
        public bool IsAvailable { get; set; }
    }
}

public enum CarType
{
    Suv,
    Sedan,
    Convertib,
    Coupe,
    Minivan,
    Hatchback
}

public enum Transmission
{
    M,
    A
}