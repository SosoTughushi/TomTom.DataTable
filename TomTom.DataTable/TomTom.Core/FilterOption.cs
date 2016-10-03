using System;
using System.Collections.Generic;
using System.Linq;
using TomTom.Utilities;

namespace TomTom.DataTable
{

    public abstract class FilterOption
    {
        public string PropName { get; set; }
        public int Order { get; set; }
        public OperationType OperationType { get; set; }
        public string Text { get; set; }

        public abstract string Value { get; set; }
        public abstract Type ValueType { get; }

        public IEnumerable<OperationType> AllowedFilterOprationTypes { get; set; }
        public IEnumerable<SelectListItem> AwaibleValues { get; set; }


        //TODO: Refactor as extention method in other library
        //public Filter GetFilter()
        //{
        //    return new Filter()
        //    {
        //        Id = Id,
        //        OperationType = OperationType,
        //        Value = !string.IsNullOrEmpty(Value) ? Value : null,
        //        PopertyName = PropName
        //    };
        //}
        //public IEnumerable<SelectListItem> GetAvaibleFilterOperations(params FilterOperation[] filterOperations)
        //{
        //    if (AllowedFilterOprationTypes == null || !AllowedFilterOprationTypes.Any())
        //    {
        //        return filterOperations.Select(f => new SelectListItem()
        //        {
        //            Value = f.OperationType.ToString(),
        //            Text = f.Title
        //        }).ToList();
        //    }

        //    return
        //        filterOperations.Where(f => AllowedFilterOprationTypes.Contains(f.OperationType))
        //            .Select(p => new SelectListItem()
        //            {
        //                Text = p.Title,
        //                Value = p.OperationType.ToString()
        //            });
        //}

        public int Id { get; set; }

        public class FilterOperation
        {
            public OperationType OperationType { get; set; }
            public FilterOperation(OperationType opType, string title)
            {
                OperationType = opType;
                Title = title;
            }

            public string Title { get; set; }
        }

        private string _editorTemplateNameField;

        public string EditorTemplateName
        {
            get { return string.IsNullOrEmpty(_editorTemplateNameField) ? null : _editorTemplateNameField; }
            set { _editorTemplateNameField = value; }
        }

        public abstract List<OperationType> DefaultFilterOptionTypes { get; }

    }

    public class FilterOption<T> : FilterOption
    {
        private readonly Func<string, T> _converter;

        public List<T> Val { get; set; }


        public FilterOption()
        {

        }
        public FilterOption(Func<string, T> valueConverter)
        {
            _converter = valueConverter;
        }

        public override string Value
        {
            get
            {
                return Val != null ? string.Join(",", Val.ToArray()) : "";
            }
            set
            {
                if (value != null)
                {
                    var type = typeof(T);
                    if (type != typeof(string))
                    {
                        Val =
                            value.Split(',')
                                .Select(ChangeType)
                                .ToList();
                    }
                    else
                    {
                        Val = new List<T>() { ChangeType(value) };
                    }
                    //Val = (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(type) ?? type);
                }
                else
                {
                    Val = new List<T>();
                }
            }

        }

        public override Type ValueType
        {
            get
            {
                if (OperationType == OperationType.Between || OperationType == OperationType.In)
                {
                    return typeof(T[]);
                }
                return typeof(T);//Extracting Value Type if T is nullable
            }
        }

        private T ChangeType(string value)
        {
            return _converter != null ?
                        _converter(value) :
                        value.IsNotEmpty() ?
                            (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T))
                            : default(T);
        }
        

        public override List<OperationType> DefaultFilterOptionTypes
        {
            get
            {
                if (IsDirectType<string>())
                {
                    return new List<OperationType>()
                    {
                        OperationType.Empty,
                        OperationType.Equals,
                        OperationType.Contains,
                    };
                }

                if (IsType<bool>())
                {
                    return new List<OperationType>
                    {
                        OperationType.Empty,
                        OperationType.Equals,
                        OperationType.NotEquals
                    };
                }

                if (IsType<DateTime>())
                {
                    return new List<OperationType>
                    {
                        OperationType.Empty,
                        OperationType.MoreThen,
                        OperationType.LessThen,
                        OperationType.MoreOrEquealsThen
                    };
                }

                return new List<OperationType>()
                    {
                        OperationType.Empty,
                        OperationType.Equals,
                        OperationType.MoreThen,
                        OperationType.MoreOrEquealsThen,
                        OperationType.LessThen,
                        OperationType.LessOrEquealsThen,
                        OperationType.In,
                    };

            }
        }

        private static bool IsType<T2>() where T2 : struct
        {
            return typeof(T) == typeof(T2) || typeof(T) == typeof(T2?);
        }

        private static bool IsDirectType<T2>()
        {
            return typeof (T) == typeof (T2);
        }

        
    }
}