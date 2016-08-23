using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TomTom.DataTable.Core.Tests
{

    [TestClass]
    public class DataTableReponseTests
    {

        [TestMethod]
        public void overriden_object_must_be_same_as_list()
        {
            var resp = new DataTableResponse<Dummy>
            {
                TotalRecords = 3,
                DataList = new List<Dummy>()
                    {
                        new Dummy("Merry"),
                        new Dummy("All them witches")
                    }
            };
            CollectionAssert.AreEqual(resp.DataList, resp.Data as List<Dummy>);

        }
    }
}
