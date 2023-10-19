namespace EngineBay.Core
{
    using System;
    using System.Linq.Expressions;

    public abstract class PaginatedQuery<TBaseModel>
    {
        protected IQueryable<TBaseModel> Sort(IQueryable<TBaseModel> query, Expression<Func<TBaseModel, string?>> sortByPredicate, PaginationParameters paginationParameters)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (paginationParameters is null)
            {
                throw new ArgumentNullException(nameof(paginationParameters));
            }

            var sortBy = paginationParameters.SortBy;

            var sortedQuery = query;

            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortOrder = paginationParameters.SortOrder;

                switch (sortOrder)
                {
                    case SortOrderType.Ascending:
                        sortedQuery = query.OrderBy(sortByPredicate);
                        break;
                    case SortOrderType.Descending:
                        sortedQuery = query.OrderByDescending(sortByPredicate);
                        break;
                    default:
                        break;
                }
            }

            return sortedQuery;
        }

        protected IQueryable<TBaseModel> Paginate(IQueryable<TBaseModel> query, PaginationParameters paginationParameters)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (paginationParameters is null)
            {
                throw new ArgumentNullException(nameof(paginationParameters));
            }

            var limit = paginationParameters.Limit;
            var skip = limit > 0 ? paginationParameters.Skip : 0;

            var paginatedQuery = query;

            if (limit > 0)
            {
                paginatedQuery = query.Skip(skip)
                .Take(limit);
            }

            return paginatedQuery;
        }
    }
}