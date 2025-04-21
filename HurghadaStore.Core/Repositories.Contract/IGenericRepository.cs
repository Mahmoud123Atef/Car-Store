using HurghadaStore.Core.Entities;
using HurghadaStore.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity // give a constraint to the Generic(T).
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> spec);
        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);
    }
}
