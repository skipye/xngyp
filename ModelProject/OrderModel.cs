using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    //仓库设置
    public class OrderModel
    {
        public int Id { get; set; }
        public string OrderSN { get; set; }
        public decimal? OrderPrice { get; set; }
        public string SaleName { get; set; }
        public string OrderTel { get; set; }
        public string SendTel { get; set; }//仓库功能说明
        public string Custmor { get; set; }
    }
   
    
}
