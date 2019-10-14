using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class LabelsModel
    {
        public int Id { get; set; }
        public string SN { get; set; }//标签编码
        public string ProductName { get; set; }
        public string ProductXL { get; set; }
        public int ProductId { get; set; }
        public int? WoodId { get; set; }
        public string WoodName { get; set; }
        public string WoodNameXL { get; set; }
        public string Color { get; set; }
        public int? ColorId { get; set; }
        public int? INVId { get; set; }
        public string INVName { get; set; }
        public DateTime? InputDateTime { get; set; }
        public int? Status { get; set; }
        public int? CRM_Id { get; set; }
        public int? WIP_Id { get; set; }
        public int? ProductSNId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public List<SelectListItem> SHDroList { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public int? flag { get; set; }//销售0，预投1
        public string Remark { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string ListId { get; set; }
        public DateTime? CheckDate { get; set; }
        public string CRM_SN { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int qty { get; set; }
        public int? Grade { get; set; }
        public string Picture { get; set; }
        public int? FatherId { get; set; }
        public string ProductSN { get; set; }//产品标签
    }
    public class SLabelsModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int? WoodId { get; set; }
        public int? INVId { get; set; }
        public int? ColorId { get; set; }
        public int? ProductSNId { get; set; }
        public int? FatherId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> SHDroList { get; set; }
        public List<SelectListItem> CKDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
    
}
