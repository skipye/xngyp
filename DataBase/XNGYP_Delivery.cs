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
    
    public partial class XNGYP_Delivery
    {
        public int Id { get; set; }
        public Nullable<int> CDeatailId { get; set; }
        public Nullable<int> CHeadId { get; set; }
        public Nullable<int> OperatorId { get; set; }
        public string Operator { get; set; }
        public Nullable<System.DateTime> DeliverTime { get; set; }
        public string OrderNum { get; set; }
        public Nullable<int> LabelsId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<bool> DeleteFlag { get; set; }
    
        public virtual XNGYP_INV_Labels XNGYP_INV_Labels { get; set; }
        public virtual Contract_Detail Contract_Detail { get; set; }
    }
}
