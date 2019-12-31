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
    
    public partial class WIP_workorder
    {
        public WIP_workorder()
        {
            this.WIP_workflow = new HashSet<WIP_workflow>();
        }
    
        public int id { get; set; }
        public string workorder { get; set; }
        public Nullable<int> CRM_contract_detail_id { get; set; }
        public Nullable<int> WIP_contract_id { get; set; }
        public int qty { get; set; }
        public Nullable<System.DateTime> BOM_ready_date { get; set; }
        public string paper_path { get; set; }
        public string BOM_path { get; set; }
        public Nullable<System.DateTime> BOM_over_date { get; set; }
        public Nullable<System.DateTime> timetable_ready_date { get; set; }
        public byte status { get; set; }
        public bool closed_flag { get; set; }
        public string remark { get; set; }
        public Nullable<int> reserved1 { get; set; }
        public Nullable<int> reserved2 { get; set; }
        public string reserved3 { get; set; }
        public string reserved4 { get; set; }
        public string reserved5 { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
    
        public virtual ICollection<WIP_workflow> WIP_workflow { get; set; }
        public virtual CRM_contract_detail CRM_contract_detail { get; set; }
    }
}
