using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars;
using CleanArchitecture.Domain.Entitites;
using EntityFrameworkCorePagination.Nuget.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Services
{
    public interface ICarService
    {
        Task CreateCarAsync(CreateCarCommand createCarCommand, CancellationToken cancellationToken);
        Task<PaginationResult<Car>> GetAllCarsAsync(GetAllCarQuery getAllCarsQuery, CancellationToken cancellationToken);
    }
}
