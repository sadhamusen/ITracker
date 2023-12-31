﻿using InitiativeTracker.DataBaseConnection;
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
        //[Authorize(Roles ="Admin,Approver,User")]
        public async Task<ActionResult<IEnumerable<Comments>>> get()
        {
            // var result= await databaseAccess.commentsTable.ToListAsync();

            var q = (from c in databaseAccess.commentsTable
                     join b in databaseAccess.ideaTable on c.Taskid equals b.Id
                     select new { Task_id = c.Taskid, Comments = c.Comment, User = c.user.userName, Role = c.user.Role.type, commentsTime = c.CommentsTimeOnly,commentsDate=c.CommentsDateOnly }).ToList();

            return Ok(q);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Approver,User")]
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
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<IEnumerable<Comments>>> getcomments([FromRoute] int id) {
            var comments=  await databaseAccess.commentsTable.Include(x=>x.user).Where(c => c.Taskid == id).ToListAsync();
                return Ok(comments.Select( x=> new
                {
                    TaskId = x.Taskid,
                    UserId=x.userId,
                    UserName=x.user.userName,
                    date=x.CommentsDateOnly,
                    comment=x.Comment,

                }));
        }
    }
}
