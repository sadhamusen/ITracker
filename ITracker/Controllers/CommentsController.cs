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
    public class CommentsController : ControllerBase
    {
        private readonly DatabaseAccess databaseAccess;

        public CommentsController(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comments>>> get()
        {
            // var result= await databaseAccess.commentsTable.ToListAsync();

            var q = (from c in databaseAccess.commentsTable
                     join b in databaseAccess.ideaTable on c.Taskid equals b.Id
                     select new { Task_id = c.Taskid, Comments = c.Comment, User = c.user.userName, Role = c.user.Role.type, commentsTime = c.CommentsTimeOnly,commentsDate=c.CommentsDateOnly }).ToList();

            return Ok(q);
        }

        [HttpPost]
        public async Task<ActionResult<Comments>> add(NewComments newComments)
        {

            Comments comments = new Comments();

            comments.userId = newComments.UserId;
            comments.Taskid = newComments.taskId;
            comments.Comment = newComments.Comment;
            comments.CommentsTimeOnly=  DateTime.Now.ToShortTimeString();
            comments.CommentsDateOnly = DateTime.Now.ToShortDateString();
            comments.user = databaseAccess.usersTable.Find(newComments.UserId);

            comments.Idea = databaseAccess.ideaTable.Find(newComments.taskId);

            await databaseAccess.commentsTable.AddAsync(comments);

            await databaseAccess.SaveChangesAsync();

            return Ok(newComments);

        }
    }
}
