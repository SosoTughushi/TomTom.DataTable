using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Demo.Models
{
    public class Demo1Model
    {
        public int Int { get; set; }
        public string String { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DateN { get; set; }

        public Demo1NestedModel NestedProperty { get; set; }
    }

    public class Demo1NestedModel
    {
        public string String { get; set; }
    }
}