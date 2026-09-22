using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;
using DrugStore.Domain.Entities;
using DrugStore.Persistence.Repositories.Interfaces;

namespace DrugStore.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private List<Order> orders = new List<Order>();

        public IEnumerable<Order> Get()
        {
            return orders;
        }

        public Order GetById(int id)
        {
            return orders.FirstOrDefault(x => x.Id == id);
        }

        public void Insert(Order order)
        {
            orders.Add(order);
        }

        public void Update(Order order)
        {
            var oldOrder = GetById(order.Id);

            if (oldOrder != null)
            {
                oldOrder.DrugId = order.DrugId;
                oldOrder.CustomerId = order.CustomerId;
                oldOrder.Quantity = order.Quantity;
                oldOrder.Price = order.Price;
            }
        }

        public void Delete(int id)
        {
            var order = GetById(id);

            if (order != null)
            {
                orders.Remove(order);
            }
        }
    }
}