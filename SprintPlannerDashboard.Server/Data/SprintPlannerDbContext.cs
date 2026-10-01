using Microsoft.EntityFrameworkCore;
using PrintPlannerDashboard.Domain.Entites;

namespace SprintPlannerDashboard.Server.Data
{
    public class SprintPlannerDbContext(DbContextOptions<SprintPlannerDbContext> options) : DbContext(options)
    {
        public DbSet<BacklogItem> BacklogItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBoard> ProjectBoards { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<Sprint> Sprints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
