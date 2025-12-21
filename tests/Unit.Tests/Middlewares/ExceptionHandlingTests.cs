using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Revision_Part_1.Domain.Entities;
using Revision_Part_1.Domain.Exceptions;
using Revision_Part_1.Middlewares;



namespace Unit.Tests.Middlewares;

public class ExceptionHandlingTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
    {
        //Arrange
        var loggerMock = new Mock<ILogger<ExceptionHandling>>();
        var middleware = new ExceptionHandling(loggerMock.Object);
        var context = new DefaultHttpContext();
        var nextDelegateMock = new Mock<RequestDelegate>();

        //Act
        await middleware.InvokeAsync(context, nextDelegateMock.Object);

        //Assert
        nextDelegateMock.Verify(next => next.Invoke(context), Times.Once);
    }

    [Fact]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandling>>();
        var middleware = new ExceptionHandling(loggerMock.Object);
        var notFoundException = new NotFoundException(nameof(Restaurant), "1");

        // act
        await middleware.InvokeAsync(context, _ => throw notFoundException);

        // Assert
        context.Response.StatusCode.Should().Be(404);

    }

    [Fact]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandling>>();
        var middleware = new ExceptionHandling(loggerMock.Object);
        var exception = new ForbiddenAccessException();

        // act
        await middleware.InvokeAsync(context, _ => throw exception);

        // Assert
        context.Response.StatusCode.Should().Be(403);

    }

    [Fact]
    public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var loggerMock = new Mock<ILogger<ExceptionHandling>>();
        var middleware = new ExceptionHandling(loggerMock.Object);
        var exception = new Exception();

        // act
        await middleware.InvokeAsync(context, _ => throw exception);

        // Assert
        context.Response.StatusCode.Should().Be(500);

    }
}
