namespace EngineBay.Core
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public static void Annotations(ModelBuilder modelBuilder)
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