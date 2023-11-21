namespace EngineBay.Core
{
    public class PaginatedDto<T>
    {
        public PaginatedDto(int total, int skip, int limit, IEnumerable<T> data)
        {
            ArgumentNullException.ThrowIfNull(data);

            this.Total = total;
            this.Skip = skip;
            this.Limit = limit;
            this.Data = data.ToList();
        }

        public int Total { get; set; }

        public int Skip { get; set; }

        public int Limit { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}