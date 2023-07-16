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
            var approved = await this.databaseAccess.taskApproversTable.Where(x => x.status.Equals("accepted")).ToListAsync();
            return Ok(approved);
        }
        [HttpGet]
        [Route("rejected")]
        public async Task<IActionResult> getrejected()
        {
            var approved = await this.databaseAccess.taskApproversTable.Where(x => x.status.Equals("rejected")).ToListAsync();
            return Ok(approved);
        }
        [HttpPost]
        public async Task<IActionResult> addApprover(RequestTaskApprover requestTaskApprover)
        {

            var task = await taskApproverService.getapprover(requestTaskApprover);
            return Ok(task.Value);

        }
    }
}
