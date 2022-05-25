using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SignupL.Models;

namespace SignupL.Controllers
{
    public class LoginController : Controller
    {
        SignupLoginEntities db = new SignupLoginEntities();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(user u)
        {
            var d = db.users.Where(model => model.username == u.username && model.password == u.password).SingleOrDefault();
            if(d != null)
            {
                Session["UserId"] = u.Id.ToString();
                Session["Username"] = u.username.ToString();
                TempData["LoginSuccessMessage"] = "<script>alert('Login Successfully !!')</script>";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "<script>alert('Username and Password incorect !!')</script>";
                return View();
            }
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(user u)
        {
            if (ModelState.IsValid == true)
            {
                db.users.Add(u);
               var a= db.SaveChanges();
                if (a > 0)
                {
                    ViewBag.Message = "<script>alert('Signup Successfully !!')</script>";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "<script>alert('incorrect detail fill !!')</script>";
                }
                
            }
            return View();
        }

        //public ActionResult Logout()
        //{
        //    FormsAuthentication.SignOut();
        //    return RedirectToAction("Index", "Login");
        //}
    }
}