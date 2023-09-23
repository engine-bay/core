namespace EngineBay.Core
{
    public class FilterParameters
    {
        public FilterParameters()
            : base()
        {
        }

        public IEnumerable<Guid>? Ids { get; set; }
    }
}