using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Demo.Models
{
    public class DetailsDemoModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string Detailurl(UrlHelper urlHelper, string invokerId)
        {
            return urlHelper.Action("Details", "Home", new {id = Id});
        }
    }
}