using AutoMapper;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance.Mappings
{
    public sealed class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Car, CreateCarCommand>().ReverseMap(); 
        }
    }
}
