using InitiativeTracker.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITracker.Models
{
    public class TaskApprovers
    {
        public int Id { get; set; }


        [DisplayName("IdOfApprover")]
        public int approverId { get; set; }

        [ForeignKey("IdOfApprover")]
        public User approver { get; set; }


        [DisplayName("TaskId")]
        public int taskId { get; set; }

        [ForeignKey("IdOfTask")]
        public Idea idea { get; set; }

        public string status { get; set; }
    }
}
