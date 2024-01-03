namespace EngineBay.Core.Tests
{
    using Xunit;

    public class DynamicFilteredPaginationParametersTests
    {
        [Fact]
        public void ConstructorSetsProperties()
        {
            // arrange
            int skip = 9;
            int limit = 18;
            string sortBy = "sortBySelector";
            SortOrderType sortOrder = SortOrderType.Descending;
            IList<string> filters = new List<string>
            {
                "name:eq:bob",
                "surname:ne:builder",
            };

            // act
            var sut = new DynamicFilteredPaginationParameters(skip, limit, sortBy, sortOrder, filters);

            // assert
            Assert.Equal(skip, sut.Skip);
            Assert.Equal(limit, sut.Limit);
            Assert.Equal(sortBy, sut.SortBy);
            Assert.Equal(sortOrder, sut.SortOrder);
            Assert.Equal(filters.Count, sut.Filters.Count);
        }
    }
}
