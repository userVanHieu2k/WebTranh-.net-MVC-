using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL_Nhom8.Models;

namespace BTL_Nhom8.Controllers
{
    public class AccountsController : Controller
    {
        private Web_Tranh_Theu db = new Web_Tranh_Theu();

        // GET: Accounts
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(Session["idUser"]);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Account_Id,Username,Password,Active,Customer_Name,Email")] Account account)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["idUser"]);
                var check = db.Accounts.Where(s => s.Account_Id != id).FirstOrDefault(m => m.Email == account.Email);
                if (check == null)
                {
                    db.Entry(account).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["HoTen"] = db.Accounts.Where(s => s.Account_Id == id).FirstOrDefault().Customer_Name;
                    Session["Email"] = db.Accounts.Where(s => s.Account_Id == id).FirstOrDefault().Email;
                    Session["idUser"] = db.Accounts.Where(s => s.Account_Id == id).FirstOrDefault().Account_Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    return View();
                }
            }
            return View(account);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
