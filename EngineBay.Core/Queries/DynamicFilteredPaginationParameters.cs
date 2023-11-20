namespace EngineBay.Core
{
    public class DynamicFilteredPaginationParameters : PaginationParameters
    {
        public DynamicFilteredPaginationParameters(int? skip, int? limit, string? sortBy, SortOrderType? sortOrder, IList<string>? filters)
            : base(skip, limit, sortBy, sortOrder)
        {
            this.Filters = new List<Filter>();

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!string.IsNullOrEmpty(filter))
                    {
                        this.Filters.Add(new Filter(filter));
                    }
                }
            }
        }

        public IList<Filter> Filters { get; }
    }
}
