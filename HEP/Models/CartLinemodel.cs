using HEPDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HEP.Models
{
    public class CartLineModel
    {
        public GetItemListProcedure1_Result Item { get; set; }
        public int Quantity { get; set; }
    }
}