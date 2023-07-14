
using InitiativeTracker.Models;
using ITracker.Models;
using Microsoft.EntityFrameworkCore;

namespace InitiativeTracker.DataBaseConnection
{
    public class DatabaseAccess: DbContext
    {
        public DatabaseAccess(DbContextOptions<DatabaseAccess> options) : base(options)
        {
        }

        public DbSet<User> usersTable { get; set; }
        public DbSet<Idea> ideaTable { get; set; }
        public DbSet<Role> rolesTable { get; set; }
        public DbSet<Comments> commentsTable { get; set; }
        public DbSet<Approver> approversTable { get; set; }

        public DbSet<Contributor> contributorTable { get; set; }

        public DbSet<TaskApprovers> taskApproversTable { get; set;}
    }
}
