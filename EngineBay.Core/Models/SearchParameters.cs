namespace EngineBay.Core
{
    public class SearchParameters : PaginationParameters
    {
        public SearchParameters(string? search, int? skip, int? limit, string? sortBy, SortOrderType? sortOrder)
            : base(skip, limit, sortBy, sortOrder)
        {
            this.Search = search;
        }

        public SearchParameters()
            : base()
        {
        }

        public string? Search { get; set; }
    }
}