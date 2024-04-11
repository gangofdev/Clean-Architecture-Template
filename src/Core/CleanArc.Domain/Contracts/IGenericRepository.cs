using CleanArc.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<T> GetAsync(int id);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> whereExpression);
    }
}
