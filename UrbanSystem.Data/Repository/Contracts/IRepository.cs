using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Repository.Contracts
{
    public interface IRepository<TType, TId>
    {
        TType GetById(TId id);

        Task<TType> GetByIdAsync(TId id);

        IEnumerable<TType> GetAll();

        Task<IEnumerable<TType>> GetAllAsync();

        Task<IEnumerable<TType>> GetAllAsync(Expression<Func<TType, bool>> predicate);

        IQueryable<TType> GetAllAttached();

        void Add(TType item);

        Task AddAsync(TType item);

        bool Delete(TId id);

        Task<bool> DeleteAsync(Expression<Func<TType, bool>> predicate);

        Task<bool> DeleteAsync(TId id);

        bool Update(TType type);

        Task<bool> UpdateAsync(TType type);
    }
}
