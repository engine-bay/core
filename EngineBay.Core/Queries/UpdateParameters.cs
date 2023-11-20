namespace EngineBay.Core
{
    public class UpdateParameters<T>
    {
        public Guid Id { get; set; }

        public T? Entity { get; set; }
    }
}