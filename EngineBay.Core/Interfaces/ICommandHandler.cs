namespace EngineBay.Core
{
    public interface ICommandHandler<TInputParameters, TUser, TOutputDto>
    {
        public Task<TOutputDto> Handle(TInputParameters inputParameters, TUser user, CancellationToken cancellation);
    }
}