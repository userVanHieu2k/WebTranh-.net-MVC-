using BTL_Nhom8.DAO;
using BTL_Nhom8.Dto;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BTL_Nhom8.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        [RequireHttps]
        public ActionResult Index(int? page, int? size, int? Category_Id,
            string searchString, string CurrentSearch)
        {
            List<Product> products;
            if (searchString != null)
            {
                ViewBag.current_Filter = searchString;
                page = 1;
            }
            else
            {
                searchString = CurrentSearch;
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                if (Category_Id == null || Category_Id == 0)
                {
                    products = db.Products.Where(p => p.Name.Contains(searchString)).ToList();
                }
                else products = db.Products.Where(p => p.Name.Contains(searchString) && p.Category_Id == Category_Id).ToList();
            }
            else
            {
                if (Category_Id == null || Category_Id == 0)
                {
                    products = db.Products.ToList();
                }

                else products = categoriesDAO.GetProductsByCategoryId(Category_Id);
            }
            List<Category> categories = categoriesDAO.GetAllCategoiries();
           
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            ViewBag.categories = categories;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.CurrentSize = pageSize;
            ViewBag.current_Cate = Category_Id;
            
            ViewBag.TotalProducts = db.Products.ToList().Count;
            return View(products.ToPagedList(pageNumber, pageSize));
        }


        [HttpGet]
        public ActionResult Shop_Detail(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            Product product = db.Products.Find(id);
            product.Product_Image = db.Product_Image.Where(pi => pi.Product_Id == id).ToList();

            var splq = db.Products.Where(p => p.Category_Id.Equals(product.Category_Id)
            && p.Product_Id != id).Take(4).ToList();
            ViewBag.sqlq =(List<Product>) splq;
            Product_Image mb = db.Product_Image.Find(id);
            //ViewBag.Product_img = mb;
            ViewBag.Product_img = mb;
           
            return View(product);
        }

        
    }
}