using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom8.Areas.Admin.Controllers
{   

    [FilterAdmin]
    public class CategoryController : Controller
    {
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Add_Category()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Category([Bind(Include = "Category_Id,Name,Description")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
            }

            return View(category);
        }
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: MonHocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Category_Id,Name,Description")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!" + ex.Message;
            }
            return View(category);
        }
        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            bool result = false;
            var u = db.Categories.Where(x => x.Category_Id == id).FirstOrDefault();
            
            if (u != null)
            {
                var productChilds = db.Products.Where(p => p.Category_Id == id).ToList();
                if (productChilds.Count > 0)
                {
                    result = false;
                }
                else
                {
                    db.Categories.Remove(u);
                    db.SaveChanges();
                    result = true;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}