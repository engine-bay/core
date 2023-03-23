namespace EngineBay.Core
{
    public interface ICommandHandler<TInputParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TInputParameters inputParameters, CancellationToken cancellation);
    }
}