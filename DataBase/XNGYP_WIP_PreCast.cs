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
    
    public partial class XNGYP_WIP_PreCast
    {
        public XNGYP_WIP_PreCast()
        {
            this.XNGYP_WorkOrder = new HashSet<XNGYP_WorkOrder>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ProductSNId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> WoodId { get; set; }
        public string WoodName { get; set; }
        public Nullable<int> ColorId { get; set; }
        public string ColorName { get; set; }
        public Nullable<int> Length { get; set; }
        public Nullable<int> Width { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> Staute { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
        public Nullable<int> Grade { get; set; }
        public string WoodNameXL { get; set; }
    
        public virtual XNGYP_Products XNGYP_Products { get; set; }
        public virtual ICollection<XNGYP_WorkOrder> XNGYP_WorkOrder { get; set; }
    }
}
