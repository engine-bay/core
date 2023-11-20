namespace EngineBay.Core
{
    public class FilterParameters
    {
        public FilterParameters()
            : base()
        {
        }

        public IEnumerable<Guid>? Ids { get; set; }

        public static ValueTask<FilterParameters> BindAsync(HttpContext context)
        {
            var queries = context?.Request.Query[nameof(Ids)].ToList();
            if (queries is null)
            {
                return ValueTask.FromResult(new FilterParameters());
            }

            var result = new FilterParameters
            {
                Ids = queries.Select(id =>
                {
                    var parsedId = Guid.Empty;
                    if (Guid.TryParse(id, out parsedId))
                    {
                        return parsedId;
                    }

                    return Guid.Empty;
                }).ToList(),
            };

            return ValueTask.FromResult(result);
        }
    }
}