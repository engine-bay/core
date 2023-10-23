namespace EngineBay.Core
{
    public interface ICurrentIdentity
    {
        string Username { get; }

        Guid UserId { get; }
    }
}
