namespace Bank.Interview.Application.Contrats.Common
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T?> GetByIdAsync(long id);

        public Task<int> CountAsync();

        public Task AddAsync(T entity);

        public Task AddRangeAsync(IEnumerable<T> entities);

        public void Edit(T entity);

        public Task DeleteByIdAsync(long id);

        public Task SaveAsync();
    }
}
