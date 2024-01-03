namespace EngineBay.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class PaginatedDtoTests
    {
        [Fact]
        public void ConstructorValidInputShouldInitializeProperties()
        {
            // Arrange
            int total = 10;
            int skip = 5;
            int limit = 2;
            IEnumerable<int> data = new List<int>() { 1, 2, 3 };

            // Act
            PaginatedDto<int> paginatedDto = new PaginatedDto<int>(total, skip, limit, data);

            // Assert
            Assert.Equal(total, paginatedDto.Total);
            Assert.Equal(skip, paginatedDto.Skip);
            Assert.Equal(limit, paginatedDto.Limit);
            Assert.Equal(data.ToList(), paginatedDto.Data);
        }

        [Fact]
        public void ConstructorEmptyDataShouldInitializeProperties()
        {
            // Arrange
            int total = 10;
            int skip = 5;
            int limit = 2;
            IEnumerable<int> data = Enumerable.Empty<int>();

            // Act
            PaginatedDto<int> paginatedDto = new PaginatedDto<int>(total, skip, limit, data);

            // Assert
            Assert.Equal(total, paginatedDto.Total);
            Assert.Equal(skip, paginatedDto.Skip);
            Assert.Equal(limit, paginatedDto.Limit);
            Assert.Equal(data.ToList(), paginatedDto.Data);
        }
    }
}