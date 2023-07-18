using InitiativeTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITracker.Models
{
    public class Contributor
    {
        public int id { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }

        [Display(Name = "TaskId")]
        public int taskId { get; set; }

        [ForeignKey("ideaId")]
        public int  ideaId { get; set; }
    }
}
