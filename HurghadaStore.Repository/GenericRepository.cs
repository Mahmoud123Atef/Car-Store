using HurghadaStore.Core.Entities;
using HurghadaStore.Core.Repositories.Contract;
using HurghadaStore.Core.Specifications;
using HurghadaStore.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext) // Ask CLR for creating object from DbContext Implicitly.
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync() 
        {
            //if(typeof(T) == typeof(Car)) // to make inner join basically with static query
            //    return (IEnumerable<T>) await _dbContext.Set<Car>().Include(C => C.Brand).Include(C => C.Category).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Car)) // to make inner join basically with static query
            //    return await _dbContext.Set<Car>().Where(C => C.Id == id).Include(C => C.Brand).Include(C => C.Category).FirstOrDefaultAsync() as T;
            return await _dbContext.Set<T>().FindAsync(id);
        }


        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {   
            return await ApllySpecifications(spec).ToListAsync(); // Immediate Execution
        }
        public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApllySpecifications(spec).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApllySpecifications(ISpecifications<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }
    }
}
