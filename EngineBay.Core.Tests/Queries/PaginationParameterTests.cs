namespace EngineBay.Core.Tests
{
    using Xunit;

    public class PaginationParameterTests
    {
        [Fact]
        public void DefaultPaginationParametersSkipShouldBeZero()
        {
            var paginationParameters = new PaginationParameters();
            Assert.Equal(0, paginationParameters.Skip);
        }

        [Fact]
        public void DefaultPaginationParametersLimitShouldBeZero()
        {
            var paginationParameters = new PaginationParameters();
            Assert.Equal(10, paginationParameters.Limit);
        }

        [Fact]
        public void DefaultPaginationParametersSortOrderShouldBeNone()
        {
            var paginationParameters = new PaginationParameters();
            Assert.Equal(SortOrderType.None, paginationParameters.SortOrder);
        }

        [Fact]
        public void DefaultPaginationParametersSortPropertyShouldBeLastModifiedAt()
        {
            var paginationParameters = new PaginationParameters();
            Assert.Equal("LastUpdatedAt", paginationParameters.SortBy);
        }

        [Fact]
        public void PartiallyConstructedPaginationParametersSkipShouldBeZero()
        {
            var paginationParameters = new PaginationParameters(null, null, null, null);
            Assert.Equal(0, paginationParameters.Skip);
        }

        [Fact]
        public void PartiallyConstructedPaginationParametersLimitShouldBeZero()
        {
            var paginationParameters = new PaginationParameters(null, null, null, null);
            Assert.Equal(10, paginationParameters.Limit);
        }

        [Fact]
        public void PartiallyConstructedPaginationParametersSortOrderShouldBeNone()
        {
            var paginationParameters = new PaginationParameters(null, null, null, null);
            Assert.Equal(SortOrderType.None, paginationParameters.SortOrder);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void PartiallyConstructedPaginationParametersSortPropertyShouldBeLastModifiedAt(string? sortBy)
        {
            var paginationParameters = new PaginationParameters(null, null, sortBy, null);
            Assert.Equal("LastUpdatedAt", paginationParameters.SortBy);
        }
    }
}
