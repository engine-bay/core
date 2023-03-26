namespace EngineBay.Core
{
    public class PaginationParameters
    {
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