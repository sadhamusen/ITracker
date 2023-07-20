using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Models;
using ITracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Xml.Linq;

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
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<IEnumerable<Idea>>> get()
        {
            var allData = databaseAccess.ideaTable.Where(x => x.isDelete == 0);
            var query = (from c in databaseAccess.ideaTable
                         join b in databaseAccess.usersTable on
                         c.approverId equals b.id
                         join d in databaseAccess.contributorTable on c.Id equals d.id
                         select new { contributor = d.Name, approvername = b.userName, title = c.title, shortdescription = c.shortDescription,
                             longdescription = c.longDescription, status = c.status, like = c.like
                         }).ToList();
            //var query= await databaseAccess.ideaTable.ToListAsync();
            //query.ForEach(x =>
            //{
            //    x.contributors = databaseAccess.contributorTable.Where(y => y.idea.Id == x.Id).ToList();
            //});
            //return await databaseAccess.ideaTable.ToListAsync();

            var result= await databaseAccess.ideaTable.Include(u=>u.User).Include(u=>u.contributors).Where(x => x.isDelete==0).ToListAsync();
            return Ok(result.Select(x => new
            {
                Id=x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff=x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            })) ;
        }
        [HttpGet]
        [Route("newidea")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> newidea()
        {
            var new_idea = await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 && x.status == "New Idea").ToListAsync();
            return Ok(new_idea.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("todo")]
       // [Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> getTodo()
        {
            var todo = await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 &&  x.status == "To Do").ToListAsync();
            return Ok(todo.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("inprogess")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> inprogress()
        {
            var inprogress =await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 && x.status == "In Progress").ToListAsync();
            return Ok(inprogress.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("inreview")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> inreview()
        {
            var inreview =await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 && x.status == "In Review").ToListAsync();
            return Ok(inreview.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("done")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> done()
        {
            var done =await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 && x.status == "Done").ToListAsync();
            return Ok(done.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("highestlike")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> highestlike()
        {
            var query = databaseAccess.ideaTable.Include(x=>x.User).OrderByDescending(p => p.like).FirstOrDefault();

            return Ok(new { likes=query.like,id = query.Id,title=query.title,owner=query.User.userName });

        }
        [HttpGet]
        [Route("{taskid}")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<IEnumerable<Idea>>> gettaskbyid([FromRoute] int taskid)
        {
            var query = databaseAccess.ideaTable.Include(x=>x.contributors).Include(z=>z.User).Where(x => x.Id.Equals(taskid) ).ToList();

            return Ok(query.Select(x => new
            {
                id=x.Id,
                title=x.title,
                shortDescription=x.shortDescription,
                longDescription=x.longDescription,
                stratDate=x.startDate,
                endDate=x.endDate,
                status=x.status,
                ideacreatedtime=x.ideaCreatedDate,
                user=x.User.userName,
                contributors=x.contributors,
            }));

        }
        [HttpPut]
        [Route("update/id")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> updateresult(UpdateStatus updateStatus) {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == updateStatus.id);
            idea.status=updateStatus.status;

            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<Idea>> add(NewIdea newIdea)
        {

            Idea idea = new Idea();
            idea.title = newIdea.Title;
            idea.shortDescription = newIdea.Short_Description;
            idea.longDescription = newIdea.Long_Description;
            idea.ideaCreatedDate = DateTime.Now.ToShortDateString();
            idea.status = newIdea.Status;
            idea.idOfOwner = newIdea.idOfOwner;
            //  idea.approverId = newIdea.approverId;

            // idea.Approver= databaseAccess.approversTable.FirstOrDefault(x=>x.id==newIdea.approverId);

            idea.User = await databaseAccess.usersTable.FindAsync(newIdea.idOfOwner);



            if (newIdea.IdOfContributors.Count()==1&&newIdea.IdOfContributors.IndexOf(0)==0)
            {

            }
            else if (newIdea.IdOfContributors.Count() != 0)
            {
                foreach (var id in newIdea.IdOfContributors)
                {
                    Contributor contributor = new Contributor();
                    User user = await databaseAccess.usersTable.FindAsync(id);
                    contributor.UserId = user.id;
                    contributor.Name = user.userName;
                    contributor.taskId = 100;

                    idea.contributors.Add(contributor);

                    await databaseAccess.contributorTable.AddAsync(contributor);


                }
            }

            await databaseAccess.ideaTable.AddAsync(idea);

            await databaseAccess.SaveChangesAsync();


            return Ok(idea);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin,Approver,User")]
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
        [HttpGet]
        [Route("excel")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<IEnumerable<Idea>>> excelsheet() {
            var user = databaseAccess.ideaTable.Include(p=>p.User).Where(x => x.isDelete == 0).ToList();
            return Ok(user.Select(y => new { taskid=y.Id, username = y.User.userName,title=y.title, startdate =y.startDate,enddate=y.endDate,signoff=y.signOff,like=y.like}));
        }
        //[HttpPut]
        //[Route("{taskId:int}")]
        //public async Task<IActionResult> updatedate([FromRoute] int taskId)
        //{
        //    Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == taskId);
        //    idea.startDate = DateTime.Now.ToShortDateString();
        //    idea.endDate = DateTime.Now.ToShortTimeString();

        //    databaseAccess.ideaTable.Update(idea);
        //    await databaseAccess.SaveChangesAsync();
        //    return Ok(idea);
        //}
        [HttpPut]
        [Route("like/{liketaskId:int}")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> like([FromRoute] int liketaskId)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == liketaskId);
            idea.like = idea.like + 1;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea.like);
        }

        //[Authorize(Roles = "Admin,User")]
        [HttpPut]
        //[Authorize(Roles = "Admin,Approver,User")]
        [Route("delete/{deletetaskId:int}")]

        public async Task<IActionResult> delete([FromRoute] int deletetaskId)
        {
            Idea idea = databaseAccess.ideaTable.FirstOrDefault(x => x.Id == deletetaskId);
            idea.isDelete = 1;
            databaseAccess.ideaTable.Update(idea);
            await databaseAccess.SaveChangesAsync();
            return Ok(idea);
        }

        [HttpGet]
        [Route("get/{getuserstask}")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult> gettaskofuser([FromRoute]int getuserstask) {
            //Idea idea = new Idea();
            //var query = databaseAccess.ideaTable.Include(x=>x.contributors).Where(x => x.idOfOwner == getuserstask);
            //return Ok(query);

            var result = await databaseAccess.ideaTable.Include(u => u.User).Include(u => u.contributors).Where(x => x.isDelete == 0 && x.idOfOwner == getuserstask).ToListAsync();
            return Ok(result.Select(x => new
            {
                Id = x.Id,
                Name = x.User.userName,
                Title = x.title,
                Shortdescription = x.shortDescription,
                Longdescription = x.longDescription,
                Status = x.status,
                like = x.like,
                signoff = x.signOff,
                CreatedTime = x.ideaCreatedDate,

                Contributor = x.contributors.Where(z => z.ideaId == x.Id).Select(y => new
                {
                    Name = y.Name,
                }).ToList()
            }));
        }
        [HttpGet]
        [Route("Numberofpost")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult> Numberofpost() {
            var idea = databaseAccess.ideaTable.Where(x => x.isDelete == 0).Count();
            var query = databaseAccess.usersTable.OrderByDescending(p => p.rating).FirstOrDefault();
            var highestrating = new { name = query.userName, rating = query.rating };
            var GroupedTags = databaseAccess.ideaTable.Where(x=>x.isDelete==0).GroupBy(c => c.idOfOwner)
                .Select(x=>new
                {
                    Id = x.First().Id,
                    title=x.First().title,
                    Count=x.ToList().Count(),
                    name=x.First().User.userName

                }).OrderByDescending(p=>p.Count).Take(1);
            var newidea = databaseAccess.ideaTable.Where(x => x.isDelete == 0).GroupBy(c => c.status)
                .Select(x => new
                {
                    Count=x.ToList().Count(),
                    Status=x.First().status,
                });
           
            return Ok(new {highestrating=highestrating, noofpost = idea,groupby=GroupedTags,newidea=newidea });
        }
    }
}
