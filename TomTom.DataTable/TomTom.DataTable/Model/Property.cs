using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public class Property<TObj>
    {
        public Func<TObj, object> ObjectGetter { get; set; }

        public ColumnBase GridColumnAttribute { get; set; }

        public DisplayFormatAttribute DisplayFormatAttribute { get; set; }

        public DisplayAttribute DisplayAttribute { get; set; }

        public string Name { get; set; }

        public Type Type { get; set; }

        public object DefaultValue { get; set; }

        public int ColumnId { get; set; }

        public FilterOption GenerateFilterOption()
        {
            if (GridColumnAttribute.CreateNullableFilter)
            {
                if (!Type.IsNullable())
                {
                    Type = Type.ConvertToNullableType();
                }
            }
            var ret = 
                (FilterOption)Activator.CreateInstance(typeof(FilterOption<>)
                .MakeGenericType(Type));

            ret.Text = GridColumnAttribute.Title ?? DisplayAttribute.GetName();
            ret.EditorTemplateName = GridColumnAttribute.FilterEditorTemplateName;
            ret.PropName = Name;
            ret.GetType().GetProperty("Val").SetValue(ret, GridColumnAttribute.DefaultFilterValues);

            ret.Id = ColumnId;
            ret.AllowedFilterOprationTypes = GridColumnAttribute.AllowedFilterOperationTypes;
            ret.AwaibleValues = GridColumnAttribute.AwaibleValues;
            return ret;
        }

    }
}