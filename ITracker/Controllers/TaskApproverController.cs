using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskApproverController : ControllerBase
    {
        private readonly DatabaseAccess databaseAccess;

        public TaskApproverController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskApprovers>>> Get()
        {
            return await databaseAccess.taskApproversTable.ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> addApprover(RequestTaskApprover requestTaskApprover)
        {
            TaskApprovers taskApprovers = new TaskApprovers();
             taskApprovers.Approver=await databaseAccess.approversTable.FindAsync(requestTaskApprover.approverId);
            taskApprovers.idea = await databaseAccess.ideaTable.FindAsync(requestTaskApprover.taskId);

            taskApprovers.approverId = requestTaskApprover.approverId;
            taskApprovers.taskId=requestTaskApprover.taskId;

            // 

            Idea idea=await databaseAccess.ideaTable.FindAsync(taskApprovers.taskId);

            idea.Approver = taskApprovers.Approver;
            idea.approverId=requestTaskApprover.approverId;


           await databaseAccess.taskApproversTable.AddAsync(taskApprovers);

           //  databaseAccess.ideaTable.Update(idea);

            databaseAccess.SaveChangesAsync();

            return Ok(taskApprovers);

        }
    }
}
