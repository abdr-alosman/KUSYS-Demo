using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class User: IdentityUser
    {
        [Required(ErrorMessage = "Please enter first name")]
        [StringLength(50)]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Please enter last name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? NameSurname { get; set; }
    }
}
