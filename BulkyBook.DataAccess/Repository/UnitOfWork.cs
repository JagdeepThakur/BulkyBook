using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public ICategoryRepository Category { get; private set; }

        public ISP_Call SP_Call { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailsRepository OrderDetails { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            dbContext = db;
            Category = new CategoryRepository(dbContext);
            SP_Call = new SP_Call(dbContext);
            CoverType = new CoverTypeRepository(dbContext);
            Product = new ProductRepository(dbContext);
            Company = new CompanyRepository(dbContext);
            ApplicationUser = new ApplicationUserRepository(dbContext);
            ShoppingCart = new ShoppingCartRepository(dbContext);
            OrderHeader = new OrderHeaderRepository(dbContext);
            OrderDetails = new OrderDetailsRepository(dbContext);
        }

        public void Dispose()
        {
           dbContext.Dispose();
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
