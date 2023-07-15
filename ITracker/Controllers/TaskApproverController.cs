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
        public TaskApproverController(DatabaseAccess access)
        {
            taskApproverService = new taskApproverService(access);
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

        [HttpPost]
        public async Task<IActionResult> addApprover(RequestTaskApprover requestTaskApprover)
        {

            var task = await taskApproverService.getapprover(requestTaskApprover);
            return Ok(task.Value);

        }
    }
}
