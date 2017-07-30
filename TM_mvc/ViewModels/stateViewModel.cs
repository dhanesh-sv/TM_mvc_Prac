using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TM_mvc.Models;

namespace TM_mvc.ViewModels
{
    public class stateViewModel
    {
        public long state_id { get; set; }
        public string state_code { get; set; }
        public string state_name { get; set; }
        public long country_id { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public long version { get; set; }

        public IEnumerable<country> Countries { get; set; }
    }
}