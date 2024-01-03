namespace EngineBay.Core.Tests
{
    using System.Globalization;
    using System.Linq.Expressions;
    using Xunit;

    public class FilterTests
    {
        private IQueryable<MockModel> users;
        private MockModel userJohn;
        private MockModel userBob;

        public FilterTests()
        {
            this.userJohn = new MockModel() { Id = Guid.Parse("da59bd50-93dd-4630-91fa-182a299fdd63"), Name = "John", Age = 29 };
            this.userBob = new MockModel() { Id = Guid.Parse("5fd87278-4988-4fb3-8a05-b2cca0334551"), Name = "Bob", Age = 30 };

            this.users = new List<MockModel>()
            {
                this.userJohn,
                this.userBob,
            }.AsQueryable();
        }

        [Fact]
        public void ConstructorValidatesNull()
        {
            string? filterString = null;
#pragma warning disable CS8604 // We are testing null

            Assert.Throws<ArgumentNullException>(() => new Filter(filterString));

#pragma warning restore CS8604
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        public void ConstructorValidatesFilterString(string filterString)
        {
            Assert.Throws<ArgumentException>(() => new Filter(filterString));
        }

        [Theory]
        [InlineData(":eq:value")]
        [InlineData("property::value")]
        [InlineData("property:eq:")]
        public void ConstructorValidatesFilterStringComponents(string filterString)
        {
            Assert.Throws<ArgumentException>(() => new Filter(filterString));
        }

        [Theory]
        [InlineData("Name:eq:Bob", "Bob")]
        [InlineData("Name:neq:Bob", "John")]
        [InlineData("Age:gt:29", "Bob")]
        [InlineData("Age:gte:30", "Bob")]
        [InlineData("Age:lt:30", "John")]
        [InlineData("Age:lte:29", "John")]
        public void FilterWhere(string filterString, string expectedName)
        {
            Filter filter = new Filter(filterString);

            Expression<Func<MockModel, bool>> filterPredicate = GetFilterPredicate(filter);

            var results = this.users.Where(filterPredicate).Select(mockModel => mockModel);
            Assert.Equal(1, results.Count());

            var actual = results.First();
            Assert.NotNull(actual);
            Assert.Equal(expectedName, actual.Name);
        }

        private static Expression<Func<MockModel, bool>> GetFilterPredicate(Filter filter)
        {
            var parameter = Expression.Parameter(typeof(MockModel));
            switch (filter.Field)
            {
                case nameof(MockModel.Name):
                    return filter.CreateFilterPredicate<MockModel>(parameter, Expression.Property(parameter, nameof(MockModel.Name)), Expression.Constant(filter.Value));
                case nameof(MockModel.Age):
                    return filter.CreateFilterPredicate<MockModel>(parameter, Expression.Property(parameter, nameof(MockModel.Age)), Expression.Constant(int.Parse(filter.Value, CultureInfo.InvariantCulture)));
                default: throw new ArgumentException($"MockModel Filter type {filter.Field} not found");
            }
        }
    }
}
