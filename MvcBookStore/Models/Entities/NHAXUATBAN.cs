//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcBookStore.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class NHAXUATBAN
    {
        public NHAXUATBAN()
        {
            this.SACHes = new HashSet<SACH>();
        }
    
        public int MaNXB { get; set; }
        public string TenNXB { get; set; }
        public string Diachi { get; set; }
        public string DienThoai { get; set; }
    
        public virtual ICollection<SACH> SACHes { get; set; }
    }
}
