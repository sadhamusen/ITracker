using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseAccess databaseAccess;

        public UserController(DatabaseAccess access)
        {
            databaseAccess = access;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> get()
        {  
            return await databaseAccess.usersTable.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<User>> add(NewUser newUser)
        {
            var Emailcheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            if (Emailcheck != null)
            {
                return BadRequest("Email Already Exist");
            }
            var Usercheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.userName.Equals(newUser.userName));
            if (Usercheck != null)
            {
                return BadRequest("User Exist");
            }
            var password = newUser.password;
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            User user = new User();
            user.userName = newUser.userName;
            user.email = newUser.email;
            user.password = encodedData;
            user.Role = databaseAccess.rolesTable.Find(3);

            user.rId = user.Role.id;


            await databaseAccess.usersTable.AddAsync(user);

            user.Role=databaseAccess.rolesTable.Find(3);

            await databaseAccess.SaveChangesAsync();


            return Ok(user);
        }
        [HttpPost]
        [Route("/auth")]
        public async Task<IActionResult> UserAuth(NewUser newUser)
        {
            // Find the value by the id of the customer.

            User user = new User();
            string msg = "user not Found";
            var userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            if (userSearch == null)
            {
                return BadRequest(msg);
            }
            var password = newUser.password;
            byte[] encData_byte = new byte[password.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            password = encodedData;

            userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email) && x.password.Equals(password));

            if (userSearch != null)
            {

                user.Role = databaseAccess.rolesTable.Find(3);

                return Ok(new { user.Role });
            }
            msg = "User and Password combo is wrong";
            return BadRequest(msg);
        }

        [HttpPut]
        public async Task<ActionResult<User>> edituser(EditUser  editUser)
        {
            User user = databaseAccess.usersTable.FirstOrDefault(x => x.id.Equals(editUser.id));
            user.Role=databaseAccess.rolesTable.Find(editUser.rId);
            user.rId = user.Role.id;

            databaseAccess.usersTable.Update(user);
            await databaseAccess.SaveChangesAsync();


            return Ok(user);

        }




    }
    
}

