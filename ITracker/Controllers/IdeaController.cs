using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : ControllerBase
    {
        public ideaService ideaService;
        private readonly DatabaseAccess databaseAccess;

        public IdeaController(DatabaseAccess databaseAccess)
        {
            ideaService = new ideaService(databaseAccess);
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> get()
        {
            var allData = databaseAccess.ideaTable.Where(x => x.isDelete == 0);
            var query = (from c in databaseAccess.ideaTable
                         join b in databaseAccess.usersTable on
                         c.approverId equals b.id
                         join d in databaseAccess.contributorTable on c.Id equals d.id
                         select new { contributor = d.Name, approvername = b.userName ,title=c.title,shortdescription=c.shortDescription,
                         longdescription=c.longDescription,status=c.status,like=c.like
                         }).ToList();
            //var query= await databaseAccess.ideaTable.ToListAsync();
            //query.ForEach(x =>
            //{
            //    x.contributors = databaseAccess.contributorTable.Where(y => y.idea.Id == x.Id).ToList();
            //});
            //return await databaseAccess.ideaTable.ToListAsync();
            return Ok(query);
        }
        [HttpGet]
        [Route("newidea")]
        public async Task<IActionResult> newidea()
        {
            var new_idea = databaseAccess.ideaTable.Where(x => x.status == "new idea");
            return Ok(new_idea);
        }
        [HttpGet]
        [Route("todo")]
        public async Task<IActionResult> getTodo()
        {
            var todo = databaseAccess.ideaTable.Where(x => x.status == "To do");
            return Ok(todo);
        }
        [HttpGet]
        [Route("inprogess")]
        public async Task<IActionResult> inprogress()
        {
            var inprogress = databaseAccess.ideaTable.Where(x => x.status == "Inprogress");
            return Ok(inprogress);
        }
        [HttpGet]
        [Route("inreview")]
        public async Task<IActionResult> inreview()
        {
            var inreview = databaseAccess.ideaTable.Where(x => x.status == "Inreview");
            return Ok(inreview);
        }
        [HttpGet]
        [Route("done")]
        public async Task<IActionResult> donw()
        {
            var inreview = databaseAccess.ideaTable.Where(x => x.status == "Done");
            return Ok(inreview);
        }
        [HttpGet]
        [Route("highestlike")]
        public async Task<IActionResult> highestlike()
        {
            var query = databaseAccess.ideaTable.OrderByDescending(p => p.like).FirstOrDefault();

            return Ok(new { id = query.Id });

        }
        [HttpGet]
        [Route("{taskid}")]
        public async Task<IActionResult> gettaskbyid([FromRoute] int taskid)
        {
            var query = databaseAccess.ideaTable.FirstOrDefault(x => x.Id.Equals(taskid));

            return Ok(query);

        }
        [HttpPost]
        public async Task<ActionResult<Idea>> add(NewIdea newIdea)
        {

            Idea idea = new Idea();
            idea.title = newIdea.Title;
            idea.shortDescription = newIdea.Short_Description;
            idea.longDescription = newIdea.Long_Description;
            idea.ideaCreatedDate= DateTime.Now.ToShortDateString();
            idea.status = newIdea.Status;
            idea.idOfOwner = newIdea.idOfOwner;
            //  idea.approverId = newIdea.approverId;

            // idea.Approver= databaseAccess.approversTable.FirstOrDefault(x=>x.id==newIdea.approverId);

            idea.User = databaseAccess.usersTable.FirstOrDefault(x => x.id == newIdea.idOfOwner);

            await databaseAccess.ideaTable.AddAsync(idea);

            await databaseAccess.SaveChangesAsync();


            return Ok(idea);
        }
        [HttpPut]
        public async Task<IActionResult> updateidea(Updateidea updateidea)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == updateidea.Id);
            idea.title = updateidea.title;
            idea.shortDescription = updateidea.shortDescription;
            idea.longDescription = updateidea.longDescription;
            idea.status = updateidea.status;
            idea.signOff = updateidea.signOff;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);

        }
        [HttpPut]
        [Route("{taskId:int}")]
        public async Task<IActionResult> updatedate([FromRoute] int taskId)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == taskId);
            idea.startDate = DateTime.Now.ToShortDateString();
            idea.endDate = DateTime.Now.ToShortTimeString();

            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);
        }
        [HttpPut]
        [Route("like/{liketaskId:int}")]
        public async Task<IActionResult> like([FromRoute] int liketaskId)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == liketaskId);
            idea.like = idea.like + 1;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);
        }
        [HttpPut]
        [Route("delete/{deletetaskId:int}")]
        public async Task<IActionResult> delete([FromRoute] int deletetaskId)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == deletetaskId);
            idea.isDelete = 1;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);
        }


    }
}
