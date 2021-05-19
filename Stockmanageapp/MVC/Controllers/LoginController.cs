using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(Admin ad1) 
        {
            if (ModelState.IsValid)
            {
                using (DBStockEntities db=new DBStockEntities())
                {
                    var obj = db.Admins.Where(model => model.UserName.Equals(ad1.UserName) && model.Password.Equals(ad1.Password)).FirstOrDefault();
                    
                   
                        Session["Userid"] = ad1.Id.ToString();
                        Session["UserName"] = ad1.UserName.ToString();
                        Console.WriteLine("Login sucessfully");
                        return RedirectToAction("Index", "Stock", null);
                    
                    
                }
            }
            return View(ad1);
        }

     }
}