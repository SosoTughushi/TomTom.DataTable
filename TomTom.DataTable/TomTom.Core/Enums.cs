using System.Collections.Generic;
using System.ComponentModel;

namespace TomTom.DataTable
{
    public enum OperationType
    {
        Empty,
        Equals,
        Contains,
        MoreThen,
        MoreOrEquealsThen,
        LessThen,
        LessOrEquealsThen,
        Between,
        In,
        NotEquals
    }

    public static class OperationTypeTranslations
    {
        private static readonly Dictionary<OperationType,string> Dict = new Dictionary<OperationType, string>
        {
            {OperationType.Empty,"x" },
            {OperationType.Equals,"=" },
            {OperationType.MoreThen,">" },
            {OperationType.MoreOrEquealsThen,">=" },
            {OperationType.LessThen,"<" },
            {OperationType.LessOrEquealsThen,"<=" },
            {OperationType.Between,"x" },
            {OperationType.In,"⊂" },
            {OperationType.Contains,"⊃" },
            {OperationType.NotEquals,"<>" },
        }; 

        public static string ToMathString(this OperationType operationType)
        {
            return Dict[operationType];
        }
    }
}