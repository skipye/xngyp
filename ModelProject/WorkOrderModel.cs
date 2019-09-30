using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class WorkOrderModel
    {
        public int Id { get; set; }
        public string WorkOrder { get; set; }
        public int? Contract_Detail_Id { get; set; }
        public int? WIP_PreCast_Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductSN { get; set; }
        public string ProductName { get; set; }
        public int? Qty { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Customer { get; set; }
        public string SN { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
    public class SWorkOrderModel
    {
        public int Id { get; set; }
        public string WorkOrder { get; set; }
        public int? Contract_Detail_Id { get; set; }
        public int? WIP_PreCast_Id { get; set; }
        public int? ProductId { get; set; }
        public string ProductSN { get; set; }
        public string ProductName { get; set; }
        public int? Qty { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
