using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITracker.Services
{
    public class ideaService
    {
        public readonly DatabaseAccess databaseAccess;
        public ideaService(DatabaseAccess databaseAccess)
        {
            databaseAccess = databaseAccess;
        }
        public async Task<ActionResult<List<Idea>>> getall() {
           
                var allData = databaseAccess.ideaTable.Where(x => x.isDelete == 0);

                //return await databaseAccess.ideaTable.ToListAsync();
                return null;
             }
    }
}
