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
    
    public partial class XNGYP_Products_Price
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int WoodId { get; set; }
        public string WoodName { get; set; }
        public Nullable<decimal> MCPrice { get; set; }
        public Nullable<decimal> KLPrice { get; set; }
        public Nullable<decimal> DHPrice { get; set; }
        public Nullable<decimal> MGPrice { get; set; }
        public Nullable<decimal> GMPrice { get; set; }
        public Nullable<decimal> YQPrice { get; set; }
        public Nullable<decimal> FLPrice { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
        public Nullable<decimal> CostCprice { get; set; }
        public Nullable<decimal> CCprice { get; set; }
        public Nullable<decimal> MGQPrice { get; set; }
        public Nullable<decimal> PJPrice { get; set; }
    
        public virtual XNGYP_Products XNGYP_Products { get; set; }
    }
}
