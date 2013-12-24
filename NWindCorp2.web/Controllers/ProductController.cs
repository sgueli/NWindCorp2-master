using NWindCorp2.bus.Entities;
using NWindCorp2.dal;
using NWindCorp2.web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NWindCorp2.web.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index(int id)
        {
            using (var ctx = new NWindDB())
            {
                var model = ctx.Categories.Include("Products").SingleOrDefault(c => c.Id == id);
                return View(model);
            }
        }

        public ActionResult Create(int id)
        {
            using (var ctx = new NWindDB())
            {
                var category = ctx.Categories.SingleOrDefault(c => c.Id == id);
                var product = new Product { };
                product.Category = category;
                product.CategoryId = category.Id;
                var model = new ProductCreateViewModel
                {
                    Categories = ctx.Categories.ToList(),
                    Product = product
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (var ctx = new NWindDB())
            {
                ctx.Products.Add(product);
                ctx.SaveChanges();
                TempData["message"] = String.Format("A product with an id of {0} has been created", product.Id);
                TempData["modified"] = product.Id;
                return RedirectToAction("Index", new { id = product.CategoryId });
            }
        }

        public ActionResult Edit(int id)
        {
            using (var ctx = new NWindDB())
            {
                var product = ctx.Products.SingleOrDefault(p => p.Id == id);
                var categories = ctx.Categories.ToList();
                var model = new ProductCreateViewModel { 
                    Product = product,
                    Categories = categories
                };
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (var ctx = new NWindDB())
            {
                ctx.Products.Attach(product);
                ctx.Entry(product).State = System.Data.EntityState.Modified;
                ctx.SaveChanges();
                TempData["message"] = String.Format("A product with an id of {0} has been modified", product.Id);
                TempData["modified"] = product.Id;
                return RedirectToAction("Index", new { id = product.CategoryId });
            }
        }

        [HttpPost]
        public ActionResult Delete(Product product, int categoryId)
        {
            using (var ctx = new NWindDB())
            {
                ctx.Products.Attach(product);
                ctx.Entry(product).State = System.Data.EntityState.Deleted;
                ctx.SaveChanges();
                TempData["message"] = String.Format("A product with an id of {0} has been deleted", product.Id);
                return RedirectToAction("Index", new { id = categoryId });
            }
        }

    }
}
