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
    
    public partial class XNGYP_WorkOrder
    {
        public int Id { get; set; }
        public string WorkOrder { get; set; }
        public Nullable<int> Contract_Detail_Id { get; set; }
        public Nullable<int> WIP_PreCast_Id { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<System.DateTime> BOM_ready_date { get; set; }
        public Nullable<System.DateTime> BOM_over_date { get; set; }
        public string Paper_path { get; set; }
        public string BOM_path { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> ClosedFlag { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
        public Nullable<int> ProductId { get; set; }
    
        public virtual Contract_Detail Contract_Detail { get; set; }
        public virtual XNGYP_WIP_PreCas XNGYP_WIP_PreCas { get; set; }
        public virtual XNGYP_Products XNGYP_Products { get; set; }
    }
}
