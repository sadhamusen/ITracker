using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ITracker.Services
{
    public class userService
    {
        private readonly DatabaseAccess databaseAccess;
        private readonly IConfiguration _configuration;

        public userService(DatabaseAccess databaseAccess, IConfiguration configuration)
        {
            this.databaseAccess = databaseAccess;
            _configuration = configuration;
        }
        public async Task<ActionResult<List<User>>> getAlluser()
        {
            return databaseAccess.usersTable.ToList();

        }
        public async Task<ActionResult<User>> postuser(NewUser newUser)
        {

            var Emailcheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            if (Emailcheck != null)
            {

                return null;
            }
            var Usercheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.userName.Equals(newUser.userName));
            User user = new User();
            if (Usercheck != null)
            {
                user.userName = "-1";
                return user;
            }
            string passwordHash
                       = BCrypt.Net.BCrypt.HashPassword(newUser.password);

            user.userName = newUser.userName;
            user.email = newUser.email;
            user.password = passwordHash;
            user.Role = databaseAccess.rolesTable.Find(3);

            user.rId = user.Role.id;


            databaseAccess.usersTable.AddAsync(user);

            user.Role = databaseAccess.rolesTable.Find(3);

            await databaseAccess.SaveChangesAsync();
            return (user);
        }
        public async Task<ActionResult<User>> authuser(NewUser newUser)
        {

            User user = new User();
            string msg = "user not Found";
            var userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            if (userSearch == null)
            {
                user.email = "usernotfound";
                return user;
            }
            User user1 = databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            var ans = user1.password;
            var answer = BCrypt.Net.BCrypt.Verify(newUser.password, ans);
            userSearch = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email) && answer);

            if (userSearch != null)
            {
                string token = createtoken(user1);
                user.Role = databaseAccess.rolesTable.Find(3);

                return user;
            }
            user.email = "UserandPasswordcomboiswrong";
            return user;
        }
        private string createtoken(User user)
        {
            var a = user.email;
            List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.Name,user.email)
                };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Appsettings:Token").Value!));



            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: creds
                );



            var jwt = new JwtSecurityTokenHandler().WriteToken(token);



            return jwt;
        }
        public async Task<ActionResult<User>> update(EditUser editUser)
        {
            User user = databaseAccess.usersTable.FirstOrDefault(x => x.id.Equals(editUser.id));
            user.Role = databaseAccess.rolesTable.Find(editUser.rId);
            user.rId = user.Role.id;

            databaseAccess.usersTable.Update(user);
            await databaseAccess.SaveChangesAsync();


            return user;
        }
    }
}
