using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DrugStore.Application.Dtos;
using DrugStore.Domain.Entities;

namespace DrugStore.Application.Profiles
{
    public class DrugProfile : Profile
    {
        public DrugProfile()
        {
            CreateMap<Drug, DrugDto>();
            CreateMap<DrugDto, Drug>();
        }
    }
}