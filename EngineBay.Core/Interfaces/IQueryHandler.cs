namespace EngineBay.Core
{
    public interface IQueryHandler<TQueryParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TQueryParameters queryParameters, CancellationToken cancellation);
    }
}