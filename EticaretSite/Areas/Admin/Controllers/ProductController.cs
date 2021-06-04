using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;
using EticaretSite.DataAccess.IMainRepository;
using EticaretSite.Models.DbModels;
using EticaretSite.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.XPath;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using Extensions = System.Xml.Linq.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EticaretSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _uow;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork uow,IWebHostEnvironment hostEnvironment)
        {
            _uow = uow;
            _hostEnvironment = hostEnvironment;

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
            var allObj = _uow.Product.GetAll(includeProperties:"Category");
            return Json(new { data = allObj });

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var deleteData = _uow.Product.Get(id);
            if (deleteData == null)

                return Json(new { success = false, message = "Data Not Found!" });

            string webRootPath = _hostEnvironment.WebRootPath;

            _uow.Product.Remove(deleteData);
            _uow.Save();
            return Json(new { success = true, message = "Delete Operation Successfully" });

        }
        #endregion

        //create or update get method
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategoryList = _uow.category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value= i.Id.ToString()
                }),
               
                CoverTypeList = _uow.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if(id==null)
            

                return View(productVM);

                productVM.Product = _uow.Product.Get(id.GetValueOrDefault());
                if(productVM.Product==null)
                
                    return NotFound();
            return View(productVM);
                
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {

                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images/products");
                    var extenstion = Path.GetExtension(files[0].FileName);
                
                if (productVM.Product.ImageUrl != null)
                {
                    var imageUrl = productVM.Product.ImageUrl;
                    var imagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extenstion),
                        FileMode.Create))
                {                   
                    files[0].CopyTo(fileStreams);
                }
                productVM.Product.ImageUrl = @"/images/products/" + fileName + extenstion;
            }
            else
            {
                if(productVM.Product.Id != 0)
                {
                    var productData = _uow.Product.Get(productVM.Product.Id);
                    productVM.Product.ImageUrl = productData.ImageUrl;
                }
            }

                    if (productVM.Product.Id == 0)
                    {

                        //Create
                        _uow.Product.Add(productVM.Product);

                    }
                    else
                    {

                        //Update
                        _uow.Product.Update(productVM.Product);

                    }
                _uow.Save();
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _uow.category.GetAll().Select(a => new SelectListItem
                {
                    Text = a.CategoryName,
                    Value = a.Id.ToString()
                });

                productVM.CoverTypeList = _uow.CoverType.GetAll().Select(a => new SelectListItem
                {

                    Text = a.Name,
                    Value = a.Id.ToString()

                });

                if(productVM.Product.Id != 0)
                {
                    productVM.Product = _uow.Product.Get(productVM.Product.Id);

                }

                
          
            }


            return View(productVM.Product);
        }
    }
}


/*Category cat = new Category();
       if (id == null)
       {
           //create işlemi için
           return View(cat);

       }
       cat = _uow.category.Get((int)id);
       if (cat != null) 
       {
           return View(cat);
       }
       return NotFound();
       */