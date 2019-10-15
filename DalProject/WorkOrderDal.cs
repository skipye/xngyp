using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class WorkOrderDal
    {
        public List<WorkOrderModel> GetPageList(int? Status,bool IsSale)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_WorkOrder.Where(k => k.DeleteFlag == false && k.ClosedFlag == false)
                            where IsSale==true?p.Contract_Detail_Id>0:p.WIP_PreCast_Id>0
                            where Status>0 ? p.Status==Status : true
                            orderby p.CreateTime descending
                            select new WorkOrderModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName=p.XNGYP_Products.name,
                                ProductSN=p.XNGYP_Products.XNGYP_Products_SN.name,
                                WorkOrder = p.WorkOrder,
                                Qty = p.Qty,
                                Status = p.Status,
                                CreateTime=p.CreateTime,
                                Customer=p.Contract_Detail.Contract_Header.XNGYP_Customers.ShortName,
                                SN= p.Contract_Detail.Contract_Header.SN,
                                Length=p.XNGYP_Products.length,
                                Width=p.XNGYP_Products.width,
                                Height=p.XNGYP_Products.height,
                            }).ToList();
                return List;
            }
        }
        public void AddWorkOrder(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    int i = 1;
                    if (!string.IsNullOrEmpty(item))
                    {
                        XNGYP_WorkOrder WWTable = new XNGYP_WorkOrder();
                        int ProductId = 0;
                        int Status = 0;
                        int Qty = 1;
                        string WorkOrder = "";
                        int CId = 0;
                        int WId = 0;
                        string Paper_path = "";
                        string BOM_path = "";
                        if (item.Contains("s") == true)
                        {
                            int Id = Convert.ToInt32(item.Replace("s", ""));
                            CId = Id;
                            WorkOrder = "WO" + DateTime.Now.ToString("yyyyMMddfff") + i;
                            var tabls = db.Contract_Detail.Where(k => k.Id == Id).SingleOrDefault();
                            Qty = tabls.Qty - tabls.LabelseCount ?? 0;
                            ProductId = tabls.ProductId ?? 0;
                            Paper_path = tabls.XNGYP_Products.paper_path;
                            BOM_path = tabls.XNGYP_Products.BOM_path;
                            tabls.Status = 2;
                        }
                        else {
                            int Id = Convert.ToInt32(item.Replace("w", ""));
                            WId = Id;
                            WorkOrder = "WP" + DateTime.Now.ToString("yyyyMMddfff") + i;
                            var tabls = db.XNGYP_WIP_PreCast.Where(k => k.Id == Id).SingleOrDefault();
                            Qty = tabls.Qty??1;
                            ProductId = tabls.ProductId ?? 0;
                            Paper_path = tabls.XNGYP_Products.paper_path;
                            BOM_path = tabls.XNGYP_Products.BOM_path;
                            tabls.Staute = 2;
                        }
                        if (!string.IsNullOrEmpty(BOM_path) && !string.IsNullOrEmpty(Paper_path))
                        {
                            WWTable.BOM_over_date = DateTime.Now;
                            WWTable.BOM_ready_date = DateTime.Now;
                            Status = 1;
                        }
                        WWTable.WorkOrder = WorkOrder;
                        WWTable.ProductId = ProductId;
                        if (CId > 0) { WWTable.Contract_Detail_Id = CId;WWTable.Flag = 1; }
                        if (WId > 0) { WWTable.WIP_PreCast_Id = WId; WWTable.Flag = 2; }
                        WWTable.Paper_path = Paper_path;
                        WWTable.BOM_path = BOM_path;
                        WWTable.Qty = Qty;
                        WWTable.Status = Status;
                        WWTable.ClosedFlag = false;
                        WWTable.CreateTime = DateTime.Now;
                        WWTable.DeleteFlag = false;
                        db.XNGYP_WorkOrder.Add(WWTable);
                        i++;
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
