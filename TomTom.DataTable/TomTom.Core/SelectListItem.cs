using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.Core
{

    public class SelectListItem
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public SelectListItem()
        {

        }

        public SelectListItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

    }
}
