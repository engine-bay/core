namespace EngineBay.Core
{
    public interface ICurrentIdentity
    {
        public Guid UserId { get; }

        public string Username { get; }

        public bool HasPermission(string permission);
    }
}
