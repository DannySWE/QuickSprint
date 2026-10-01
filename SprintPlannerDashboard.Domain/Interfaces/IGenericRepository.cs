using PrintPlannerDashboard.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Domain.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task<T> GetAsync(int Id);
        public void Delete(T entity);
    }
}
