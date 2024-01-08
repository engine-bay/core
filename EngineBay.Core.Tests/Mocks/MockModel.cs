namespace EngineBay.Core
{
    using System;

    public class MockModel
    {
        public MockModel()
        {
            this.Id = Guid.Empty;
            this.Name = string.Empty;
            this.Age = 0;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}