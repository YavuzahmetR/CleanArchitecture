using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCars;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCorePagination.Nuget.Pagination;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Car> _cars;
        private readonly IMapper _mapper;

        private readonly ICarRepository _carRepository; //Repository pattern
        //private readonly IUnitOfWork _unitOfWork; //Repository pattern

        public CarService(AppDbContext _appDbContext, IMapper _mapper ,
            ICarRepository _carRepository/* IUnitOfWork _unitOfWork*/)
        {
            this._appDbContext = _appDbContext;
            _cars = _appDbContext.Set<Car>();
            this._mapper = _mapper;
            this._carRepository = _carRepository;

            //this._unitOfWork = _unitOfWork;
        }
        public async Task CreateCarAsync(CreateCarCommand createCarCommand, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(createCarCommand);
            await _cars.AddAsync(car, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);


            //await _carRepository.AddAsync(car, cancellationToken); Repository pattern 
            //await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        //public async Task<IList<Car>> GetAllCarsAsync(GetAllCarQuery getAllCarsQuery, CancellationToken cancellationToken)
        //{
        //    return await _carRepository.GetAll().ToListAsync(cancellationToken);
        //}

        public async Task<PaginationResult<Car>> GetAllCarsAsync(GetAllCarQuery getAllCarsQuery, CancellationToken cancellationToken)
        {
            return await _carRepository.Where(p=> p.Name.ToLower().Contains(getAllCarsQuery.Search.ToLower()))
                .ToPagedListAsync(getAllCarsQuery.PageNumber, getAllCarsQuery.PageSize, cancellationToken);

        }
    }
}
