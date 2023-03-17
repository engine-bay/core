namespace EngineBay.Core
{
    using Microsoft.EntityFrameworkCore;

    public interface IBaseModel
    {
        public static void CreateDataAnnotations(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}