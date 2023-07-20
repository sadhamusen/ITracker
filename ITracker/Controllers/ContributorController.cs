using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Models;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<Contributor>> get([FromRoute] int taskId)
        {
            /*var result = databaseAccess.contributorTable.Find(taskId);

            result.idea=databaseAccess.ideaTable.Find(taskId);

            return Ok(result);*/

            var query = (from a in databaseAccess.contributorTable
                         join b in databaseAccess.ideaTable on a.ideaId equals b.Id
                         where a.ideaId == taskId
                         select new { a.Name, a.taskId, b.User.userName }).ToList();

            return Ok(query);

        }
        [HttpPost]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<Idea>> add(NewContributor newcontributor)
        {
            //contributor contributor = new contributor();
            //contributor.name = newcontributor.;
            //contributor.taskid = newcontributor.taskid;
            //contributor.idea = databaseaccess.ideatable.find(contributor.taskid);
            //foreach (var newcontributor in c)
            //    await databaseaccess.addasync(contributor);

            //await databaseaccess.savechangesasync();

            //return ok(contributor);

            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x=>x.Id==newcontributor.taskId);
            if (newcontributor.contributorid.Count!=0)
            {
                foreach (var id in newcontributor.contributorid)
                {
                    Contributor contributor = new Contributor();
                    User user = await databaseAccess.usersTable.FindAsync(id);
                    contributor.UserId = user.id;
                    contributor.Name = user.userName;
                    contributor.taskId = 100;

                    idea.contributors.Add(contributor);

                    await databaseAccess.contributorTable.AddAsync(contributor);


                }
                await databaseAccess.SaveChangesAsync();
            }
            return Ok(idea);
        }
    }
}
