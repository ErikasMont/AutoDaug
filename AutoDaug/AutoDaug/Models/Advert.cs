using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class Advert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public int AdvertType_Id { get; set; }
        public int User_Id { get; set; }
    }
}
