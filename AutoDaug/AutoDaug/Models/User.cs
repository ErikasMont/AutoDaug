using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        [MaxLength(50), MinLength(6)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [MaxLength(200), MinLength(8, ErrorMessage = "The password has to containt at least 8 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [MaxLength(12, ErrorMessage = "Please use +370 phone format"), MinLength(12, ErrorMessage = "Please use +370 phone format")]
        public string PhoneNumber { get; set; }
        public string AccountState { get; set; }
        public bool IsAdmin { get; set; }
    }
}
