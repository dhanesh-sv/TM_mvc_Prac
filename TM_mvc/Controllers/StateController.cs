using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TM_mvc.Models;
using TM_mvc.ViewModels;
using AutoMapper;

namespace TM_mvc.Controllers
{
    public class StateController : Controller
    {
        TM_mvc_DBEntities db = new TM_mvc_DBEntities();
        // GET: State
        public ActionResult Index()
        {
            List<state> obj = db.states.ToList();
            return View(obj);
        }

        public ActionResult AddEditModal(long stateId = 0)
        {
            stateViewModel obj = new stateViewModel();
            if (stateId == 0)
            {
                obj.country_id = 0;
            }
            else
            {
                Mapper.Initialize(cfg => cfg.CreateMap<state, stateViewModel>());
                state objstate = db.states.ToList().Where(x => x.state_id == stateId).FirstOrDefault();
                obj = Mapper.Map<stateViewModel>(objstate);
            }
            obj.Countries = db.countries.ToList();
            return PartialView("AddEditState", obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUpdateState([Bind(Include = "country_id,state_id,state_code,state_name,is_active,version")]stateViewModel obj)
        {
            //Save
            state objState;
            Mapper.Initialize(cfg => cfg.CreateMap<stateViewModel, state>());
            objState = Mapper.Map<state>(obj);
            if (objState.state_id == 0)
            {
                objState.created_by = 1;
                objState.created_date = DateTime.Now;
                objState.version = 1;
                objState.is_active = true;
                objState.is_deleted = false;
                db.states.Add(objState);
                db.SaveChanges();


            }
            //Update
            else
            {
                state objS = db.states.Where(x => x.state_id == obj.state_id).FirstOrDefault();
                objS.state_code = obj.state_code;
                objS.state_name = obj.state_name;
                objS.country_id = obj.country_id;
                objS.last_modified_by = 1;
                objS.last_modified_date = DateTime.Now;
                objS.is_active = obj.is_active;
                objS.is_deleted = false;
                objS.version = obj.version + 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}