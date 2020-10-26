﻿using BulkyBook.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }

        ISP_Call SP_Call { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }

        ICompanyRepository Company { get; }

        IApplicationUserRepository ApplicationUser { get; }

        void Save();
    }
}
