namespace ITracker.Controllers
{
    public class RequestTaskApprover
    {
        public int approverId { get; set; }
        public int taskId { get; set; }
        public string status { get; set; }
        public string? feedback { get; set; }
    }
}