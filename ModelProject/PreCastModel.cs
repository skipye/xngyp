using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class PreCastModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? ProductSNId { get; set; }
        public string ProductSN { get; set; }
        public int? ProductId { get; set; }
        public int? WoodId { get; set; }
        public string WoodName { get; set; }
        public string Color { get; set; }
        public int? ColorId { get; set; }
        public int? Staute { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public List<SelectListItem> SHDroList { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ListId { get; set; }
        public int? Qty { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateTime { get; set; }
    }
    public class SPreCastModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? WoodId { get; set; }
        public int? ColorId { get; set; }
        public int? ProductSNId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> SHDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

}
