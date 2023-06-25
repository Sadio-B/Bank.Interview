using AutoMapper;
using Bank.Interview.Application.Contrats.Common;
using Microsoft.EntityFrameworkCore;

namespace Bank.Interview.Persistence.Repositories.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BankContext _bankContext;
        protected readonly IMapper _mapper;

        public GenericRepository(BankContext bankContext, IMapper mapper)
        {
            _bankContext = bankContext;
            _mapper = mapper;
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _bankContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _bankContext.Set<T>().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _bankContext.Set<T>().CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _bankContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _bankContext.Set<IEnumerable<T>>().AddRangeAsync(entities);
        }

        public void Edit(T entity)
        {
            _bankContext.Entry<T>(entity).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(long id)
        {
            var entity = await _bankContext.Set<T>().FindAsync(id);

            if (entity is null)
                return;

            _bankContext.Set<T>().Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _bankContext.SaveChangesAsync();
        }
    }
}
