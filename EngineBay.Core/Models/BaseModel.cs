namespace EngineBay.Core
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public static void CreateDataAnnotations(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        protected static void CreateBaseDataAnnotations(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.Entity<BaseModel>().HasKey(x => x.Id);

            modelBuilder.Entity<BaseModel>().Property(x => x.CreatedAt).IsRequired();

            modelBuilder.Entity<BaseModel>().Property(x => x.LastUpdatedAt).IsRequired();
        }
    }
}