using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TomTom.DataTable.Razor;
using TomTom.DataTable.Razor.Ajax;

namespace TomTom.DataTable.Demo.Infrastruture
{
    public class MyDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver _previousResolver;

        public MyDependencyResolver(IDependencyResolver previousResolver)
        {
            _previousResolver = previousResolver;
        }
        public object GetService(Type serviceType)
        {
            if (serviceType == typeof (IDataTableMetaDataStorage))
                return new DataTablaSessionMetaDataStorage();

            

            return _previousResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _previousResolver.GetServices(serviceType);
        }
        
    }
}