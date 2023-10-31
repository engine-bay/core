namespace EngineBay.Core
{
    public interface ICurrentIdentity
    {
        public Guid UserId { get; }

        public string Username { get; }

        public Task<Guid> GetUserIdAsync(CancellationToken cancellation)
        {
            return Task.FromResult(this.UserId);
        }

        public Task<string> GetUsernameAsync(CancellationToken cancellation)
        {
            return Task.FromResult(this.Username);
        }
    }
}
