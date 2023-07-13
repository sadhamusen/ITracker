using System.ComponentModel.DataAnnotations;

namespace InitiativeTracker.Models
{
    public class Role
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string type { get; set; }

        
    }
}
