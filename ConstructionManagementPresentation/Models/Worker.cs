using ConstructionManagementPresentation.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementPresentation.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public Role Role { get; set; } 

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

    

    }
}