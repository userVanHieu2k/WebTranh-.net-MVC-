using BTL_Nhom8.Dto;
using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BTL_Nhom8.Controllers
{
    [MyFilter]
    public class CheckoutController : Controller
    {
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        private const string CartSession = "CartSession";

        [HttpPost]
        public ActionResult Items_Error(string CartItems)
        {

            var cart = (Cart)Session[CartSession];
            if (cart == null || cart.cartLines.Count == 0)
            {
                return RedirectToAction("index");
            }
            var jsonCartItem = new JavaScriptSerializer().Deserialize<List<CartItem>>(CartItems);
            List<ItemError> itemErrors = new List<ItemError>();
            foreach (var item in jsonCartItem)
            {
                int sltQuantity = db.Products.Where(x => x.Product_Id == item.productDto.Id).Select(x => x.Quantity).SingleOrDefault();

                if (item.quantity > sltQuantity)
                {
                    var i = cart.cartLines.SingleOrDefault(x => x.productDto.Id == item.productDto.Id);
                    itemErrors.Add(new ItemError(i.productDto, sltQuantity, item.quantity));
                }
            }
            if (itemErrors.Count > 0)
            {
                return View(itemErrors);
            }
            else
            {
                int id_user = (int)Session["idUser"];
                Account acc = (Account)db.Accounts.Where(a => a.Account_Id.Equals(id_user)).First();
                cart.customerDto = new CustomerDto(acc);
                Session[CartSession] = cart;
                return RedirectToAction("Index");
            }
        }
        
        public ActionResult Index()
        {
            var cart = (Cart)Session[CartSession];
            if (cart == null || cart.cartLines.Count == 0)
            {
                return Redirect("/Cart/Index");
            }
            return View(cart);

        }

        [HttpPost]
        public ActionResult Final_Checkout(CustomerDto customerDto)
        {

            var cart = (Cart)Session[CartSession];

            if (cart.cartLines.Count < 1)
            {
                return Redirect("/Cart/Index");
            }
            if (ModelState.IsValid)
            {
                Order order = new Order();
                order.Account_Id = (int)Session["idUser"];
                order.Customer_Address = customerDto.Address;
                order.Customer_Phone = customerDto.Telephone;
                order.Status = true;
                order.Order_Date = DateTime.Now;
                order.Total_Amount = ((long)cart.cartLines.Sum(x => x.getAmount()));
                db.Orders.Add(order);
                foreach (var item in cart.cartLines)
                {
                    var product = db.Products.Find(item.productDto.Id);
                    product.Quantity -= item.quantity;
                    db.Entry(product).State = EntityState.Modified;
                    DetailProduct_Order detail = new DetailProduct_Order();
                    detail.Order_Id = order.Order_Id;
                    detail.Product_Id = item.productDto.Id;
                    detail.Order_Quantity = item.quantity;
                    detail.Order_Price = item.productDto.Price;
                    db.DetailProduct_Order.Add(detail);  
                }
                db.SaveChanges();
                cart.cartLines.RemoveAll(x => x.getAmount() > 0);
                Session["CartLineTotal"] = 0;
                Session.Remove(CartSession);
                return View(cart);
            }
            cart.customerDto = customerDto;
            return View("Index",cart);
        }

    }
}