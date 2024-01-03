namespace EngineBay.Core.Tests
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;
    using Xunit;

    public class FilterParametersTests
    {
        [Fact]
        public void ConstructorSetsProperties()
        {
            FilterParameters filterParameters = new FilterParameters();
            Assert.NotNull(filterParameters);
            Assert.Null(filterParameters.Ids);
        }

        [Fact]
        public async void BindSetsNoProperties()
        {
#pragma warning disable CS8625
            var filterParameters = await FilterParameters.BindAsync(null);
            Assert.NotNull(filterParameters);
            Assert.Null(filterParameters.Ids);
#pragma warning restore CS8625
        }

        [Fact]
        public async void BindSetsProperties()
        {
            Guid[] idArray =
            [
                new Guid("bc9e6512-c124-4697-ae26-0f62b30e964d"),
                new Guid("67be3801-9a69-4b13-805d-050b9d358feb"),
            ];
            Dictionary<string, StringValues> queryString = new Dictionary<string, StringValues>
            {
                { "Ids", new StringValues(idArray.Select(id => id.ToString()).ToArray()) },
            };
            QueryCollection queryCollection = new QueryCollection(queryString);
            HttpContext httpContext = new DefaultHttpContext();
            httpContext.Request.Query = queryCollection;

            var filterParameters = await FilterParameters.BindAsync(httpContext);
            Assert.NotNull(filterParameters);
            Assert.NotNull(filterParameters.Ids);
            Assert.Equal(idArray.Length, filterParameters.Ids.Count());
            Assert.Equal(idArray, filterParameters.Ids);
        }
    }
}
