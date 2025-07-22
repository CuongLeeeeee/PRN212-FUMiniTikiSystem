using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniTikiSystem.DAL.Entities;
using FUMiniTikiSystem.DAL.Interfaces;
using FUMiniTikiSystem.DAL.Repositories;

namespace FUMiniTikiSystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FuminiTikiSystemContext _context;
        public IRepository<Product> Products { get; private set; }
        public IRepository<Customer> Customers { get; private set; }

        public UnitOfWork(FuminiTikiSystemContext context)
        {
            _context = context;
            Products = new Repository<Product>(_context);
            Customers = new Repository<Customer>(_context);
        }

        public Task<int> SaveChangesAsync() =>  _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }

}
