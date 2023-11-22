namespace EngineBay.Core
{
    public class PaginationParameters
    {
        public PaginationParameters(int? skip, int? limit, string? sortBy, SortOrderType? sortOrder)
        {
            this.Skip = skip.GetValueOrDefault(0);
            this.Limit = limit.GetValueOrDefault(10);
            this.SortBy = string.IsNullOrEmpty(sortBy) ? "LastUpdatedAt" : sortBy;
            this.SortOrder = sortOrder.GetValueOrDefault(SortOrderType.None);
        }

        public PaginationParameters()
        {
            this.Skip = 0;
            this.Limit = 10;
            this.SortBy = "LastUpdatedAt";
            this.SortOrder = SortOrderType.None;
        }

        public int Skip { get; set; }

        public int Limit { get; set; }

        public string SortBy { get; set; }

        public SortOrderType SortOrder { get; set; }
    }
}