namespace EngineBay.Core
{
    using System.Security.Claims;

    public interface IQueryHandler<TQueryParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TQueryParameters query, CancellationToken cancellation);
    }

    public interface IQueryHandler<TOutputDto>
    {
        public Task<TOutputDto> Handle(CancellationToken cancellation);
    }
}