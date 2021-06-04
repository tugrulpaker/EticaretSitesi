using System;
using EticaretSite.DataAccess.IRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.IMainRepository
{
    
    
        public interface IProductRepository : IRepository<Product>

        {
            void Update(Product product);
        }
    }

