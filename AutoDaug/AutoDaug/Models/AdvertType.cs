using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class AdvertType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300), MinLength(5)]
        public string Description { get; set; }
    }
}
