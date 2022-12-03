using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Requests
{
    public class AdvertTypeDto
    {
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(300), MinLength(5)]
        public string Description { get; set; }
    }
}
