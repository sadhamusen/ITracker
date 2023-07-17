using Azure.Core;
using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly DatabaseAccess databaseAccess;
        public userService userService;
        public readonly DatabaseAccess databaseAccess;
        public UserController(DatabaseAccess databaseAccess, IConfiguration configuration)
        {
            userService = new userService(databaseAccess, configuration);
            this.databaseAccess = databaseAccess;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            return Ok(await userService.getAlluser());
        }

        [HttpPost]
        public async Task<ActionResult> add(NewUser newUser)
        {
            var user = await userService.postuser(newUser);
            if (user.Value.userName.Equals("0")){
                return BadRequest("Email id already exist");
            }
            if (user.Value.userName.Equals("-1"))
            {
                return BadRequest("username already taken");
            }
            return Ok(user);
        }
        [HttpPost]
        [Route("/auth")]

        public async Task<ActionResult<User>> UserAuth(AuthUser authUser)
        {
            // Find the value by the id of the customer.
            var user = await userService.authuser(authUser);
            if (user.Value.email == ("user not found")){
               return  BadRequest("user not Found");
            }
            if (user.Value.email == ("User and Password combo is wrong"))
            {
                return BadRequest("User and Password combo is wrong");
            }
            var query = (from c in this.databaseAccess.usersTable
                         join d in this.databaseAccess.rolesTable on c.rId equals d.id
                         where c.email == authUser.email
                         select new { id = c.id, email = c.email, role = d.type }).ToList();

            return Ok(query);
        }

        [HttpPut]

        public async Task<ActionResult> edit(EditUser editUser)
        {
            return Ok(await userService.update(editUser));

        }




    }

}

