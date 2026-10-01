using Microsoft.EntityFrameworkCore;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class ProjectTaskRepository(SprintPlannerDbContext dbContext):GenericRepository<ProjectTask>(dbContext)
    {
        public async Task<ProjectTask> GetByIdAsync(Guid taskId)
        {
            try
            {
                return await Context.ProjectTasks
                    .FirstOrDefaultAsync(t => t.Id == taskId) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        
        public async Task<List<ProjectTask>> GetTasksByBacklogItemIdAsync(int backlogItemId)
        {
            return await Context
                .ProjectTasks
                .Where(t => t.BacklogItemId == backlogItemId)
                .ToListAsync();
        }
    }
}