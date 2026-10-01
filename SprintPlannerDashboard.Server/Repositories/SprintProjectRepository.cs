using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Domain.Interfaces;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class SprintProjectRepository(SprintPlannerDbContext dbContext):GenericRepository<Project>(dbContext)
    {
        public async Task<Project> GetByPublicIdAsync(string publicId)
        {
            try
            {
                return await Context.Projects.FirstOrDefaultAsync(p => p.PublicId == publicId) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }
        public async Task<Project> GetProjectWithProjectBoardsAsync(string projectId)
        {
            return await Context
                .Projects
                .Include(p => p.Boards)
                .FirstAsync(p => p.PublicId == projectId);
        }

        public async Task<List<Project>> GetAllWithNavigationProperties()
        {
            return await Context.Projects.Include(pb => pb.Boards).ToListAsync();
        }
    }
}
