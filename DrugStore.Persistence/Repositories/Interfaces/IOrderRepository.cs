using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;

using DrugStore.Domain.Entities;

namespace DrugStore.Persistence.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> Get();

        Order GetById(int id);

        void Insert(Order order);

        void Update(Order order);

        void Delete(int id);
    }
}