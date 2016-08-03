using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public enum Align
    {
        Left,
        Right,
        Center,
    }

    [Flags]
    public enum RowType
    {
        None = 1,
        Error = 2,
        Disabled = 4,
        Warning = 8,
        Hidden = 16,
        Bold = 32,
        Unauthorized = 64,
        Success = 128
    }
}