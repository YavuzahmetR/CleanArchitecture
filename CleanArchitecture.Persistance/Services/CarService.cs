using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Services
{
    public sealed class CarService : ICarService
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<Car> _cars;
        private readonly IMapper _mapper;
        public CarService(AppDbContext _appDbContext, IMapper _mapper)
        {
            this._appDbContext = _appDbContext;
            _cars = _appDbContext.Set<Car>();
            this._mapper = _mapper;
        }
        public async Task CreateCarAsync(CreateCarCommand createCarCommand, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(createCarCommand);
            await _cars.AddAsync(car, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
