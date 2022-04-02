using BTL_Nhom8.Helper;
using BTL_Nhom8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom8.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        Web_Tranh_Theu db = new Web_Tranh_Theu();
        [FilterAdmin]
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult Login()
       {
            return View();
       }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            Account acc = null ;
            if (ModelState.IsValid)
            {
                acc =
                (Account)(from a in db.Accounts
                              //join r in db.Roles_Account on a.Account_Id equals r.Account_Id
                              where(a.Email.Equals(account.Email) &&
                              a.Password.Equals(account.Password) /*&& r.Role_Id == 1*/)
                              select a).FirstOrDefault();
                if (acc != null)
                {
                    Session["idAdmin"] = acc.Account_Id;
                    Session["UserName"] = acc.Username;
                    Session["Email"] = acc.Email;
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
            return View(acc);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }

}