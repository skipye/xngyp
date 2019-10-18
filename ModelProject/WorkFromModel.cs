using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class WorkFromModel
    {
        public string HTSN { get; set; }
        public string ProductName { get; set; }
        public string Customer { get; set; }
        public string ProductXL { get; set; }
        public int? product_SN_id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WorkOrderId { get; set; }
        public string workorder { get; set; }
        public int? GXId { get; set; }
        public int? Status { get; set; }
        public decimal? cost { get; set; }
        public string ArrUser { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? DepartmentId { get; set; }
        public string Department { get; set; }
        public DateTime? exp_begin_date { get; set; }
        public DateTime? exp_end_date { get; set; }
        public DateTime? act_begin_date { get; set; }
        public DateTime? act_end_date { get; set; }
        public string CheckedUser { get; set; }
        public List<SelectListItem> UserByJob { get; set; }
        public string Remark { get; set; }
        public string GongXu { get; set; }
        public string Job { get; set; }
        public string source { get; set; }
        public string SaleName { get; set; }
        public string ListId { get; set; }
        public string WoodName { get; set; }
        public int? Flag { get; set; }
    }
    public class SWorkFromModel
    {
        public int? GXId { get; set; }
        public int? ProductSNId { get; set; }
        public int? FatherId { get; set; }
        public int? WoodId { get; set; }
        public List<SelectListItem> XLDroList { get; set; }
        public List<SelectListItem> MCDroList { get; set; }
    }
    public class WorkOrderEven
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? WorkOrderId { get; set; }
        public string workorder { get; set; }
        public string UserName { get; set; }
        public string Eventlog { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
