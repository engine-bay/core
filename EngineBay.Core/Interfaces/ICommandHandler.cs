namespace EngineBay.Core
{
    using System.Security.Claims;

    public interface ICommandHandler<TInputParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TInputParameters inputParameters, ClaimsPrincipal user, CancellationToken cancellation);
    }
}