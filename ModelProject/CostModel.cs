using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ModelProject
{
    public class CostModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WoodId { get; set; }
        public string WoodName { get; set; }
        public decimal? MCPrice { get; set; }
        public decimal? KLPrice { get; set; }
        public decimal? DHPrice { get; set; }
        public decimal? MGPrice { get; set; }
        public decimal? GMPrice { get; set; }
        public decimal? YQPrice { get; set; }
        public decimal? FLPrice { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? FatherId { get; set; }
        public int? ProductSNId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
    }
    public class SCostModel
    {
        public int? ProductId { get; set; }
        public int? WoodId { get; set; }
        public int? FatherId { get; set; }
        public int? ProductSNId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }

    }
   
}
