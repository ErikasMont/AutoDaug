using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        public string Milage { get; set; }
        [Required]
        public string GasType { get; set; }
        [Required]
        public string Engine { get; set; }
        public string Color { get; set; }
        [Required]
        public string Gearbox { get; set; }
        public int Advert_Id { get; set; }
        public int User_Id { get; set; }
    }
}
