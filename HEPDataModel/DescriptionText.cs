//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HEPDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class DescriptionText
    {
        public DescriptionText()
        {
            this.Items = new HashSet<Item>();
        }
    
        public int Id { get; set; }
        public int LangId { get; set; }
        public string Text { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
