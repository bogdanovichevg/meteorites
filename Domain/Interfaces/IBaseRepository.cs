namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public string TableName { get; }

        Task<int> AddRangeAsync(IEnumerable<T> entity);
    }
}
