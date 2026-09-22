using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;

namespace DrugStore.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        List<Customer> customers = new List<Customer>();

        public IEnumerable<Customer> Get()
        {
            return customers;
        }

        public Customer GetById(int id)
        {
            return customers.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Customer customer)
        {
            customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var oldCustomer = GetById(customer.Id);

            if (oldCustomer != null)
            {
                oldCustomer.Name = customer.Name;
                oldCustomer.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);

            if (customer != null)
            {
                customers.Remove(customer);
            }
        }
    }
}