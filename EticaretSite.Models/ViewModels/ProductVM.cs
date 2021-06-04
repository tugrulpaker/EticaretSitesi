using System;
using System.Collections.Generic;
using EticaretSite.Models.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EticaretSite.Models.ViewModels
{
    public class ProductVM
    {

        public Product Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

    }
}
