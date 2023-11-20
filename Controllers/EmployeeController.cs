using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Auth;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    [EmployeeAccess]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Dashboard()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AssignedRequest()
        {
            int id = Convert.ToInt32(Session["employeeID"]);
            NGO_ZeroHungerEntities requestDB = new NGO_ZeroHungerEntities();
            var request = (from req in requestDB.Requests
                           where req.employee_id == id && req.status == "Assigned"
                          select req);
            return View(request);
        }
        
        [HttpPost]
        public ActionResult AssignedRequest(Request req)
        {
            var requestDB = new NGO_ZeroHungerEntities();
            var request = (from r in requestDB.Requests
                           where r.id.Equals(req.id)
                           select r).SingleOrDefault();

            requestDB.Entry(request).CurrentValues.SetValues(req);
            requestDB.SaveChanges();
            return RedirectToAction("AssignedRequest", "Employee");
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}