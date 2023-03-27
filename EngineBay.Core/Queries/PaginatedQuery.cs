namespace EngineBay.Core
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.EntityFrameworkCore;

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

                if (sortOrder == SortOrderType.Ascending)
                {
                    sortedQuery = query.OrderByDescending(sortByPredicate);
                }

                if (sortOrder == SortOrderType.Descending)
                {
                    sortedQuery = query.OrderBy(sortByPredicate);
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