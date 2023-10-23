namespace EngineBay.Core
{
    public class BaseDto
    {
        public BaseDto(BaseModel baseModel)
        {
            if (baseModel is null)
            {
                throw new ArgumentNullException(nameof(baseModel));
            }

            this.Id = baseModel.Id;
            this.CreatedAt = baseModel.CreatedAt;
            this.LastUpdatedAt = baseModel.LastUpdatedAt;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }
    }
}