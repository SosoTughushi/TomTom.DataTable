using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Razor;

namespace TomTom.DataTable.Demo.Infrastruture
{
    public class MyDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IDataTableMetaDataStorage))
                return new DataTablaSessionMetaDataStorage();

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return null;
        }

        public static DependencyResolver Instance =>new DependencyResolver();
    }
}