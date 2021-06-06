using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EticaretSite.DataAccess.IMainRepository;
using EticaretSite.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EticaretSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {

        private readonly IUnitOfWork _uow;

        public CompanyController(IUnitOfWork uow)
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
            var allObj = _uow.Company.GetAll();
            return Json(new { data = allObj });

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Company.Get(id);
            if (deleteData == null)

                return Json(new { success = false, message = "Data Not Found!" });

            _uow.Company.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });

        }
        #endregion

        //create or update get method
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Company cat = new Company();
            if (id == null)
            {
                //create işlemi için
                return View(cat);

            }
            cat = _uow.Company.Get((int)id);
            if (cat != null)
            {
                return View(cat);
            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {

                    //Create
                    _uow.Company.Add(company);

                }
                else
                {

                    //Update
                    _uow.Company.Update(company);

                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(company);
        }
    }
}
