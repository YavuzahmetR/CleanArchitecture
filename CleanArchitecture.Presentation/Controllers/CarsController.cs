using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Presentation.Abstraction;
using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class CarsController : ApiController
    {

        public CarsController(IMediator _mediator) : base(_mediator){}
        
                                         
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCarAsync(CreateCarCommand createCarCommand,CancellationToken cancellationToken)
        {
           MessageResponse messageResponse = await _mediator.Send(createCarCommand,cancellationToken);
            return Ok(messageResponse);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetAllCarsAsync(GetAllCarQuery getAllCarQuery, CancellationToken cancellationToken)
        {
            PaginationResult<Car> response = await _mediator.Send(getAllCarQuery, cancellationToken);
            return Ok(response);
        }

    }
}
