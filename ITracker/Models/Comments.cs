using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitiativeTracker.Models
{
    public class Comments
    {
        public int id { get; set; }

        public string Comment { get; set; }
        public string? CommentsDateOnly { get; set; } = DateTime.Now.ToString();

        public string? CommentsTimeOnly { get; set; } = DateTime.Now.ToString();

        [DisplayName("TaskId")]
        public int Taskid { get; set; }

        [ForeignKey("IdOfIdea")]
        public Idea Idea { get; set; }

        [DisplayName("OwnerId")]

        public int userId { get; set; }

        [ForeignKey("Owner")]

        public User user { get; set; }




    }
}
