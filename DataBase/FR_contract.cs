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
    
    public partial class FR_contract
    {
        public int id { get; set; }
        public int contract_id { get; set; }
        public string SN { get; set; }
        public string customer { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<System.DateTime> receive_date { get; set; }
        public string pay_mode { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<int> operator_id { get; set; }
        public string operator_name { get; set; }
        public System.DateTime created_time { get; set; }
        public bool delete_flag { get; set; }
    }
}
