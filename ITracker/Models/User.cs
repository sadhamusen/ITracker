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

        public string? secondary_email { get; set; }
        public string? dob { get; set; }
        public string? mobile_number { get; set; }
        public string? blood_grop { get; set; }
        public string? linkedin { get; set; }
        public string? instagram { get; set;}
        public string? image { get; set; }
        public int? rating { get; set; }
        public string? bio  { get; set; }
        public string? created_time { get; set; }= DateTime.Now.ToString();
        [Display(Name = "Role")]
        public int rId { get; set; }

        [ForeignKey("UserType")]
        public virtual Role? Role { get; set; }
    }
}
