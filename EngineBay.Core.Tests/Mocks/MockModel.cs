namespace EngineBay.Core
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class MockModel : BaseModel
    {
        public MockModel()
        {
            this.Name = string.Empty;
            this.Age = 0;
        }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}