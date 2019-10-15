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
    
    public partial class XNGYP_Products
    {
        public XNGYP_Products()
        {
            this.XNGYP_Products_WorkFrom_Price = new HashSet<XNGYP_Products_WorkFrom_Price>();
            this.XNGYP_INV_Labels = new HashSet<XNGYP_INV_Labels>();
            this.Contract_Detail = new HashSet<Contract_Detail>();
            this.XNGYP_WIP_PreCast = new HashSet<XNGYP_WIP_PreCast>();
            this.XNGYP_WorkOrder = new HashSet<XNGYP_WorkOrder>();
        }
    
        public int Id { get; set; }
        public int ProductsSNId { get; set; }
        public string name { get; set; }
        public Nullable<int> length { get; set; }
        public Nullable<int> width { get; set; }
        public Nullable<int> height { get; set; }
        public string picture { get; set; }
        public string paper_path { get; set; }
        public string BOM_path { get; set; }
        public Nullable<decimal> volume { get; set; }
        public string remark { get; set; }
        public Nullable<System.DateTime> created_time { get; set; }
        public Nullable<bool> delete_flag { get; set; }
        public Nullable<int> FatherId { get; set; }
    
        public virtual ICollection<XNGYP_Products_WorkFrom_Price> XNGYP_Products_WorkFrom_Price { get; set; }
        public virtual XNGYP_Products_SN XNGYP_Products_SN { get; set; }
        public virtual ICollection<XNGYP_INV_Labels> XNGYP_INV_Labels { get; set; }
        public virtual ICollection<Contract_Detail> Contract_Detail { get; set; }
        public virtual XNGYP_Products_SN XNGYP_Products_SN1 { get; set; }
        public virtual ICollection<XNGYP_WIP_PreCast> XNGYP_WIP_PreCast { get; set; }
        public virtual ICollection<XNGYP_WorkOrder> XNGYP_WorkOrder { get; set; }
    }
}
