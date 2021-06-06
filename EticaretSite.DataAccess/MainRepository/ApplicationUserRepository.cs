using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EticaretSite.Data;
using EticaretSite.DataAccess.IMainRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.MainRepository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

       /* public void Update(Category category)
        {
            var data = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (data != null)
            {
                data.CategoryName = category.CategoryName;
            }

        }
       */
    }
}
