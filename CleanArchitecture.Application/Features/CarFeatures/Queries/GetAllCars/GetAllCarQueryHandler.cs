using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using EntityFrameworkCorePagination.Nuget.Pagination;
using MediatR;

namespace CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars
{
    public sealed class GetAllCarQueryHandler : IRequestHandler<GetAllCarQuery, PaginationResult<Car>>
    {
        private readonly ICarService _carService;
        public GetAllCarQueryHandler(ICarService _carService)
        {
            this._carService = _carService;
        }
        public Task<PaginationResult<Car>> Handle(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            return _carService.GetAllCarsAsync(request, cancellationToken);
        }
    }
}
