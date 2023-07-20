using Azure.Core;
using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ITracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        // private readonly DatabaseAccess databaseAccess;
        public userService userService;
        public readonly DatabaseAccess databaseAccess;

        private readonly IConfiguration _configuration;
        public UserController(DatabaseAccess databaseAccess, IConfiguration configuration)
        {
            userService = new userService(databaseAccess, configuration);
            this.databaseAccess = databaseAccess;

            _configuration = configuration;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<IActionResult> get()
        {
            return Ok(await userService.getAlluser());
        }
        [HttpPut]
        [Route("Rating")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> rating(Updaterating updaterating)
        {
             User user = await databaseAccess.usersTable.FirstOrDefaultAsync(x => x.id == updaterating.id);
            user.rating=updaterating.rating;
            databaseAccess.usersTable.Update(user);
            await databaseAccess.SaveChangesAsync();
            return Ok(user);
        }
        [HttpPost]
        //[Authorize(Roles = "Admin,Approver,User")]

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

        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<User>> UserAuth(AuthUser authUser)
        {
            // Find the value by the id of the customer.
            User user = new User();
            string msg = "user not Found";
            var userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(authUser.email));
            if (userSearch == null)
            {
                return BadRequest("user not found");
            }
            User user1 = databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(authUser.email));
            var ans = user1.password;
            var answer = BCrypt.Net.BCrypt.Verify(authUser.password, ans);
            userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(authUser.email) && answer);

            if (userSearch != null)
            {
                string token = createtoken(user1);
                user.Role = databaseAccess.rolesTable.Find(3);
                user.JWT = token;
                var query = (from c in databaseAccess.usersTable
                            join d in databaseAccess.usersTable on c.email equals authUser.email where c.email == authUser.email
                            select new { role = c.Role.type, id = c.id, email = c.email, username = c.userName, jwt = token }).FirstOrDefault();
                return Ok(query);
            }
            return BadRequest("User and Password combo is wrong");
        }
        private string createtoken(User user)
        {
            var a = user.email;
            List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.email),
                    new Claim(ClaimTypes.Role,databaseAccess.rolesTable.Find(user.rId).type)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPut]

        public async Task<ActionResult> edit(EditUser editUser)
        {
            return Ok(await userService.update(editUser));

        }

        [HttpPut]
        [Route("updatedetails/{id:int}")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<User>> updateuserdetails([FromRoute] int id, updateuserdetails updateuserdetails)
        {
            if (updateuserdetails != null)
            {
          User user = databaseAccess.usersTable.FirstOrDefault(x => x.id.Equals(id));
                if (user != null)
                {
                    user.secondary_email = updateuserdetails.secondary_email;
                    user.dob = updateuserdetails.dob;
                    user.bio = updateuserdetails.bio;
                    user.mobile_number = updateuserdetails.mobile_number;
                    user.image = updateuserdetails.image;
                    user.linkedin = updateuserdetails.linkedin;
                    user.instagram = updateuserdetails.instagram;
                    user.blood_grop = updateuserdetails.blood_grop;
                    databaseAccess.usersTable.Update(user);
                    await databaseAccess.SaveChangesAsync();

                }

                return user;

            }
            else
            {

                return BadRequest("Not Available");

            }

        }

        [HttpGet]
        [Route("{userid}")]
        //[Authorize(Roles = "Admin,Approver,User")]
        public async Task<ActionResult<User>> getuserdetails([FromRoute] int userid) {
            User user = await databaseAccess.usersTable.Include(x=>x.Role).FirstOrDefaultAsync(x => x.id == userid);
            return user;
        }
    }

}

