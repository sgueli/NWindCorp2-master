using NWindCorp2.bus.Entities;
using NWindCorp2.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NWindCorp2.web.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            using (var ctx = new NWindDB())
            {
                var model = ctx.Categories.Include("Products").ToList();
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var category = new Category { };
            return View(category);
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            using (var ctx = new NWindDB())
            {
                //ctx.Categories.Add(category);
                ctx.Categories.Attach(category);
                ctx.Entry(category).State = System.Data.EntityState.Added;
                ctx.SaveChanges();
                TempData["message"] = String.Format("You added a category with an Id of {0}", category.Id);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var ctx = new NWindDB())
            {
                var category = ctx.Categories.SingleOrDefault(c => c.Id == id);
                return View(category);
            }
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            using (var ctx = new NWindDB())
            {
                ctx.Categories.Attach(category);
                ctx.Entry(category).State = System.Data.EntityState.Modified;
                ctx.SaveChanges();
                TempData["message"] = String.Format("Category Id with an id of {0}", category.Id);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Delete(Category category)
        {
            using (var ctx = new NWindDB())
            {
                ctx.Categories.Attach(category);
                ctx.Entry(category).State = System.Data.EntityState.Deleted;
                ctx.SaveChanges();
                TempData["message"] = String.Format("Cateory with an Id of {0} has been deleted", category.Id);
                return RedirectToAction("Index");
            }
        }

    }
}
