using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Demo.Models
{
    public class ActionsModel : BaseViewModel
    {
        public bool HasActions { get; set; }

        public string Name { get; set; }

        public override List<ActionItem> GetActions(UrlHelper helper,string tableId)
        {
            if(!HasActions)
                return base.GetActions(helper,tableId);

            return new List<ActionItem>()
            {
                new ActionItem()
                {
                    ActionName = "yell name",
                    Icon = "btn btn-success",
                    OnClick = string.Format("alert('{0}')",Name)
                },
                new ActionItem()
                {
                    ActionName = "go to /Home/Index",
                    Link = helper.Action("Index","Home"),
                    Icon = "btn btn-primary"
                }
            };
        }

        public override string GetRowData(string invokerId)
        {
            return string.Format("name={0}",Name);
        }
    }
}