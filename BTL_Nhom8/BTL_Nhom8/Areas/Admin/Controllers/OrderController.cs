using BTL_Nhom8.Areas.Admin.Dto;
using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom8.Areas.Admin.Controllers
{
    [FilterAdmin]
    public class OrderController : Controller
    {
        // GET: Admin/Order
        Web_Tranh_Theu db = new Web_Tranh_Theu();
        public static List<OrderDto> orders;
        public ActionResult Index()
        {
            orders  =
            ((List<OrderDto>)(from s in db.Orders
            join r in db.Accounts on s.Account_Id equals r.Account_Id
            select new OrderDto()
            {
                Order_Id = s.Order_Id,
                Customer_Name = r.Customer_Name,
                Customer_Address = s.Customer_Address,
                Customer_Phone = s.Customer_Phone ,
                Order_Date = s.Order_Date.ToString(),
                Customer_Email = r.Email,
                Total_Amount = s.Total_Amount,
                Status = s.Status
            }).ToList());
            return View(orders);
        }
        public ActionResult Order_Detail(int? orderId)
        {
            if(orderId == null)
            {
                return RedirectToAction("Index");
            }
            OrderDto orderDto = orders.Find(o => o.Order_Id == orderId);
            List<OrderDetailDto> orderDetails =
            ((List<OrderDetailDto>)(from s in db.DetailProduct_Order
                              join r in db.Products on s.Product_Id equals r.Product_Id
                              where(s.Order_Id == orderId)
                              select new OrderDetailDto()
                              {
                                  Product_Name = r.Name,
                                  Product_Price = s.Order_Price,
                                  Product_Quantity = s.Order_Quantity
                              }).ToList());
          
            ViewBag.Order = orderDto;
            return View(orderDetails);
        }

        public ActionResult Order_Edit(int? orderId)
        {
            if (orderId == null)
            {
                return RedirectToAction("Index");
            }
            OrderDto orderDto = orders.Find(o => o.Order_Id == orderId);
            return View(orderDto);
        }

        [HttpPost]
        public ActionResult Order_Edit(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
                Order order = db.Orders.Find(orderDto.Order_Id);
                order.Customer_Address = orderDto.Customer_Address;
                order.Customer_Phone = orderDto.Customer_Phone;
                order.Status = orderDto.Status;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderDto);
                
        }
    }
}