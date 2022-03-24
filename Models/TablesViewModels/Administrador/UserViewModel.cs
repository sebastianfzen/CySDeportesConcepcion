using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyS___DeportesConcepcioin_v2.Models.TablesViewModels
{
    public class UserViewModel
    {
        public string RutUser { get; set; }
        public string EmailUser { get; set; }
        public string PassUser { get; set; }
    }

    public class EditUserViewModel
    {
        public string RutUser { get; set; }
        public string EmailUser { get; set; }
        public string PassUser { get; set; }
    }
}