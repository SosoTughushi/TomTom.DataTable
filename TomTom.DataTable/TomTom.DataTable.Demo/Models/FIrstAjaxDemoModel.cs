using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Demo.Models
{
    public class FirstAjaxDemoModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal? Amount { get; set; }
        public int CategoryId { get; set; }
        public Status Status { get; set; }
        public int StatusInt => (int) Status;
    }

    public enum Status
    {
        Pending,
        Draft,
        Complete,
        Failed
    }
}