using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;
using DrugStore.Persistence.Repositories.Interfaces;

namespace DrugStore.Persistence.Repositories
{
    public class DrugRepository : IDrugRepository
    {
        List<Drug> drugs = new List<Drug>();

        public IEnumerable<Drug> Get()
        {
            return drugs;
        }

        public Drug GetById(int id)
        {
            return drugs.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Drug drug)
        {
            drugs.Add(drug);
        }

        public void Update(Drug drug)
        {
            var oldDrug = GetById(drug.Id);

            if (oldDrug != null)
            {
                oldDrug.Name = drug.Name;
                oldDrug.Price = drug.Price;
            }
        }

        public void Delete(int id)
        {
            var drug = GetById(id);

            if (drug != null)
            {
                drugs.Remove(drug);
            }
        }

       
       

        
    }
}