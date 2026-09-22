using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Application.Dtos;
using DrugStore.Application.Dtos;

namespace DrugStore.Application.BusinessServices.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDto> Get();

        CustomerDto GetById(int id);

        void Insert(CustomerDto customer);

        void Update(CustomerDto customer);

        void Delete(int id);
    }
}