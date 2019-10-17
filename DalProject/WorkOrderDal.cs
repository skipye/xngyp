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
        //开工操作
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
                        
                        int ProductId = 0;
                        int Status = 0;
                        int Qty = 1;
                        string WorkOrder = "";
                        int CId = 0;
                        int WId = 0;
                        string Paper_path = "";
                        string BOM_path = "";
                        string WoodName = "";
                        int? WoodId = 0;
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
                            WoodId = tabls.WoodId;
                            WoodName = tabls.WoodName;
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
                            WoodId = tabls.WoodId;
                            WoodName = tabls.WoodName;
                        }
                        
                        if (Qty > 1)
                        {
                            for (int j = 0; j < Qty; j++)
                            {
                                XNGYP_WorkOrder WWTable = new XNGYP_WorkOrder();
                                if (!string.IsNullOrEmpty(BOM_path) && !string.IsNullOrEmpty(Paper_path))
                                {
                                    WWTable.BOM_over_date = DateTime.Now;
                                    WWTable.BOM_ready_date = DateTime.Now;
                                    Status = 1;
                                }
                                WWTable.WorkOrder = WorkOrder;
                                WWTable.ProductId = ProductId;
                                if (CId > 0) { WWTable.Contract_Detail_Id = CId; WWTable.Flag = 1; }
                                if (WId > 0) { WWTable.WIP_PreCast_Id = WId; WWTable.Flag = 2; }
                                WWTable.Paper_path = Paper_path;
                                WWTable.BOM_path = BOM_path;
                                WWTable.Qty = 1;
                                WWTable.Status = Status;
                                WWTable.ClosedFlag = false;
                                WWTable.CreateTime = DateTime.Now;
                                WWTable.DeleteFlag = false;
                                WWTable.WoodId = WoodId;
                                WWTable.WoodName = WoodName;
                                db.XNGYP_WorkOrder.Add(WWTable);
                                i++;

                            }
                        }
                        else {
                            XNGYP_WorkOrder WWTable = new XNGYP_WorkOrder();
                            if (!string.IsNullOrEmpty(BOM_path) && !string.IsNullOrEmpty(Paper_path))
                            {
                                WWTable.BOM_over_date = DateTime.Now;
                                WWTable.BOM_ready_date = DateTime.Now;
                                Status = 1;
                            }
                            WWTable.WorkOrder = WorkOrder;
                            WWTable.ProductId = ProductId;
                            if (CId > 0) { WWTable.Contract_Detail_Id = CId; WWTable.Flag = 1; }
                            if (WId > 0) { WWTable.WIP_PreCast_Id = WId; WWTable.Flag = 2; }
                            WWTable.Paper_path = Paper_path;
                            WWTable.BOM_path = BOM_path;
                            WWTable.Qty = 1;
                            WWTable.Status = Status;
                            WWTable.ClosedFlag = false;
                            WWTable.CreateTime = DateTime.Now;
                            WWTable.DeleteFlag = false;
                            WWTable.WoodId = WoodId;
                            WWTable.WoodName = WoodName;
                            db.XNGYP_WorkOrder.Add(WWTable);
                            i++;
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        //工序安排
        public void AddWorlFrom(WorkFromModel Models, string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var WTable = db.XNGYP_WorkOrder.Where(k => k.Id == Id).FirstOrDefault();
                        int? ProId = WTable.ProductId;
                        int? WoodId = WTable.WoodId;
                        int? Flag = WTable.Flag;
                        int WorkOrderId = WTable.Id;
                        XNGYP_WorkFrom table = new XNGYP_WorkFrom();
                        table.WorkOrderId = WorkOrderId;
                        table.GXId = Models.GXId;
                        table.Name = Models.Job;
                        table.exp_begin_date = Models.exp_begin_date;
                        table.exp_end_date = Models.exp_end_date;
                        table.act_begin_date = Models.exp_begin_date;
                        table.cost = Models.cost;
                        table.UserId = Models.UserId;
                        table.UserName = Models.UserName;
                        table.DepartmentId = Models.DepartmentId;
                        table.Department = Models.Department;
                        table.Status = 0;
                        table.CreateTime = DateTime.Now;
                        table.DeleteFlag = false;
                        table.ProductId = ProId;
                        table.WoodId = WoodId;
                        table.Flag = Flag;
                        db.XNGYP_WorkFrom.Add(table);

                        XNGYP_WorkEven Emodels = new XNGYP_WorkEven();
                        Emodels.Name = Models.Job;
                        Emodels.UserId = new UserDal().GetCurrentUserName().UserId;
                        Emodels.UserName = new UserDal().GetCurrentUserName().UserName;
                        Emodels.WorkOrderId = WorkOrderId;
                        Emodels.Eventlog = Emodels.UserName + ",安排" + Models.UserName + "," + Models.Job + "任务";
                        Emodels.Remark = Models.Remark;
                        AddWorkOrderEven(Emodels, db);
                    }
                }
                db.SaveChanges();
            }
        }
        public void AddWorkOrderEven(XNGYP_WorkEven models, XNGYPEntities db)
        {
            var tables = new XNGYP_WorkEven();
            tables.Name = models.Name;
            tables.WorkOrderId = models.WorkOrderId;
            tables.UserId = models.UserId;
            tables.UserName = models.UserName;
            tables.Eventlog = models.Eventlog;
            tables.Remark = models.Remark;
            tables.CreateTime = DateTime.Now;
            tables.DeleteFlag = false;
            db.XNGYP_WorkEven.Add(tables);
        }
        public string GetUserDrolistByJob(string Job)
        {
            using (var db = new XNHREntities())
            {
                var list = (from p in db.ehr_employee.Where(k => k.status == 1)
                            where !string.IsNullOrEmpty(Job) ? p.jobs.Contains(Job) : true
                            orderby p.jobtime descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                department_id = p.department,
                                department = p.departmentname
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name;
                    var IstrValue = item.Id + "#" + item.Name + "#" + item.department_id + "#" + item.department;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        public List<WorkOrderEven> GetWorkOrderEvenList(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_WorkEven.Where(k => k.DeleteFlag == false && k.WorkOrderId == Id)
                            orderby p.CreateTime descending
                            select new WorkOrderEven
                            {
                                Id = p.Id,
                                Name = p.Name,
                                UserName = p.UserName,
                                Eventlog = p.Eventlog,
                                CreateTime = p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        //任务审核
        public void Checked(string ListId, int status)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_WorkFrom.Where(k => k.Id == Id).FirstOrDefault();
                        tables.Status = status;
                        string WoodName = "";
                        if (status == 1)//提交任务，更改实际完成时间
                        {
                            tables.act_end_date = DateTime.Now;
                        }
                        if (status == 2)//审核通过
                        {
                            tables.CheckedDate = DateTime.Now;
                            tables.CheckedUserId = new UserDal().GetCurrentUserName().UserId;
                            tables.CheckedUser = new UserDal().GetCurrentUserName().UserName;

                            int? wId = tables.WorkOrderId;
                            var WTable = db.XNGYP_WorkOrder.Where(m => m.Id == wId).SingleOrDefault();
                            int? CRM_Id = WTable.Contract_Detail_Id;
                            int? WIP_Id = WTable.WIP_PreCast_Id;
                            int Length = 0;
                            int Width = 0;
                            int Height = 0;
                            int flag = 1;
                            int? ColorId = 0;
                            string Color = "";
                            string WoodNameXL = "";
                            int? Grade = 1;
                            if (CRM_Id > 0)
                            {
                                Length = Convert.ToInt32(WTable.Contract_Detail.length);
                                Width = Convert.ToInt32(WTable.Contract_Detail.width);
                                Height = Convert.ToInt32(WTable.Contract_Detail.height);
                                ColorId = WTable.Contract_Detail.ColorId;
                                Color = WTable.Contract_Detail.Color;
                                Grade = WTable.Contract_Detail.Grade;
                                WoodNameXL= WTable.Contract_Detail.WoodNameXL;
                            }
                            else
                            {
                                Length = Convert.ToInt32(WTable.XNGYP_WIP_PreCast.Length);
                                Width = Convert.ToInt32(WTable.XNGYP_WIP_PreCast.Width);
                                Height = Convert.ToInt32(WTable.XNGYP_WIP_PreCast.Height);
                                ColorId = WTable.XNGYP_WIP_PreCast.ColorId;
                                Color = WTable.XNGYP_WIP_PreCast.ColorName;
                                Grade = WTable.XNGYP_WIP_PreCast.Grade;
                                WoodNameXL = WTable.XNGYP_WIP_PreCast.WoodNameXL;
                                flag = 2;
                            }
                            if (tables.Name.Contains("开料"))
                            { WTable.Status = 3; }
                            if (tables.Name.Contains("雕花"))
                            { WTable.Status = 4; }
                            if (tables.Name.Contains("木工后段"))//半成品入库
                            {
                                WTable.Status = 5;
                                int? SW_id = db.XNGYP_INV_Semi.Where(k => k.WorkOrderId == WTable.Id).Count();//判断是否已经入库，防止多次入库
                                if (SW_id <= 0)
                                {
                                    XNGYP_INV_Semi INTable = new XNGYP_INV_Semi();
                                    INTable.ProductId = tables.ProductId;
                                    INTable.ProductName = tables.XNGYP_Products.name;
                                    INTable.ProductSNId = tables.XNGYP_Products.ProductsSNId;
                                    INTable.FatherId = tables.XNGYP_Products.FatherId;
                                    INTable.ProductSN = tables.XNGYP_Products.XNGYP_Products_SN.name;
                                    INTable.WoodId = tables.WoodId;
                                    INTable.WoodName = WTable.WoodName;
                                    INTable.ColorId = ColorId;
                                    INTable.ColorName = Color;
                                    INTable.Length = Length;
                                    INTable.Width = Width;
                                    INTable.Height = Height;
                                    INTable.Status = 0;
                                    INTable.CDetailId = CRM_Id;
                                    INTable.WPCastId = WIP_Id;
                                    INTable.Flag = flag;
                                    INTable.DeleteFlag = false;
                                    INTable.WorkOrderId = tables.WorkOrderId;
                                    INTable.CreateTime = DateTime.Now;
                                    db.XNGYP_INV_Semi.Add(INTable);
                                }
                            }
                            if (tables.Name.Contains("刮磨"))
                            { WTable.Status = 6; }
                            if (tables.Name.Contains("油漆"))
                            { WTable.Status = 7; }
                            if (tables.Name.Contains("配件安装"))//到这里，成品入库
                            {
                                WTable.Status = 8;
                                int? LabCount = db.XNGYP_INV_Labels.Where(k => k.WorkOrderId == WTable.Id).Count();//判断是否已经入库，防止多次入库
                                if (LabCount <= 0)
                                {

                                    XNGYP_INV_Labels table = new XNGYP_INV_Labels();
                                    table.SN = "XN" + new LabelsDal().GenerateTimeStamp() + "_" + Grade;
                                    table.ProductsId = tables.ProductId??1;
                                    table.ProductsSNId = tables.XNGYP_Products.ProductsSNId;
                                    table.Length = Length;
                                    table.Width = Width;
                                    table.Height = Height;
                                    table.WoodId = tables.WoodId;
                                    table.WoodName = WTable.WoodName;
                                    table.ColorId = ColorId;
                                    table.Color = Color;
                                    table.Company = "上海香凝工艺品有限公司";
                                    table.Address = "上海市青浦区朱家角朱枫公路1355号";
                                    table.WebSite = "www.xiangninghm.com";
                                    table.Status = 0;
                                    table.CreateTime = DateTime.Now;
                                    table.DeleteFlag = false;
                                    table.Flag = flag;
                                    table.ContractDetailId = CRM_Id;
                                    table.WIPContractIid = WIP_Id;
                                    table.Grade = Grade;
                                    table.FatherId = tables.XNGYP_Products.FatherId;
                                    table.ProductSN = tables.XNGYP_Products.XNGYP_Products_SN1.SN + tables.XNGYP_Products.XNGYP_Products_SN1.XNGYP_Products_SN2.SN + WoodNameXL + Grade;
                                    db.XNGYP_INV_Labels.Add(table);
                                }      
                            }
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
