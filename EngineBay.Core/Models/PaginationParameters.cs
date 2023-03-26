namespace EngineBay.Core
{
    public class PaginationParameters
    {
        private int skipValue;

        private int limitValue;

        private string? sortBy;

        private SortOrderType? sortOrder;

        public int? Skip
        {
            get { return this.skipValue; }
            set { this.skipValue = value.GetValueOrDefault(0); }
        }

        public int? Limit
        {
            get { return this.limitValue; }
            set { this.limitValue = value.GetValueOrDefault(10); }
        }

        public string? SortBy
        {
            get { return this.sortBy; }
            set { this.sortBy = value is null ? value : "LastUpdatedAt"; } // ordering by last modified as a sensible fallback
        }

        public SortOrderType? SortOrder
        {
            get { return this.sortOrder; }
            set { this.sortOrder = value.GetValueOrDefault(SortOrderType.None); }
        }
    }
}