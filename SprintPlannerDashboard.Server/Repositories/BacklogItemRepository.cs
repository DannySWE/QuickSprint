using Microsoft.EntityFrameworkCore;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class BacklogItemRepository(SprintPlannerDbContext dbContext):GenericRepository<BacklogItem>(dbContext)
    {
        public async Task<BacklogItem> GetByPublicIdAsync(string publicId)
        {
            try
            {
                return await Context.BacklogItems
                    // Including related entities: Sprint and Tasks
                    .Include(b => b.Sprint)
                    .Include(b => b.Tasks)
                    .FirstOrDefaultAsync(b => b.PublicId == publicId) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        
        public async Task<BacklogItem> GetItemWithDetailsAsync(string backlogItemPublicId)
        {
            return await Context
                .BacklogItems
                // Including related entities: Sprint and Tasks
                .Include(b => b.Sprint)
                .Include(b => b.Tasks)
                .FirstAsync(b => b.PublicId == backlogItemPublicId);
        }
    }
}