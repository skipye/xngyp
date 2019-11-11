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
    
    public partial class INV_wood_type
    {
        public INV_wood_type()
        {
            this.CRM_contract_detail = new HashSet<CRM_contract_detail>();
            this.INV_labels = new HashSet<INV_labels>();
            this.SYS_product_Cost = new HashSet<SYS_product_Cost>();
            this.WIP_workflow = new HashSet<WIP_workflow>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string place { get; set; }
        public int safe_stock_max { get; set; }
        public int safe_stock_min { get; set; }
        public string remark { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
        public Nullable<decimal> g_ccl { get; set; }
        public Nullable<decimal> g_bz { get; set; }
        public Nullable<decimal> q_ccl { get; set; }
        public Nullable<decimal> q_bz { get; set; }
        public Nullable<decimal> prcie { get; set; }
        public Nullable<decimal> cc_prcie { get; set; }
        public Nullable<int> Sort { get; set; }
        public Nullable<decimal> PersonPrice { get; set; }
        public string SN { get; set; }
    
        public virtual ICollection<CRM_contract_detail> CRM_contract_detail { get; set; }
        public virtual ICollection<INV_labels> INV_labels { get; set; }
        public virtual ICollection<SYS_product_Cost> SYS_product_Cost { get; set; }
        public virtual ICollection<WIP_workflow> WIP_workflow { get; set; }
    }
}
