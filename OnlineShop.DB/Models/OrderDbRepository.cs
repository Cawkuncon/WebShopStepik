﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace OnlineShop.DB.Models
{
    public class OrderDbRepository : IOrderRepository
    {
        private readonly DataBaseContext dataBaseContext;

        public OrderDbRepository(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public void Add(Order order)
        {
            dataBaseContext.Orders.Add(order);
            dataBaseContext.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return dataBaseContext.Orders.ToList();
        }

        public Order GetOrder(Guid id)
        {
            return dataBaseContext.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateStatus(Guid id, int status)
        {
            var ord = this.GetOrder(id);
            ord.Status = status;
            dataBaseContext.SaveChanges();
        }

        public List<Product> GetAllProducts(Guid id)
        {
            var prods = dataBaseContext.Carts.Where(cart => cart.OrderId == id).Select(cart => cart.Product).ToList();
            return prods;
        }
    } 

    public interface IOrderRepository
    {

        public List<Order> GetAll();
        public void Add(Order order);
        public Order GetOrder(Guid id);
        public void UpdateStatus(Guid id, int status);
        public List<Product> GetAllProducts(Guid id);

    }
}
