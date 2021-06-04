using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EticaretSite.DataAccess.IMainRepository;
using EticaretSite.Models.DbModels;
using EticaretSite.Utility;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EticaretSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _uow;

        public CoverTypeController(IUnitOfWork uow)
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
            // var allObj = _uow.CoverType.GetAll();
            var allCoverTypes = _uow.sp_call.List<CoverType>(ProjectConstant.Proc_CoverType_GetAll, null);
            return Json(new { data = allCoverTypes });

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // var deleteData = _uow.CoverType.Get(id);
            // if(deleteData == null)

            //   return Json(new { success = false, message = "Data Not Found!" });

            //    _uow.CoverType.Remove(deleteData);
            //    _uow.Save();
            //    return Json(new { success = true, message = "Delete Operation Successfully" });
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var deleteData = _uow.sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);
            if(deleteData == null)
            return Json(new { success = false, message = "Data Not Found!" });

            _uow.sp_call.Execute(ProjectConstant.Proc_CoverType_Delete, parameter);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });
        }
        #endregion

        //create or update get method
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if(id == null)
            {
                //create işlemi için
                return View(coverType);

            }
            var parameter = new DynamicParameters();
            parameter.Add("@Id",id);
            coverType = _uow.sp_call.OneRecord<CoverType>(ProjectConstant.Proc_CoverType_Get, parameter);

            //coverType = _uow.CoverType.Get((int)id);
            if(coverType != null)
            {
                return View(coverType);
            }
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert (CoverType CoverType)
        {
            if(ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", CoverType.Name);

                if (CoverType.Id == 0)
                {

                    //Create
                    //  _uow.CoverType.Add(coverType);
                    _uow.sp_call.Execute(ProjectConstant.Proc_CoverType_Create, parameter);

                }
                else
                {

                    //Update
                    parameter.Add("@Id", CoverType.Id);
                    //_uow.CoverType.Update(coverType);
                    _uow.sp_call.Execute(ProjectConstant.Proc_CoverType_Update, parameter);

                }
                _uow.Save();
                return RedirectToAction("Index");
            }
            return View(CoverType);
        }
    }
}
