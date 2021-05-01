using System;
using EticaretSite.DataAccess.IRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.IMainRepository
{
    public interface ICategoryRepository :IRepository<Category>

    {
        void Update(Category category);
    }
}
