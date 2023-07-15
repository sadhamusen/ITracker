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
        public UserController(DatabaseAccess access, IConfiguration configuration)
        {
            userService = new userService(access, configuration);
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
            return Ok(user);
        }
        [HttpPost]
        [Route("/auth")]

        public async Task<ActionResult<User>> UserAuth(NewUser newUser)
        {
            // Find the value by the id of the customer.
            var user = await userService.authuser(newUser);

            return Ok(user.Value.Role);
        }

        [HttpPut]

        public async Task<ActionResult> edit(EditUser editUser)
        {
            return Ok(await userService.update(editUser));

        }




    }

}

