using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces; 

namespace DrugStore.Application.BusinessServices
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _repository;
        private readonly IMapper _mapper;

        public DrugService(IDrugRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<DrugDto> Get()
        {
            var drugs = _repository.Get();

            return _mapper.Map<IEnumerable<DrugDto>>(drugs);
        }

        public DrugDto GetById(int id)
        {
            var drug = _repository.GetById(id);

            return _mapper.Map<DrugDto>(drug);
        }

        public void Insert(DrugDto drug)
        {
            var newDrug = _mapper.Map<Drug>(drug);

            _repository.Insert(newDrug);
        }

        public void Update(DrugDto drug)
        {
            var updateDrug = _mapper.Map<Drug>(drug);

            _repository.Update(updateDrug);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}