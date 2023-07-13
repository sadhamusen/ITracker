using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApproverController : ControllerBase
    {
        private readonly DatabaseAccess databaseAccess;

        public ApproverController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Approver>>> get()
        {
            return await databaseAccess.approversTable.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Approver>> add(Approver newApprover)
        {
            await databaseAccess.approversTable.AddAsync(newApprover);
            await databaseAccess.SaveChangesAsync();
            return Ok(newApprover);
        }


        }
}
