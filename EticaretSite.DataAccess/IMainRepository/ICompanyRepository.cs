using System;
using EticaretSite.DataAccess.IRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.IMainRepository
{
    public interface ICompanyRepository : IRepository<Company>

    {
        void Update(Company company);
    }
}

