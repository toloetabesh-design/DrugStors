using DrugStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugStore.Persistence.Repositories.Interfaces
{
    public interface IDrugRepository
    {
        IEnumerable<Drug> Get();
        Drug GetById(int id);
        void Insert(Drug drug);
        void Update(Drug drug);
        void Delete(int id);
    }
 
}