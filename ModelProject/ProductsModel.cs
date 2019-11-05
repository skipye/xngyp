using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    //仓库设置
    public class ProductsNameModel
    {
        public int Id { get; set; }
        public int ProductsSNId { get; set; }
        public string ProductsSNName { get; set; }
        public string name { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public decimal? price { get; set; }
        public string remark { get; set; }
        public string Picture { get; set; }
        public string paper_path { get; set; }
        public string BOM_path { get; set; }
        public decimal? volume { get; set; }
        public DateTime? created_time { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> FatherDroList { get; set; }
        public int? PersonPrice { get; set; }
        public int? FatherId { get; set; }
        public string XLSecName { get; set; }
    }
    public class ProductsSNModel
    {
        public int Id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string remark { get; set; }
        public string FatherName { get; set; }
        public int? FatherId { get; set; }
        public DateTime? created_time { get; set; }
        public List<SelectListItem> ProXLDroList { get; set; }
    }
}
