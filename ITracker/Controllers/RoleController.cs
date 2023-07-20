using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //[Authorize(Roles = "Admin,Approver,User")]
    public class RoleController : ControllerBase
    {

        private readonly DatabaseAccess databaseAccess;

        public RoleController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> get()
        {
            return await databaseAccess.rolesTable.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Role>> add(Role role)
        {
            await databaseAccess.rolesTable.AddAsync(role);

            await databaseAccess.SaveChangesAsync();


            return Ok(role);
        }
    }
}
