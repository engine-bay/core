namespace EngineBay.Core.Tests
{
    using System;
    using Xunit;

    public class ForbiddenExceptionTests
    {
        [Fact]
        public async void ConstructorWithNoArgumentsThrowsException()
        {
            // Arrange
            var exception = new ForbiddenException();

            // Act

            // Assert
            await Assert.ThrowsAsync<ForbiddenException>(() => { throw exception; });
        }

        [Fact]
        public void ConstructorWithMessageSetsMessage()
        {
            // Arrange
            var message = "Test message";
            var exception = new ForbiddenException(message);

            // Act

            // Assert
            Assert.Equal(message, exception.Message);
        }

        [Fact]
        public void ConstructorWithMessageAndInnerExceptionSetsMessageAndInnerException()
        {
            // Arrange
            var message = "Test message";
            var innerException = new ArgumentException("Inner exception");
            var exception = new ForbiddenException(message, innerException);

            // Act

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}