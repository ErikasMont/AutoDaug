using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Make { get; set; }
        [Required]
        [MaxLength(50), MinLength(2)]
        public string Model { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        [Required]
        [MaxLength(6), MinLength(1)]
        public string Milage { get; set; }
        [Required]
        [MaxLength(50), MinLength(3)]
        public string GasType { get; set; }
        [Required]
        [MaxLength(3), MinLength(1)]
        public string Engine { get; set; }
        [MaxLength(30), MinLength(5)]
        public string Color { get; set; }
        [Required]
        [MaxLength(30), MinLength(5)]
        public string Gearbox { get; set; }
        public int? Advert_Id { get; set; }
        public int User_Id { get; set; }
    }
}
