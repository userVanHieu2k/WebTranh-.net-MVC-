using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BTL_Nhom8.Areas.Admin.Controllers
{
    [FilterAdmin]
    public class ProductController : Controller
    {
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        // GET: Admin/Product
        public ActionResult Index(int? page, string searchString, string currentFilter, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SortByPrice = sortOrder == "Gia" ? "gia_desc" : "Gia";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var products = db.Products.Select(p => p);
            products = products.OrderBy(p => p.Product_Id);
            ViewBag.currentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc": products = products.OrderByDescending(p => p.Name); break;
                case "Gia": products = products.OrderBy(p => p.Price); break;
                case "gia_desc": products = products.OrderByDescending(p => p.Price); break;
                default: products = products.OrderBy(p => p.Product_Id); break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Add_Product()
        {
            var list = db.Categories.ToList();
            ViewBag.list = list;
            ViewBag.Category_Id = new SelectList(db.Categories, "Category_Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Product(Product product)
        {
            try
            {
                ViewBag.Category_Id = new SelectList(db.Categories, "Category_Id", "Name", product.Category_Id);
                if (ModelState.IsValid)
                {
                    var list = db.Categories.ToList();
                    product.Avatar = "";
                    var f = Request.Files["ImageFile"];
                    //var f1 = Request.Files["anh1"];
                    //var f2 = Request.Files["anh2"];
                    //var f3 = Request.Files["anh3"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);                 
                        string UploadPath = Server.MapPath("~/wwwroot/img/Images/" + FileName);
                        f.SaveAs(UploadPath);
                        product.Avatar = FileName;
                    }
                    db.Products.Add(product);
                    //string FileName1 = System.IO.Path.GetFileName(f1.FileName);
                    //string FileName2 = System.IO.Path.GetFileName(f2.FileName);
                    //string FileName3 = System.IO.Path.GetFileName(f3.FileName);
                    //string UploadPath1 = Server.MapPath("~/wwwroot/img/Child_Product_Images/" + FileName1);
                    //string UploadPath2 = Server.MapPath("~/wwwroot/img/Child_Product_Images/" + FileName2);
                    //string UploadPath3 = Server.MapPath("~/wwwroot/img/Child_Product_Images/" + FileName3);
                    //f1.SaveAs(UploadPath1);
                    //f2.SaveAs(UploadPath2);
                    //f3.SaveAs(UploadPath3);
                    int maSP = product.Product_Id;
                    //db.Product_Image.Add(new Product_Image(FileName1, maSP));
                    //db.Product_Image.Add(new Product_Image(FileName2, maSP));
                    //db.Product_Image.Add(new Product_Image(FileName3, maSP));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + e.Message;
            }
            return View(product);
        }
        [HttpGet]
        public ActionResult Edit_Product(int? id)
        {
            Product p = db.Products.Find(id);
            ViewBag.Category_Id = new SelectList(db.Categories, "Category_Id", "Name", p.Category_Id);
            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_Product(Product product)
        {
            try
            {
                ViewBag.Category_Id = new SelectList(db.Categories, "Category_Id", "Name", product.Category_Id);
                if (ModelState.IsValid)
                {
                    var list = db.Categories.ToList();
                    product.Avatar = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UploadPath = Server.MapPath("~/wwwroot/img/Images/" + FileName);
                        f.SaveAs(UploadPath);
                        product.Avatar = FileName;
                    }
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + e.Message;
            }
            return View(product);

        }

        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            bool result = false;
            var u = db.Products.Where(x => x.Product_Id == id).FirstOrDefault();
            if (u != null)
            {
                var image_Childs = db.Product_Image.Where(p => p.Product_Id == id).ToList();
                if (image_Childs.Count > 0)
                {
                    foreach(var item in image_Childs)
                    {
                        db.Product_Image.Remove(item);
                    }
                }
                db.Products.Remove(u);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}