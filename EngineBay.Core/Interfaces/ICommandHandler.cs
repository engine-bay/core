namespace EngineBay.Core
{
    using System.Security.Claims;

    public interface ICommandHandler<TInputParameters, TOutputDto>
    {
        public Task<TOutputDto> Handle(TInputParameters inputParameters, ClaimsPrincipal user, CancellationToken cancellation);
    }

    /// <summary>
    ///   A command handler for the scenario where the command does not return anything (i.e. if it's related to an HTTP DELETE operation).
    /// </summary>
    /// <typeparam name="TInputParameters">The command input parameters.</typeparam>
    public interface ICommandHandler<in TInputParameters>
    {
        public Task Handle(TInputParameters inputParameters, ClaimsPrincipal user, CancellationToken cancellationToken);
    }
}