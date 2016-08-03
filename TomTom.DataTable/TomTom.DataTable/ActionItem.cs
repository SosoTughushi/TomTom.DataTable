﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{
    public class ActionItem
    {
        public string Id { get; set; }
        public string Link { get; set; }
        public string NameResource { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public string OnClick { get; set; }
        public bool TargetBlank { get; set; }
        public object Clone()
        {
            return base.MemberwiseClone();
        }
    }
}