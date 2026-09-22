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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> Get()
        {
            var orders = _repository.Get();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public OrderDto GetById(int id)
        {
            var order = _repository.GetById(id);

            return _mapper.Map<OrderDto>(order);
        }

        public void Insert(OrderDto order)
        {
            var newOrder = _mapper.Map<Order>(order);

            _repository.Insert(newOrder);
        }

        public void Update(OrderDto order)
        {
            var updateOrder = _mapper.Map<Order>(order);

            _repository.Update(updateOrder);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}