using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SprintPlannerDashboard.Domain.Entites;

namespace SprintPlannerDashboard.Server.Data
{
    public class SprintPlannerUserDbContext(DbContextOptions<SprintPlannerUserDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<SprintPlannerUser> SprintPlannerUsers { get; set; }
    }
}
