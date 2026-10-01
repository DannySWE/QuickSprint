using Microsoft.EntityFrameworkCore;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class SprintRepository(SprintPlannerDbContext dbContext):GenericRepository<Sprint>(dbContext)
    {

        public async Task<List<Sprint>> GetAllWithNavigationPropertiesAsync()
        {
            try
            {
                return await Context.Sprints
                    .Include(s => s.BacklogItems)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        public async Task<Sprint> GetByPublicIdAsync(string publicId)
        {
            try
            {
                return await Context.Sprints
                    // Updated to include BacklogItems based on Sprint.cs
                    .Include(s => s.BacklogItems)
                    .FirstOrDefaultAsync(s => s.PublicId == publicId) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        
        public async Task<Sprint> GetSprintWithDetailsAsync(string sprintPublicId)
        {
            return await Context
                .Sprints
                // Updated to include BacklogItems based on Sprint.cs
                .Include(s => s.BacklogItems)
                .FirstAsync(s => s.PublicId == sprintPublicId);
        }
    }
}