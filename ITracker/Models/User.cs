using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeTracker.Models
{
    public class User
    {

        public int id { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Display(Name = "Role")]
        public int rId { get; set; }

        [ForeignKey("UserType")]
        public virtual Role? Role { get; set; }
    }
}
