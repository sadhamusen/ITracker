using ITracker.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeTracker.Models
{
    public class Idea
    {
        public int Id { get; set; }

        public string title { get; set; }

        public string shortDescription { get; set; }

        public string? longDescription { get; set; }

        public string? startDate { get; set; }

        public string? endDate { get; set; }

        public string? status { get; set; }

        public string? signOff { get; set; }

        public int isDelete { get; set; }

        public int like { get; set; }

        public string? ideaCreatedDate { get; set; } = DateTime.Now.ToString();

        [DisplayName("OwnerId")]
        public int idOfOwner { get; set; }

        [ForeignKey("IdOFUser")]
        public virtual User? User { get; set; }


        [DisplayName("AppId")]
        public int? approverId { get; set; }

        public ICollection<Contributor> contributors { get; set; }

    }
}
