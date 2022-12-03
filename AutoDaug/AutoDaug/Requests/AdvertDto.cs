using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Requests
{
    public class AdvertDto
    {
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(300), MinLength(5)]
        public string Description { get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int Price { get; set; }
        public int AdvertType_Id { get; set; }
    }
}
