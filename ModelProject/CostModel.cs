using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ModelProject
{
    public class CostModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductSN { get; set; }
        public int? WoodId { get; set; }
        public string WoodName { get; set; }
        public decimal? MCPrice { get; set; }
        public decimal? KLPrice { get; set; }
        public decimal? DHPrice { get; set; }
        public decimal? MGQPrice { get; set; }
        public decimal? MGHPrice { get; set; }
        public decimal? GMPrice { get; set; }
        public decimal? YQPrice { get; set; }
        public decimal? FLPrice { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? FatherId { get; set; }
        public int? ProductSNId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public decimal? CCprice { get; set; }
        public decimal? CostCprice { get; set; }
        public decimal? PersonPrice { get; set; }
        public decimal? Volume { get; set; }
        public decimal? OldPersonPrice { get; set; }
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
