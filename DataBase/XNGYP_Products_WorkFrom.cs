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
    
    public partial class XNGYP_Products_WorkFrom
    {
        public XNGYP_Products_WorkFrom()
        {
            this.XNGYP_Products_WorkFrom_Price = new HashSet<XNGYP_Products_WorkFrom_Price>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
    
        public virtual ICollection<XNGYP_Products_WorkFrom_Price> XNGYP_Products_WorkFrom_Price { get; set; }
    }
}
