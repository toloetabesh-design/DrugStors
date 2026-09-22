using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int DrugId { get; set; }

        public Drug Drug { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}