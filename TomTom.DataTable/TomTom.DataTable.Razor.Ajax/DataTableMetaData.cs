using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TomTom.Utilities;

namespace TomTom.DataTable.Razor.Ajax
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
        public virtual GridModel GenerateList(IEnumerable<TModel> models, HtmlHelper htmlHelper)
        {
            List<Property<TModel>> properties;

            if (Properties.IsNotEmpty())
                properties = Properties;
            else
            {
                var controller = ((IGridColumnProvider<TModel>)_resolver.GetService(Parameters.DataTableControllerType));
                properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(controller.GetColumns(Parameters.TableId));
            }

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
        
    }


}