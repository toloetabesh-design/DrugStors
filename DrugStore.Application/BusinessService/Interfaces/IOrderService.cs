using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Application.Dtos;

namespace DrugStore.Application.BusinessServices.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDto> Get();

        OrderDto GetById(int id);

        void Insert(OrderDto order);

        void Update(OrderDto order);

        void Delete(int id);
    }
}