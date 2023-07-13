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
    }
}