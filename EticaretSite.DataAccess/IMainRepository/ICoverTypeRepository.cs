using System;
using EticaretSite.DataAccess.IRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.IMainRepository
{
    public interface ICoverTypeRepository :IRepository<CoverType>

    {
        void Update(CoverType category);
    }
}
