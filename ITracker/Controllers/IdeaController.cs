using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdeaController : ControllerBase
    {

        private readonly DatabaseAccess databaseAccess;

        public IdeaController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> get()
        {
            var allData = databaseAccess.ideaTable.Where(x => x.isDelete == 0);

            //return await databaseAccess.ideaTable.ToListAsync();
            return Ok(allData);
        }
        [HttpGet]
        [Route("newidea")]
        public async Task<IActionResult> newidea() {
            var new_idea = databaseAccess.ideaTable.Where(x=>x.status== "new idea");
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
        [Route("highestlike")]
        public async Task<IActionResult> highestlike() {
            var query = databaseAccess.ideaTable.OrderByDescending(p => p.like).FirstOrDefault();

            return Ok(new { id=query.Id });

        }
        [HttpPost]
        public async Task<ActionResult<Idea>> add(NewIdea newIdea)
        {

            Idea idea = new Idea();
            idea.title = newIdea.Title;
            idea.shortDescription = newIdea.Short_Description;
            idea.longDescription= newIdea.Long_Description;
          
            idea.status = "Idea Proposed";
            idea.idOfOwner = newIdea.idOfOwner;
          //  idea.approverId = newIdea.approverId;

           // idea.Approver= databaseAccess.approversTable.FirstOrDefault(x=>x.id==newIdea.approverId);

            idea.User = databaseAccess.usersTable.FirstOrDefault(x => x.id == newIdea.idOfOwner);

            await databaseAccess.ideaTable.AddAsync(idea);

            await databaseAccess.SaveChangesAsync();


            return Ok(idea);
        }
        [HttpPut]
        public async Task<IActionResult> updateidea(Updateidea updateidea) {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == updateidea.Id);
            idea.title = updateidea.title;
            idea.shortDescription=updateidea.shortDescription;
            idea.longDescription=updateidea.longDescription;
            idea.status = updateidea.status;
            idea.signOff = updateidea.signOff;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);

        }
        [HttpPut]
        [Route("{taskId:int}")]
        public async Task<IActionResult> updatedate([FromRoute]int taskId)
        {
            Idea idea =databaseAccess.ideaTable.FirstOrDefault(x=>x.Id==taskId);
            idea.startDate =   DateTime.Now.ToShortDateString();
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
