namespace EngineBay.Core.Tests
{
    using Xunit;

    public class SearchParameterTests
    {
        [Fact]
        public void DefaultSearchParametersSkipShouldBeZero()
        {
            var paginationParameters = new SearchParameters();
            Assert.Equal(0, paginationParameters.Skip);
        }

        [Fact]
        public void DefaultSearchParametersLimitShouldBeZero()
        {
            var paginationParameters = new SearchParameters();
            Assert.Equal(10, paginationParameters.Limit);
        }

        [Fact]
        public void DefaultSearchParametersSortOrderShouldBeNone()
        {
            var paginationParameters = new SearchParameters();
            Assert.Equal(SortOrderType.None, paginationParameters.SortOrder);
        }

        [Fact]
        public void DefaultSearchParametersSortPropertyShouldBeLastModifiedAt()
        {
            var paginationParameters = new SearchParameters();
            Assert.Equal("LastUpdatedAt", paginationParameters.SortBy);
        }

        [Fact]
        public void PartiallyConstructedSearchParametersSkipShouldBeZero()
        {
            var paginationParameters = new SearchParameters(null, null, null, null, null);
            Assert.Equal(0, paginationParameters.Skip);
        }

        [Fact]
        public void PartiallyConstructedSearchParametersLimitShouldBeZero()
        {
            var paginationParameters = new SearchParameters(null, null, null, null, null);
            Assert.Equal(10, paginationParameters.Limit);
        }

        [Fact]
        public void PartiallyConstructedSearchParametersSortOrderShouldBeNone()
        {
            var paginationParameters = new SearchParameters(null, null, null, null, null);
            Assert.Equal(SortOrderType.None, paginationParameters.SortOrder);
        }

        [Fact]
        public void PartiallyConstructedSearchParametersSortPropertyShouldBeLastModifiedAt()
        {
            var paginationParameters = new SearchParameters(null, null, null, null, null);
            Assert.Equal("LastUpdatedAt", paginationParameters.SortBy);
        }
    }
}
