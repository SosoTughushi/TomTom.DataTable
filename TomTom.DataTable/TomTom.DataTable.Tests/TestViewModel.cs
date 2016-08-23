using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.DataTable.Razor.Tests
{

    internal class TestViewModel
    {
        public int Id { get; set; }
        public int Number { get; internal set; }
        public string Name { get; internal set; }
    }
}
