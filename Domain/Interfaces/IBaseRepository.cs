namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public string TableName { get; }

        Task<int> CreateRangeAsync(IEnumerable<T> entity);
    }
}
