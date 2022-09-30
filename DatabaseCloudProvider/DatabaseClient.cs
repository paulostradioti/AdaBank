namespace DatabaseCloudProvider
{
    public class DatabaseClient<T> : IDatabaseClient<T>
    {
        readonly Random _random = Random.Shared;
        private readonly Dictionary<string, T> _dbSet = new();

        public async Task InsertAsync(string id, T item)
        {
            await Task.Delay(_random.Next(100,200));

            _dbSet.Add(id, item);
        }

        public async Task<IReadOnlyCollection<T>> ScanAsync()
        {
            await Task.Delay(_random.Next(100, 200));

            return _dbSet.Values;
        }

        public async Task<T> GetAsync(string id)
        {
            await Task.Delay(_random.Next(100, 200));

            var contains = _dbSet.TryGetValue(id, out var item);

            return  contains ? item : default;
        }
    }
}
