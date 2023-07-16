using InitiativeTracker.DataBaseConnection;
using InitiativeTracker.Models;
using ITracker.Controllers;
using ITracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITracker.Services
{
    public class taskApproverService
    {
        private readonly DatabaseAccess databaseAccess;
        public taskApproverService(DatabaseAccess databaseAccess)
        {
            this.databaseAccess = databaseAccess;
        }
        public async Task<ActionResult<List<TaskApprovers>>> getAlltask()
        {
            return databaseAccess.taskApproversTable.ToList();

        }
        public async Task<ActionResult<TaskApprovers>> getapprover(RequestTaskApprover requestTaskApprover)
        {
            TaskApprovers taskApprovers = new TaskApprovers();
            taskApprovers.approver = await databaseAccess.usersTable.FindAsync(requestTaskApprover.approverId);
            taskApprovers.idea = await databaseAccess.ideaTable.FindAsync(requestTaskApprover.taskId);

            taskApprovers.approverId = requestTaskApprover.approverId;
            taskApprovers.taskId = requestTaskApprover.taskId;
            taskApprovers.status = requestTaskApprover.status;
            taskApprovers.feedback = requestTaskApprover.feedback;
            // 

            Idea idea = await databaseAccess.ideaTable.FindAsync(taskApprovers.taskId);


            idea.approverId = requestTaskApprover.approverId;


            await databaseAccess.taskApproversTable.AddAsync(taskApprovers);

            //  databaseAccess.ideaTable.Update(idea);

            databaseAccess.SaveChangesAsync();

            return taskApprovers;
        }

    }
}
