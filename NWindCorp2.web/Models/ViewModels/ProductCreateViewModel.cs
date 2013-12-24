using NWindCorp2.bus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NWindCorp2.web.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        public List<Category> Categories { get; set; }
        public Product Product { get; set; }
        public List<SelectListItem> SelectListItems()
        {
            return Categories.Select(c => new SelectListItem { 
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToList();
        }
    }
}