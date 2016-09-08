using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TomTom.DataTable
{
    public class PagingAndOrderingInfo
    {
        /// <summary>
        /// Items per page count must be greater then zero if you want this argument to work
        /// </summary>
        public int Offset { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int? OrderingColumnIndex { get; set; }
        public string OrderingField { get; set; }
        public int TotalRecords { get; set; }
        public bool IsSortDirectionAscending { get; set; }
        public string TableId { get; set; }
    }
}
