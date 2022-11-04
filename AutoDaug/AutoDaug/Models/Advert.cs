using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class Advert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300), MinLength(5)]
        public string Description { get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int Price { get; set; }
        public int AdvertType_Id { get; set; }
        public int User_Id { get; set; }
    }
}
