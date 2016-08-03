using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TomTom.DataTable.Razor
{

    public class Filters
    {
        //without parameterless constructor this class can't be serialized when sent from client to server
        public Filters()
        {
            StringFilterOptions = new List<FilterOption<string>>();

            IntFilterOptions = new List<FilterOption<int>>();
            IntFilterOptionsNullable = new List<FilterOption<int?>>();

            DoubleFilterOptions = new List<FilterOption<double>>();
            DoubleFilterOptionsNullable = new List<FilterOption<double?>>();

            DateTimeFilterOptions = new List<FilterOption<DateTime>>();
            DateTimeFilterOptionsNullable = new List<FilterOption<DateTime?>>();

            DecimalFilterOptions = new List<FilterOption<decimal>>();
            DecimalFilterOptionsNullable = new List<FilterOption<decimal?>>();

            BoolFilterOptions = new List<FilterOption<bool>>();
            BoolFilterOptionsNullable = new List<FilterOption<bool?>>();

            FloatFilterOptions = new List<FilterOption<float>>();
            FloatFilterOptionsNullable = new List<FilterOption<float?>>();


        }

        public Filters(List<FilterOption> filterOptions, string tableId)
        {
            TableId = tableId;
            if (filterOptions == null) return;



            StringFilterOptions = filterOptions.OfType<FilterOption<string>>().ToList();

            IntFilterOptions = filterOptions.OfType<FilterOption<int>>().ToList();
            IntFilterOptionsNullable = filterOptions.OfType<FilterOption<int?>>().ToList();

            DoubleFilterOptions = filterOptions.OfType<FilterOption<double>>().ToList();
            DoubleFilterOptionsNullable = filterOptions.OfType<FilterOption<double?>>().ToList();

            DateTimeFilterOptions = filterOptions.OfType<FilterOption<DateTime>>().ToList();
            DateTimeFilterOptionsNullable = filterOptions.OfType<FilterOption<DateTime?>>().ToList();

            DecimalFilterOptions = filterOptions.OfType<FilterOption<decimal>>().ToList();
            DecimalFilterOptionsNullable = filterOptions.OfType<FilterOption<decimal?>>().ToList();

            BoolFilterOptions = filterOptions.OfType<FilterOption<bool>>().ToList();
            BoolFilterOptionsNullable = filterOptions.OfType<FilterOption<bool?>>().ToList();

            FloatFilterOptions = filterOptions.OfType<FilterOption<float>>().ToList();
            FloatFilterOptionsNullable = filterOptions.OfType<FilterOption<float?>>().ToList();

        }

        public List<FilterOption> GetFilterOptions()
        {
            List<FilterOption> filterOptions = new List<FilterOption>();

            filterOptions.AddRange(StringFilterOptions);

            filterOptions.AddRange(IntFilterOptions);
            filterOptions.AddRange(IntFilterOptionsNullable);

            filterOptions.AddRange(DateTimeFilterOptions);
            filterOptions.AddRange(DateTimeFilterOptionsNullable);

            filterOptions.AddRange(DecimalFilterOptions);
            filterOptions.AddRange(DecimalFilterOptionsNullable);

            filterOptions.AddRange(BoolFilterOptions);
            filterOptions.AddRange(BoolFilterOptionsNullable);

            filterOptions.AddRange(FloatFilterOptions);
            filterOptions.AddRange(FloatFilterOptionsNullable);


            return filterOptions;
        }

        public string TableId { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        /// <summary>
        /// Items per page count must be greater then zero if you want this argument to work
        /// </summary>
        public int Offset { get; set; }
        public int? OrderingColumnIndex { get; set; }
        public bool IsSortDirectionAscending { get; set; }

        public string OrderingField { get; set; }

        public List<FilterOption<string>> StringFilterOptions { get; set; }

        public List<FilterOption<int>> IntFilterOptions { get; set; }

        public List<FilterOption<double>> DoubleFilterOptions { get; set; }

        public List<FilterOption<DateTime>> DateTimeFilterOptions { get; set; }

        public List<FilterOption<DateTime?>> DateTimeFilterOptionsNullable { get; set; }

        public List<FilterOption<decimal>> DecimalFilterOptions { get; set; }

        public List<FilterOption<bool>> BoolFilterOptions { get; set; }

        public List<FilterOption<float>> FloatFilterOptions { get; set; }


        public List<FilterOption<int?>> IntFilterOptionsNullable { get; set; }

        public List<FilterOption<double?>> DoubleFilterOptionsNullable { get; set; }

        public List<FilterOption<decimal?>> DecimalFilterOptionsNullable { get; set; }

        public List<FilterOption<bool?>> BoolFilterOptionsNullable { get; set; }

        public List<FilterOption<float?>> FloatFilterOptionsNullable { get; set; }

    }
}