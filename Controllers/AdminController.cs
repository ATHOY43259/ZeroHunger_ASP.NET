using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            NGO_ZeroHungerEntities requestDB = new NGO_ZeroHungerEntities();
            var request = requestDB.Requests;
            return View(request);
        }

        [HttpGet]
        public ActionResult PendingRequest()
        {
            NGO_ZeroHungerEntities requestDB = new NGO_ZeroHungerEntities();
            var request = (from req in requestDB.Requests
                           where req.status.Equals("Pending")
                           select req);
            return View(request);
        }
        
        [HttpPost]
        public ActionResult PendingRequest(Request req)
        {
            var requestDB = new NGO_ZeroHungerEntities();
            var request = (from r in requestDB.Requests 
                           where r.id.Equals(req.id)
                           select r).SingleOrDefault();
            if(request != null)
            {
                requestDB.Entry(request).CurrentValues.SetValues(req);
                requestDB.SaveChanges();
                return RedirectToAction("PendingRequest", "Admin");
            }

            TempData["Msg"] = "Employee ID Invalid";
            return RedirectToAction("PendingRequest", "Admin");
        }

        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}