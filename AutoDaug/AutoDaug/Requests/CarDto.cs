using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Requests
{
    public class CarDto
    {
        [MaxLength(50), MinLength(2)]
        public string Make { get; set; }
        [MaxLength(50), MinLength(2)]
        public string Model { get; set; }
        public DateTime ManufactureDate { get; set; }
        [MaxLength(6), MinLength(1)]
        public string Milage { get; set; }
        [MaxLength(50), MinLength(3)]
        public string GasType { get; set; }
        [MaxLength(3), MinLength(1)]
        public string Engine { get; set; }
        [MaxLength(30), MinLength(5)]
        public string Color { get; set; }
        [MaxLength(30), MinLength(5)]
        public string Gearbox { get; set; }
        public int? Advert_Id { get; set; }
    }
}
