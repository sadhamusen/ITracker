using InitiativeTracker.DataBaseConnection;
using ITracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributorController : ControllerBase
    {
        private readonly DatabaseAccess databaseAccess;

        public ContributorController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }
        [HttpGet]
        [Route("{taskId:int}")]

        public async Task<ActionResult<Contributor>> get([FromRoute] int taskId)
        {
            /*var result = databaseAccess.contributorTable.Find(taskId);

            result.idea=databaseAccess.ideaTable.Find(taskId);

            return Ok(result);*/

            var q = (from a in databaseAccess.contributorTable join b in databaseAccess.ideaTable on a.taskId equals b.Id
                    select new { a.Name,a.taskId,b.User.userName}).ToList();

            return Ok(q);

        }
        [HttpPost]
        public async Task<ActionResult<Contributor>> add(NewContributor newContributor)
        { Contributor contributor=new Contributor();
            contributor.Name= newContributor.Name;
            contributor.taskId = newContributor.id;
            contributor.idea= databaseAccess.ideaTable.Find(contributor.taskId);

            await databaseAccess.AddAsync(contributor);

            await databaseAccess.SaveChangesAsync();

            return Ok(contributor);
        }
    }
}
