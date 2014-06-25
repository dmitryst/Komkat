using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace ImportDBFromExcel.Model
{
    public class ImportDataRowModel : ObservableObject
    {

        public ImportDataRowModel()
        {
        
        }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string SubModel { get; set; }

        public string Article { get; set; }

        public string Description { get; set; }

        public bool Quantity { get; set; }

        public decimal PriceBase { get; set; }

        public decimal PriceNative { get; set; }

        public string Manufacturer { get; set; }

        public string OEMs { get; set; }

        public List<string> OEMList { get; set; }
    }
}
