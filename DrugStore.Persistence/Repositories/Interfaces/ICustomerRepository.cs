using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrugStore.Domain.Entities;

namespace DrugStore.Persistence.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();

        Customer GetById(int id);

        void Insert(Customer customer);

        void Update(Customer customer);

        void Delete(int id);
    }
}
