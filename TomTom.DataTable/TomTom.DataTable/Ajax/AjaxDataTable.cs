using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TomTom.DataTable.Razor
{

    public class AjaxDataTable<T, TBaseViewModel> : DataTable<TBaseViewModel>
        where TBaseViewModel : BaseViewModel
        where T : IDataProvider<TBaseViewModel>
    {
        protected readonly IDataTableMetaDataStorage Storage;
        protected readonly List<Property<TBaseViewModel>> Properties;
        private AjaxDataTable(HtmlHelper html, DataGridParameters parameters, IDependencyResolver resolver)
            : base(html, parameters)
        {
            Storage = resolver.GetService<IDataTableMetaDataStorage>();
        }

        public AjaxDataTable(HtmlHelper html, DataGridParameters parameters, IEnumerable<Column<TBaseViewModel>> columns, IDependencyResolver resolver)
            : this(html, parameters, resolver)
        {
            parameters.IsAjaxDataTable = true;
            parameters.DataTableControllerType = typeof(T);


            var properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(columns);
            var meta = new DataTableMetaData<TBaseViewModel>(resolver)
            {
                Properties = properties,
                Parameters = parameters
            };
            Storage[Parameters.TableId] = meta;

            if (!properties.Any())//TODO: this check is not intuitive. 
            {//if controller has implemented IGridColumnProvider (controller provides grid columns)
                var controller = ((IGridColumnProvider<TBaseViewModel>)resolver.GetService(typeof(T)));
                columns = controller.GetColumns(parameters.TableId);
                properties = DataTableHelpers.ExtractPropertiesAndGridAttributes(columns);
            }
            Properties = properties;

            if (parameters.HasDefaultExcellExport)
            {
                parameters.ExportButtons.Add(new ActionItem()
                {
                    Icon = "glyphicon glyphicon-floppy-disk",
                    OnClick = string.Format("DataGrid.generateDefaultExcel('{0}','/{1}/GenerateDefaultExcel')", parameters.TableId, typeof(T).Name.Replace("Controller", ""))
                });
            }
        }

        protected override GridModel GetGridModel()
        {
            DataGridFilters filterOptionCollection = new DataGridFilters(new List<FilterOption>(), Parameters.TableId)
            {
                Parameters = Parameters
            };

            if (Parameters.IsFiltrable)
            {
                filterOptionCollection =
                    new DataGridFilters(
                        Properties.Where(p => p.GridColumnAttribute.IsFiltrable)
                            .Select(p => p.GenerateFilterOption())
                            .ToList(), Parameters.TableId)
                    {
                        Parameters = Parameters
                    };
                if (Parameters.ItemsPerPage.HasValue)
                    filterOptionCollection.ItemsPerPage = filterOptionCollection.ItemsPerPage == 0 ? Parameters.ItemsPerPage.Value : filterOptionCollection.ItemsPerPage;
            }
            Parameters.DataTableControllerType = typeof(T);

            return new GridModel()
            {
                GridColumns = Properties.Select(p => DataTableHelpers.ExtractColumn(p, Html)).ToList(),
                GridData = new List<GridRow>(),
                IsAjaxTable = true,
                Parameters = Parameters,
                FilterOptionCollection = filterOptionCollection
            };
        }
    }
}