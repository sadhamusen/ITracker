﻿using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            return await databaseAccess.ideaTable.ToListAsync();
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
            idea.approverId = newIdea.approverId;

            idea.Approver= databaseAccess.approversTable.FirstOrDefault(x=>x.id==newIdea.approverId);

            idea.User = databaseAccess.usersTable.FirstOrDefault(x => x.id == newIdea.idOfOwner);






            await databaseAccess.ideaTable.AddAsync(idea);

            await databaseAccess.SaveChangesAsync();


            return Ok(idea);
        }
    }
}
