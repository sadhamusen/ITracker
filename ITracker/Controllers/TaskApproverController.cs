using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Models;
using ITracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
//    [Authorize(Roles = "Admin,Approver")]
    public class TaskApproverController : ControllerBase
    {
        public taskApproverService taskApproverService;
        private readonly DatabaseAccess databaseAccess;
        public TaskApproverController(DatabaseAccess databaseAccess)
        {
            taskApproverService = new taskApproverService(databaseAccess);
            this.databaseAccess = databaseAccess;
        }
        //private readonly DatabaseAccess databaseAccess;

        //public TaskApproverController(DatabaseAccess databaseAccess)
        //{
        //    this.databaseAccess = databaseAccess;
        //}

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await taskApproverService.getAlltask());
        }
        [HttpGet]
        [Route("approved")]
        public async Task<IActionResult> getTodo()
        {
            var approved = await this.databaseAccess.taskApproversTable.Where(x => x.status.Equals("Accepted")).ToListAsync();
            return Ok(approved);
        }
        [HttpGet]
        [Route("rejected")]
        public async Task<IActionResult> getrejected()
        {
            var approved = await this.databaseAccess.taskApproversTable.Where(x => x.status.Equals("Rejected")).ToListAsync();
            return Ok(approved);
        }
        [HttpPost]
        public async Task<IActionResult> addApprover(RequestTaskApprover requestTaskApprover)
        {

            var task = await taskApproverService.getapprover(requestTaskApprover);
            if (task.Value.status == "user idea and approver can't be same")
            {
                return BadRequest("idea posted and idea approved person can't be same");
            }
            return Ok(task.Value);

        }
        [HttpPut("TaskApproved")]
        public async Task<IActionResult> changeStatus(RequestTaskApprover requestTaskApprover)
        {
            TaskApprovers taskApprovers = await databaseAccess.taskApproversTable.FirstOrDefaultAsync(x=>x.idea.Id==requestTaskApprover.taskId);

            var query = databaseAccess.taskApproversTable.Select(x => x.idea.Id).ToList();
            

            taskApprovers.status = requestTaskApprover.status;
            taskApprovers.feedback = requestTaskApprover.feedback;

            Idea idea = await databaseAccess.ideaTable.FirstOrDefaultAsync(x => x.Id == requestTaskApprover.taskId);
            idea.signOff= DateTime.Now.ToShortDateString();
            idea.endDate=DateTime.Now.ToShortDateString();
            idea.status= requestTaskApprover.status;    

             databaseAccess.taskApproversTable.Update(taskApprovers);
            await databaseAccess.SaveChangesAsync();
            return Ok(taskApprovers);

            
        }

    }
}
