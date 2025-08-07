using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JopApplication.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T,Tkey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Tkey id);
        Task<T> AddAsync(T entity);
        Task<T?> UpdateAsync(Tkey id,T entity);
        Task<bool> DeleteAsync(Tkey Id);


    }
}
