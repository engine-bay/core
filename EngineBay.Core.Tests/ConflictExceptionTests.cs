namespace EngineBay.Core.Tests
{
    using System;
    using Xunit;

    public class ConflictExceptionTests
    {
        [Fact]
        public async void ConstructorWithNoArgumentsThrowsException()
        {
            // Arrange
            var exception = new ConflictException();

            // Act

            // Assert
            await Assert.ThrowsAsync<ConflictException>(() => { throw exception; });
        }

        [Fact]
        public void ConstructorWithMessageSetsMessage()
        {
            // Arrange
            var message = "Test message";
            var exception = new ConflictException(message);

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
            var exception = new ConflictException(message, innerException);

            // Act

            // Assert
            Assert.Equal(message, exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}