using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        [RestaurantAccess]
        public ActionResult Dashboard()
        {
            ViewBag.Message = "Restaurant";

            return View();
        }

        [RestaurantAccess]
        [HttpGet]
        public ActionResult OpenRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OpenRequest(Request req)
        {
            if (ModelState.IsValid)
            {
                var db = new NGO_ZeroHungerEntities();
                db.Requests.Add(req);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Restaurant");
            }

            return View(req);
        }
        
        [RestaurantAccess]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}