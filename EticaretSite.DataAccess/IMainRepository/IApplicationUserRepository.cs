using System;
using EticaretSite.DataAccess.IRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.IMainRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>

    {
       // void Update(ApplicationUser applicationUser);
    }
}
