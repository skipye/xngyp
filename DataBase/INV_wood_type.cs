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
    }
}
