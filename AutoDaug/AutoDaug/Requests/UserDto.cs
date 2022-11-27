using System.ComponentModel.DataAnnotations;

namespace AutoDaug.Requests
{
    public class UserDto
    {
        [Required(ErrorMessage = "Please enter username")]
        [MaxLength(50, ErrorMessage = "Username should not be longer than 50 characters"), MinLength(6, 
            ErrorMessage = "Username should not be shorter than 6 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        [MaxLength(12, ErrorMessage = "Please use +370 phone format"), MinLength(12, ErrorMessage = "Please use +370 phone format")]
        public string PhoneNumber { get; set; }
    }
}
