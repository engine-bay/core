namespace EngineBay.Core
{
    public class SearchParameters : PaginationParameters
    {
        public SearchParameters(string? search, int? skip, int? limit, string? sortBy, SortOrderType? sortOrder)
            : base(skip, limit, sortBy, sortOrder)
        {
            this.Search = search;
        }

        public SearchParameters(SearchParameters searchParameters)
            : base(searchParameters?.Skip, searchParameters?.Limit, searchParameters?.SortBy, searchParameters?.SortOrder)
        {
            ArgumentNullException.ThrowIfNull(searchParameters);

            this.Search = searchParameters.Search;
        }

        public SearchParameters(string? search, PaginationParameters paginationParameters)
            : base(paginationParameters?.Skip, paginationParameters?.Limit, paginationParameters?.SortBy, paginationParameters?.SortOrder)
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