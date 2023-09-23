namespace EngineBay.Core
{
    using System.Linq.Expressions;

    public class FilteredPaginationParameters<TBaseModel> : PaginationParameters
        where TBaseModel : BaseModel
    {
        public FilteredPaginationParameters(PaginationParameters paginationParameters, Expression<Func<TBaseModel, bool>>? filterPredicate)
            : base(paginationParameters?.Skip, paginationParameters?.Limit, paginationParameters?.SortBy, paginationParameters?.SortOrder)
        {
            this.FilterPredicate = filterPredicate;
        }

        public FilteredPaginationParameters(PaginationParameters paginationParameters, FilterParameters? filterParameters)
            : base(paginationParameters?.Skip, paginationParameters?.Limit, paginationParameters?.SortBy, paginationParameters?.SortOrder)
        {
            if (filterParameters is null)
            {
                throw new ArgumentNullException(nameof(filterParameters));
            }

            if (filterParameters.Ids is not null)
            {
                this.FilterPredicate = x => filterParameters.Ids.Contains(x.Id);
            }
        }

        public FilteredPaginationParameters(PaginationParameters paginationParameters)
            : base(paginationParameters?.Skip, paginationParameters?.Limit, paginationParameters?.SortBy, paginationParameters?.SortOrder)
        {
        }

        public FilteredPaginationParameters()
        {
        }

        public Expression<Func<TBaseModel, bool>>? FilterPredicate { get; set; }
    }
}