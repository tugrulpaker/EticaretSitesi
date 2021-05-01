using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretSite.DataAccess.IMainRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EticaretSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly IUnitOfWork _uow;

        public CategoryController(IUnitOfWork uow)
        {
            _uow = uow;

        }
        #region Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region API CALLS
        public IActionResult GetAll()
        {
            var allObj = _uow.category.GetAll();
            return Json(new { data = allObj });

        }   
        #endregion
    }
}
