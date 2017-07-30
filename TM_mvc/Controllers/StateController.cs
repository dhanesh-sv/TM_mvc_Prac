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
            return View();
        }

        public ActionResult AddEditModal(long stateId=0)
        {
            stateViewModel obj = new stateViewModel();

            Mapper.Initialize(cfg => cfg.CreateMap<state, stateViewModel>());

            if (stateId == 0)
            {
                obj.country_id = 0;
            }
            else
            {
                state objstate = db.states.ToList().Where(x => x.state_id == stateId).FirstOrDefault();
                obj = Mapper.Map<stateViewModel>(objstate);
            }
            obj.Countries = db.countries.ToList();
            return PartialView("AddEditState",obj);
        }
    }
}