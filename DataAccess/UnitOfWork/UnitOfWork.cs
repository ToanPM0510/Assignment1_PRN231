using BusinessObject.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly eStoreContext _context;

        public UnitOfWork(eStoreContext context)
        {
            _context = context;
            Members = new Repository<Member>(_context);
            Products = new Repository<Product>(_context);
            Orders = new Repository<Order>(_context);
            OrderDetails = new Repository<OrderDetail>(_context);
        }

        public IRepository<Member> Members { get; private set; }
        public IRepository<Product> Products { get; private set; }
        public IRepository<Order> Orders { get; private set; }
        public IRepository<OrderDetail> OrderDetails { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}