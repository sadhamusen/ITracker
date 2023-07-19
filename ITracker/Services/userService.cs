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
            User user = new User();
            var Emailcheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.email.Equals(newUser.email));
            if (Emailcheck != null)
            {
                user.userName = "0";
                return user;
            }
            var Usercheck = this.databaseAccess.usersTable.FirstOrDefault(x => x.userName.Equals(newUser.userName));

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
            user.created_time = DateTime.Now.ToShortDateString();
            user.rId = user.Role.id;
            user.rating=newUser.rating;
            databaseAccess.usersTable.AddAsync(user);

            user.Role = databaseAccess.rolesTable.Find(3);

            await databaseAccess.SaveChangesAsync();
            return (user);
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
