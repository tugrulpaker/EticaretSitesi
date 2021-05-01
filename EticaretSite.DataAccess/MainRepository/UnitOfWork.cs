﻿using System;
using EticaretSite.Data;
using EticaretSite.DataAccess.IMainRepository;

namespace EticaretSite.DataAccess.MainRepository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {

            _db = db;
            category = new CategoryRepository(_db);
            sp_call = new SPCallRepository(_db);
        }

        public ICategoryRepository category { get; private set; }

        public ISPCallRepository sp_call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
