using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DrugStore.Application.BusinessServices.Interfaces;
using DrugStore.Application.Dtos;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;

namespace DrugStore.Application.BusinessServices
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CustomerDto> Get()
        {
            var customers = _repository.Get();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public CustomerDto GetById(int id)
        {
            var customer = _repository.GetById(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public void Insert(CustomerDto customer)
        {
            var newCustomer = _mapper.Map<Customer>(customer);
            _repository.Insert(newCustomer);
        }

        public void Update(CustomerDto customer)
        {
            var updateCustomer = _mapper.Map<Customer>(customer);
            _repository.Update(updateCustomer);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}