namespace EngineBay.Core.Tests
{
    using System;
    using Xunit;

    public class UnauthorizedExceptionTests
    {
        [Fact]
        public async void ConstructorWithNoArgumentsThrowsException()
        {
            // Arrange
            var exception = new UnauthorizedException();

            // Act

            // Assert
            await Assert.ThrowsAsync<UnauthorizedException>(() => { throw exception; });
        }

        [Fact]
        public void ConstructorWithMessageSetsMessage()
        {
            // Arrange
            var message = "Test message";
            var exception = new UnauthorizedException(message);

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
            var exception = new UnauthorizedException(message, innerException);

            // Act

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}