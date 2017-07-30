using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM_mvc.Models;

namespace TM_mvc.Controllers
{
    public class CountryController : Controller
    {
        TM_mvc_DBEntities db = new TM_mvc_DBEntities();
        // GET: Country
        public ActionResult Index()
        {
            List<country> lstcountry = db.countries.ToList();
            // ViewBag.countries = lstcountry;
            return View(lstcountry);
        }
        //[HttpPost]
        public ActionResult AddCountry()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCountry([Bind(Include = "country_code,country_name")]country objCountry)
        {
            objCountry.created_by = 1;
            objCountry.created_date = DateTime.Now;
            objCountry.version = 1;
            objCountry.is_active = true;
            objCountry.is_deleted = false;
            db.countries.Add(objCountry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditCountry(long country_id)
        {
            country obj = db.countries.Where(x => x.country_id == country_id).FirstOrDefault();
            return PartialView(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateCountry([Bind(Include = "country_code,country_name,country_id")]country objCountry)
        {
            if (objCountry.country_id == 0)
            {
                objCountry.created_by = 1;
                objCountry.created_date = DateTime.Now;
                objCountry.version = 1;
                objCountry.is_active = true;
                objCountry.is_deleted = false;
                db.countries.Add(objCountry);
                db.SaveChanges();
            }
            else
            {
                country objUpdate = db.countries.SingleOrDefault(x => x.country_id == objCountry.country_id);                
                objUpdate.country_code = objCountry.country_code;
                objUpdate.country_name = objCountry.country_name;
                objUpdate.is_active = objCountry.is_active;
                objUpdate.last_modified_by = 1;
                objUpdate.last_modified_date = DateTime.Now;
                objUpdate.version = objCountry.version + 1;                
                objUpdate.is_deleted = false;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}