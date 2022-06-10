using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Dto
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string? LastName { get; set; }
        
    }
}
