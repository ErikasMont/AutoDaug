using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [MaxLength(255), MinLength(8, ErrorMessage = "The password has to containt at least 8 characters")]
        public string Password { get; set; }
        public string AccountState { get; set; }
        public string Role { get; set; }
    }
}
