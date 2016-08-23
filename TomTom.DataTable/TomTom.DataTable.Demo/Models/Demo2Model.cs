using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Demo.Models
{
    public class Demo2Model
    {
        public int Id { get; set; }
        public bool IsCorrupted { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsImportant { get; set; }

        public bool IsSuccess { get; set; }
        public int Amount { get; set; }

        public bool HasCustomClass { get; set; }
    }

    public class Demo2ModelViewModel : BaseViewModel
    {
        public Demo2Model Model { get; set; }

        public override RowType GetRowType(string tableId)
        {
            if(Model.IsCorrupted)
                return RowType.Error;

            if(Model.IsDisabled)
                return RowType.Disabled;

            if (Model.IsImportant)
            {
                return RowType.Bold;
            }

            if (Model.IsSuccess)
            {
                return RowType.Success;
            }

            if (Model.Amount < 50)
            {
                return RowType.Warning;
            }

            return base.GetRowType(tableId);
        }

        public override string GetRowClasses(string invokerId)
        {
            if (Model.HasCustomClass)
            {
                return "blue";
            }
            return base.GetRowClasses(invokerId);
        }
    }
}