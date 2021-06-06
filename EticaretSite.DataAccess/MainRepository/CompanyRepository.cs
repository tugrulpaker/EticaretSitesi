using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EticaretSite.Data;
using EticaretSite.DataAccess.IMainRepository;
using EticaretSite.Models.DbModels;

namespace EticaretSite.DataAccess.MainRepository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void Update(Company company)
        {
            _db.Update(company);

        }
    }
}
