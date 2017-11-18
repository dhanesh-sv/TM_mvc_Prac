using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TM_mvc.Models;

namespace TM_mvc.ViewModels
{
    public class districtViewModel
    {
        public long district_id { get; set; }
        public string district_code { get; set; }
        public string district_name { get; set; }
        public long country_id { get; set; }
        public long state_id { get; set; }
        public bool is_active { get; set; }
        public bool is_deleted { get; set; }
        public long version { get; set; }

        public IEnumerable<country> Countries { get; set; }
    }
}