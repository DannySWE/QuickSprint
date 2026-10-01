using Microsoft.EntityFrameworkCore;
using SprintPlannerDashboard.Domain.Interfaces;
using SprintPlannerDashboard.Server.Data;

namespace SprintPlannerDashboard.Server.Repositories
{
    public class GenericRepository<T>(SprintPlannerDbContext dbContext) : IGenericRepository<T> where T : class
    {
        protected readonly SprintPlannerDbContext Context = dbContext;
        protected DbSet<T> _dbSet = dbContext.Set<T>();
        public async Task AddAsync(T entity)
        {
            if (entity is not null)
            {
                try
                {
                    _dbSet.Add(entity);
                    await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                    Console.WriteLine(databaseErrorMessage);
                    throw;
                }

            }
        }
        public async Task<T> GetAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id) ?? null!;
            }
            catch (Exception ex)
            {
                var databaseErrorMessage = ex.InnerException?.Message ?? ex.Message;
                Console.WriteLine(databaseErrorMessage);
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Context.SaveChangesAsync();
        }
        public void Delete(T entity)
        {
            if(entity is not null)
            {
                try
                {
                    _dbSet.Remove(entity);
                    Context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var errorMessaage = ex.InnerException?.Message ?? ex.Message;
                    Console.WriteLine(errorMessaage);
                    throw;
                }
            }
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
