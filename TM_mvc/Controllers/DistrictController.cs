using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM_mvc.Models;
using TM_mvc.ViewModels;

namespace TM_mvc.Controllers
{
    public class DistrictController : Controller
    {
        // GET: District
        TM_mvc_DBEntities db = new TM_mvc_DBEntities();

        //fetch table data
        public ActionResult Index()
        {
            try
            {
                List<district> lstDistrict = db.districts.ToList();
                return View(lstDistrict);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ActionResult AddEditModal(long district_id)
        {
            districtViewModel objDistrictVM = new districtViewModel();
            if (district_id == 0)
            {
                objDistrictVM.district_id = 0;

            }
            else
            {
                Mapper.Initialize(cfg => cfg.CreateMap<district, districtViewModel>());
                district objDistrict = db.districts.Where(x => x.district_id == district_id).FirstOrDefault();
                objDistrictVM = Mapper.Map<districtViewModel>(objDistrict);
            }
            objDistrictVM.Countries = db.countries.ToList();
            return PartialView("AddUpdateDistrict", objDistrictVM);
        }

        public ActionResult GetStates(long country_id)
        {

            List<SelectListItem> stateNames = new List<SelectListItem>();
            List<state> lstState = db.states.Where(x => x.country_id == country_id).ToList();
            lstState.ForEach(x =>
            {
                stateNames.Add(new SelectListItem { Text = x.state_name, Value = x.state_id.ToString() });
            });


            return Json(stateNames, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateDistrict([Bind(Exclude = "district_id,is_deleted,Countries")]districtViewModel objdistvm)
        {
            district objdist;
            Mapper.Initialize(cfg => cfg.CreateMap<districtViewModel, district>());
            objdist = Mapper.Map<district>(objdistvm);

            if (objdist.district_id == 0)
            {
                objdist.created_by = 1;
                objdist.created_date = DateTime.Now;
                objdist.is_active = true;
                objdist.is_deleted = false;
                objdist.version = 1;
                db.districts.Add(objdist);
                db.SaveChanges();
            }
            else
            {
                district dbDist = db.districts.Where(x => x.district_id == objdistvm.district_id).FirstOrDefault();
                dbDist.last_modified_by = 1;
                dbDist.last_modified_date = DateTime.Now;
                dbDist.is_active = objdistvm.is_active;
                dbDist.district_code = objdistvm.district_code;
                dbDist.district_name = objdistvm.district_name;
                dbDist.state_id = objdistvm.state_id;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}