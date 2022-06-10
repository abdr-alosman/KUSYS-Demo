using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Dto
{
    public class StudentModel
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50)]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Please enter email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter Student No")]
        public long StudentNo { get; set; }
    }
}
