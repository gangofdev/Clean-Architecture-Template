using CleanArc.Domain.Common;
using CleanArc.Domain.Contracts;
using CleanArc.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArc.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : BaseAsyncRepository<T>, IGenericRepository<T> where T : BaseEntity
    {
        public GenericRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public new async Task AddAsync(T entity)
        {
            await base.AddAsync(entity);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await base.TableNoTracking.FirstOrDefaultAsync(whereExpression);
        }

        public async Task<T> GetAsync(int id)
        {
            return await base.TableNoTracking.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            base.Update(entity);
            return await this.GetAsync(entity.Id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await base.TableNoTracking.ToListAsync();
        }
    }
}
