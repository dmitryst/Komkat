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
    
    public partial class Brand
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
        }
    
        public int Id { get; set; }
        public string BrandName { get; set; }
    
        public virtual ICollection<Model> Models { get; set; }
    }
}
