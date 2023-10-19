namespace EngineBay.Core
{
    public class NoCurrentIdentity : ICurrentIdentity
    {
        public string? Username => null;

        public Guid UserId => Guid.Empty;
    }
}
