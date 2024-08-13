using System.ComponentModel.DataAnnotations;

namespace ConstructionManagementPresentation.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string ActivityType { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }  
    }
}
