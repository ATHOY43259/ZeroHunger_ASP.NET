using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registration(Employee emp)
        {
            if (ModelState.IsValid)
            {
                var db = new NGO_ZeroHungerEntities();
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }

            return View(emp);
        }
        

        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddRestaurant(Restaurant res)
        {
            if (ModelState.IsValid)
            {
                var db = new NGO_ZeroHungerEntities();
                db.Restaurants.Add(res);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }

            return View(res);
        }


        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Message = "Your Login page.";

            return View();
        }
        
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                NGO_ZeroHungerEntities db = new NGO_ZeroHungerEntities();
                var res = (from r in db.Restaurants
                            where r.name.Equals(login.Name)
                            && r.password.Equals(login.Password)
                            select r).SingleOrDefault();

                var emp = (from e in db.Employees
                               where e.name.Equals(login.Name)
                               && e.password.Equals(login.Password)
                               select e).SingleOrDefault();

                var admin = (from a in db.Admins
                               where a.name.Equals(login.Name)
                               && a.password.Equals(login.Password)
                               select a).SingleOrDefault();

                if (res != null)
                {
                    Session["restaurant"] = res;
                    Session["restaurantName"] = res.name;
                    var returnUrl = Request["ReturnUrl"];
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Dashboard", "Restaurant");
                }

                else if (emp != null)
                {
                    Session["employee"] = emp;
                    Session["employeeName"] = emp.name;
                    Session["employeeID"] = emp.id;
                    var returnUrl = Request["ReturnUrl"];
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Dashboard", "Employee");
                }

                else if (admin != null)
                {
                    Session["admin"] = admin.name;
                    var returnUrl = Request["ReturnUrl"];
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Dashboard", "Admin");
                }
                TempData["Msg"] = "Username Password Invalid";
            }
            return View(login);
        }
    }
}