using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Application.Dtos;

namespace DrugStore.Application.BusinessServices.Interfaces
{
    public interface IDrugService
    {
        IEnumerable<DrugDto> Get();

        DrugDto GetById(int id);

        void Insert(DrugDto drug);

        void Update(DrugDto drug);

        void Delete(int id);
    }
}
