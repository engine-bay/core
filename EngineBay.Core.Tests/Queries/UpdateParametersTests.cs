namespace EngineBay.Core.Tests
{
    using Xunit;

    public class UpdateParametersTests
    {
        [Fact]
        public void NewObjectSetsProperties()
        {
            var id = Guid.NewGuid();
            var mockModel = new MockModel()
            {
                Id = id,
                Age = 29,
                Name = "Bob",
            };
            UpdateParameters<MockModel> sut = new UpdateParameters<MockModel>()
            {
                Id = id,
                Entity = mockModel,
            };

            Assert.Equal(id, sut.Id);
            Assert.Equal(mockModel.Id, sut.Entity.Id);
            Assert.Equal(mockModel.Age, sut.Entity.Age);
            Assert.Equal(mockModel.Name, sut.Entity.Name);
        }
    }
}