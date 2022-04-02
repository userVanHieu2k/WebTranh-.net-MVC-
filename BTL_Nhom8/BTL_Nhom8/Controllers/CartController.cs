using BTL_Nhom8.Dto;
using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BTL_Nhom8.Controllers
{
    [MyFilter]
    public class CartController : Controller
    {

        // GET: Cart
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        private const string CartSession = "CartSession";

        public ActionResult Index()
        {
            var cart = (Cart)Session[CartSession];
            if (cart == null || cart.cartLines.Count == 0)
            {
                ViewBag.Message = "Giỏ hàng trống";
            }
            return View(cart);

        }

        [HttpPost]
        public ActionResult AddItem(int product_Id, int quantity)
        {
            if (Session["HoTen"] == null)
            {
                return Redirect("/Home/Login");
            }
            else
            {
                var cart = (Cart)Session[CartSession];
                if (cart == null)
                {
                    cart = new Cart();
                }

                if (cart.cartLines.Exists(x => x.productDto.Id == product_Id))
                {
                    var item = cart.cartLines.Find(x => x.productDto.Id == product_Id);
                     int slt = db.Products.Find(product_Id).Quantity;
                      if (item.quantity + quantity > slt)
                          item.quantity = slt;
                      else item.quantity += quantity; 
                }
                else
                {
                    Product product = db.Products.Find(product_Id);
                    ProductDto productDto = new ProductDto(product);
                    cart.cartLines.Add(new CartItem(productDto,quantity));
                }
                Session[CartSession] = cart;
                Session["CartLineTotal"] = cart.cartLines.Count();
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditItem(int id, int quantity)
        {
            var cart = (Cart) Session[CartSession];
            string sl = "";
            var item = cart.cartLines.FirstOrDefault(x => x.productDto.Id == id);
            item.quantity = quantity;
            sl = Convert.ToDecimal(item.getAmount()).ToString("#,#");
            return Json(new
            {
                status = true, sl
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItem(int id)
        {
            var cart = (Cart)Session[CartSession];
            var item = cart.cartLines.Find(p => p.productDto.Id.Equals(id));
            if(item != null)
            {
                cart.cartLines.Remove(item);
                @Session["CartLineTotal"] = (int)cart.cartLines.Count;
            }
            Session[CartSession] = cart;
            return Json(new
            {
                status = true,
                cartline = @Session["CartLineTotal"]
            }, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult DeleteAllItem()
        {
            var cart = (Cart)Session[CartSession];
            cart.cartLines.RemoveAll(p => p.getAmount() > 1);

            Session[CartSession] = cart;
            Session["CartLineTotal"] = 0;
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}