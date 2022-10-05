using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class AdvertType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
