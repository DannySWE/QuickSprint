using Microsoft.EntityFrameworkCore;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class SprintProjectBoardRepository(SprintPlannerDbContext dbContext):GenericRepository<ProjectBoard>(dbContext)
    {
        public async Task<ProjectBoard> GetByPublicIdAsync(string publicId)
        {
            try
            {
                return await Context.ProjectBoards
                    .Include(p => p.ProductBacklog)
                    .Include(p => p.Sprints)
                    .FirstOrDefaultAsync(p => p.PublicId == publicId) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        public async Task<ProjectBoard> GetProjectBoardWithBacklogitemsAndSprintsAsync(string projectBoardPublicId)
        {
            return await Context
                .ProjectBoards
                .Include(p => p.ProductBacklog)
                .Include(p => p.Sprints)
                .FirstAsync(p => p.PublicId == projectBoardPublicId);
        }

        public async Task<List<ProjectBoard>> GetAllWithNavigationProperties()
        {
            return await Context
                .ProjectBoards
                .Include(pb => pb.ProductBacklog)
                .Include(s => s.Sprints)
                .ToListAsync();
        }
    }
}
