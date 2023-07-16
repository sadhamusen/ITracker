using System.ComponentModel.DataAnnotations;

namespace ITracker.Controllers
{
    public class NewUser
    {

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
        public string? instagram { get; set; }
        public string? image { get; set; }
        public int? rating { get; set; }
        public string? bio { get; set; }
    }
}