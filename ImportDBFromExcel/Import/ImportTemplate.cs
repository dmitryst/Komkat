using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportDBFromExcel.Import
{
    public class ImportTemplate
    {
        public int StartRowNumber { get; set; }

        public int EndRowNumber { get; set; }

        public string ModelColName { get; set; }

        public string ArticleColName { get; set; }

        public string DescriptionColName { get; set; }

        public string QuantityColName { get; set; }

        public string CostBaseColName { get; set; }

        public string CostNativeColName { get; set; }

        public string OEMColName { get; set; }

        public string ManufColName { get; set; }

    }
}
