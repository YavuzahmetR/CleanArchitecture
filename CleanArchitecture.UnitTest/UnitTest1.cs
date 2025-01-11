using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public async Task Create_ReturnsOkResult_WhenRequestIsValid()
        {
            var mediatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new("Toyota", "Corolla", 210);
            MessageResponse response = new("Car Created Successfully");
            CancellationToken cancellationToken = new CancellationToken();
            mediatorMock.Setup(mediatorMock => mediatorMock.Send(createCarCommand, cancellationToken)).ReturnsAsync(response);

            CarsController carsController = new(mediatorMock.Object);
            var result = await carsController.CreateCarAsync(createCarCommand, cancellationToken);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            mediatorMock.Verify(mediatorMock => mediatorMock.Send(createCarCommand, cancellationToken), Times.Once);
        }
    }
}
