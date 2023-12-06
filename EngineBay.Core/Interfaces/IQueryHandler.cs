namespace EngineBay.Core
{
    public interface IQueryHandler<TQueryParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TQueryParameters query, CancellationToken cancellation);
    }

    public interface IQueryHandler<TOutputDto>
    {
        public Task<TOutputDto> Handle(CancellationToken cancellation);
    }

    public interface IQueryHandlerSync<TOutputDto>
    {
        public TOutputDto Handle(CancellationToken cancellation);
    }
}