using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Demo.Infrastruture;
using TomTom.DataTable.Razor;
using TomTom.DataTable.Demo.Models;
using FF = TomTom.Functional.Functional;

namespace TomTom.DataTable.Demo.Controllers
{
    public class AjaxDataTableDemoController : DataTableController, IDataProvider<FirstAjaxDemoModel>
    {
        public AjaxDataTableDemoController() : base(new MyDependencyResolver().GetService<IDataTableMetaDataStorage>())
        {
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public DataTableResponse<FirstAjaxDemoModel> GetData(DataGridFilters request, FirstAjaxDemoModel notUsed = null)
        {
            var source = _createSource();
            return new DataTableResponse<FirstAjaxDemoModel>()
            {
                DataList = source.DataTableEnumerableFilter(request).ToList(),
                TotalRecords = source.DataTableEnumerableCount(request)
            };
        }

        private List<FirstAjaxDemoModel> _createSource()
        {
            int count = 0;
            var create = FF.Parse(
                (string name, DateTime date, decimal? amount, int categoryId, Status? status) =>
                    new FirstAjaxDemoModel()
                    {
                        Id = ++count,
                        Name = name,
                        Date = date,
                        Amount = amount,
                        CategoryId = categoryId,
                        Status = status
                    });

            return new List<FirstAjaxDemoModel>()
            {
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
                create("Pink Floyd - Coming Back to Life",new DateTime(2008,1,24), 11464788,2,Status.Pending),
                create("Led Zeppelin - Going To California",new DateTime(2008,4,16), 4119524,1,Status.Complete),
                create("Pink Floyd Final Cut (9) - The Fletcher Memorial Home",new DateTime(2008,12,21), 529183,2,Status.Pending),
                create("The White Stripes I think I smell a rat",new DateTime(2009,8,15), 476079,3,Status.Pending),
                create("Ramble On - Led Zeppelin",new DateTime(2009,6,11), 13744144,1,Status.Pending),
           };
        }

    }


}