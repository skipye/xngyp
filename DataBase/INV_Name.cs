//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class INV_Name
    {
        public INV_Name()
        {
            this.XNGYP_INV_Labels = new HashSet<XNGYP_INV_Labels>();
            this.XNGYP_INV_Semi = new HashSet<XNGYP_INV_Semi>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Type { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
    
        public virtual INV_Name_Type INV_Name_Type { get; set; }
        public virtual ICollection<XNGYP_INV_Labels> XNGYP_INV_Labels { get; set; }
        public virtual ICollection<XNGYP_INV_Semi> XNGYP_INV_Semi { get; set; }
    }
}
