namespace EngineBay.Core
{
    public class PaginationParameters
    {
        private int skipValue;

        private int limitValue;

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
    }
}