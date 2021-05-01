        using System;
namespace EticaretSite.DataAccess.IMainRepository
{
    public interface IUnitOfWork :IDisposable
    {
        ICategoryRepository category { get; }
        ISPCallRepository sp_call { get; }
        void Save();


    }
}
