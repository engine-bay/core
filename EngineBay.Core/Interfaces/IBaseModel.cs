namespace EngineBay.Core
{
    using Microsoft.EntityFrameworkCore;

    public interface IBaseModel
    {
        public void CreateDataAnnotations(ModelBuilder modelBuilder);
    }
}