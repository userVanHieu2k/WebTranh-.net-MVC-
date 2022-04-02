using BTL_Nhom8.Dto;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom8.Controllers
{
    public class HomeController : Controller
    {
        private Web_Tranh_Theu db = new Web_Tranh_Theu();
        public ActionResult Index()
        {


            return View();
        }
        public ActionResult Login()
        { 
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult DanhMuc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Account_Id,Username,Password,Active,Customer_Name,Email")] Account account)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(m => m.Email == account.Email);
                if (check == null)
                {
                    account.Active = false;
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    Roles_Account ra = new Roles_Account();
                    ra.Account_Id = account.Account_Id;
                    ra.Role_Id = 2;
                    db.Roles_Account.Add(ra);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountDto acc)
        {
            if (ModelState.IsValid)
            {

                var user = db.Accounts.Where(u => u.Email.Equals(acc.Email) &&
                 u.Password.Equals(acc.Password)).ToList();
                if (user.Count > 0)
                {
                    Session["HoTen"] = user.FirstOrDefault().Customer_Name;
                    Session["Email"] = user.FirstOrDefault().Email;
                    Session["idUser"] = user.FirstOrDefault().Account_Id;
                    user.FirstOrDefault().Active = true;
                    
                    db.Entry(user.FirstOrDefault()).State = EntityState.Modified;
                    db.SaveChanges();
                    var url = Session["url-redirect"];
                    if (url != null)
                    {
                        Session.Remove("url-redirect");
                        return Redirect(url.ToString());

                    }
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công";
                }

            }
            return View();
        }
        public ActionResult Logout()
        {
            var id = Session["idUser"];
            var acc = db.Accounts.Find(id);
            acc.Active = false;
            db.Entry(acc).State = EntityState.Modified;
            db.SaveChanges();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}