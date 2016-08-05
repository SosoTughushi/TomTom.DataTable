using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public abstract class DataTableMetaData
    {
        public abstract Type Type { get; }

        public DataGridParameters Parameters { get; set; }

        //public abstract byte[] GenerateExcel(object models);
    }

    public class DataTableMetaData<TModel> : DataTableMetaData
        where TModel : BaseViewModel
    {
        private readonly IDependencyResolver _resolver;

        public DataTableMetaData(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }
        public GridModel GenerateList(IEnumerable<TModel> models, HtmlHelper htmlHelper)
        {
            var properties = _getProperties();
            return DataTableHelpers.GetGridModel(htmlHelper, models, Parameters, properties);
        }
        public override Type Type => typeof(TModel);

        public List<Property<TModel>> Properties { get; set; }


        //public override byte[] GenerateExcel(object models)
        //{
        //    var properties = _getProperties();
        //    var model = _convert(properties, models);
        //    return ExportData.ExportExcelDefault(model);
        //}

        //private ExportData.DefaultExcelExportModel<TModel> _convert(IEnumerable<Property<TModel>> properties, object models)
        //{
        //    return new ExportData.DefaultExcelExportModel<TModel>()
        //    {
        //        DataFieldsDic = properties.ToDictionary(c => c.GridColumnAttribute.ExcellWorksheetName ?? c.GridColumnAttribute.Title, c => c.Name),
        //        List = (List<TModel>)models,
        //        DataWorksheetName = Parameters.ExcelDataWorksheetName,
        //        TemplatePath = Parameters.ExcelTemplatePath
        //    };
        //}

        private List<Property<TModel>> _getProperties()
        {
            if (Properties.Any())
                return Properties;
            
            var controller = ((IGridColumnProvider<TModel>)_resolver.GetService(Parameters.DataTableControllerType));
            return DataTableHelpers.ExtractPropertiesAndGridAttributes(controller.GetColumns(Parameters.TableId));
        }
    }


}