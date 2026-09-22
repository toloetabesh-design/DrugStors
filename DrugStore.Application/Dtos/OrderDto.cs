using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrugStore.Application.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public int DrugId { get; set; }

        public int CustomerId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}