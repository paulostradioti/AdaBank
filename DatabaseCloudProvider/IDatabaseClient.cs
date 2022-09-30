namespace DatabaseCloudProvider
{
    public interface IDatabaseClient<T>
    {
        Task<T> GetAsync(string id);
        Task<IReadOnlyCollection<T>> ScanAsync();
        Task InsertAsync(string id, T value);
    }
}
