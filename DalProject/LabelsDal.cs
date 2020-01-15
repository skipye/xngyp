using Common;
using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class LabelsDal
    {
        public string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public List<LabelsModel> GetLabelsList(SLabelsModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Status!=9)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductsSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? SModel.FatherId == p.FatherId : true
                            where SModel.INVId > 0 ? SModel.INVId == p.INVId : true
                            where SModel.WoodId > 0 ? SModel.WoodId == p.WoodId : true
                            where SModel.Status !=null && SModel.Status >0? SModel.Status == p.Status : true
                            where !string.IsNullOrEmpty(SModel.ProductName) ? p.XNGYP_Products.name.Contains(SModel.ProductName) : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new LabelsModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductsId,
                                ProductName = p.XNGYP_Products.name,
                                ProductXL = p.XNGYP_Products_SN.name,
                                Picture=p.XNGYP_Products.picture,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                INVId = p.INVId,
                                INVName = p.INV_Name.Name,
                                InputDateTime = p.InputDateTime,
                                SN = p.SN,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                WoodId=p.WoodId,
                                ColorId=p.ColorId,
                                Status=p.Status,
                                flag=p.Flag,
                                Grade = p.Grade,
                                ProductSN=p.ProductSN,
                                FatherId=p.FatherId,
                                CreateTime=p.CreateTime,
                                CCprice=p.CCprice,
                                BQPrice=p.BQPrice,
                            }).ToList();
                return List;
            }
        }
        public AdminModel GetLabelsCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            AdminModel Models = new AdminModel();
            using (var db = new XNGYPEntities())
            {
                Models.TotalCount = db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Status == 1).Count();
                Models.YearCount = db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false &&  k.CreateTime > YearStartTime && k.CreateTime < YearEndTime).Count();
                Models.MonthCount = db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false  && k.CreateTime > MonthStartTime && k.CreateTime < MonthEndTime).Count();
                Models.WeekCount = db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false  && k.CreateTime > WeekStartTime && k.CreateTime < WeekEndTime).Count();
                Models.TodayCount = db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false  && k.CreateTime > StartTime).Count();
            }
            return Models;
        }
        public List<LabelsModel> GetFLabelsList(SLabelsModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.INV_labels.Where(k => k.delete_flag == false && k.flag>0 && k.status==1)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.SYS_product.product_SN_id : true
                            where SModel.INVId > 0 ? SModel.INVId == p.inv_id : true
                            where SModel.WoodId > 0 ? SModel.WoodId == p.wood_id : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new LabelsModel
                            {
                                Id = p.id,
                                ProductId = p.product_id,
                                ProductName = p.SYS_product.name,
                                Picture=p.SYS_product.picture,
                                ProductXL = p.SYS_product.SYS_product_SN.name,
                                Color = p.color,
                                WoodName = p.INV_wood_type.name,
                                INVId = p.inv_id,
                                INVName = p.INV_inventories.name,
                                InputDateTime = p.InputDateTime,
                                SN = p.SN,
                                Style = p.style,
                                ProductSN = p.product_SN,
                                CreateTime = p.created_time,
                                CCprice=p.CCprice,
                                BQPrice = p.BQPrice,
                            }).ToList();
                return List;
            }
        }
        public AdminModel GetFLabelsCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            AdminModel Models = new AdminModel();
            using (var db = new XNERPEntities())
            {
                Models.TotalCount = db.INV_labels.Where(k => k.delete_flag == false &&  k.status == 1).Count();
                Models.YearCount = db.INV_labels.Where(k => k.delete_flag == false && k.flag > 0 && k.status == 1 && k.created_time > YearStartTime && k.created_time < YearEndTime).Count();
                Models.MonthCount = db.INV_labels.Where(k => k.delete_flag == false && k.flag > 0 && k.status == 1 && k.created_time > MonthStartTime && k.created_time < MonthEndTime).Count();
                Models.WeekCount = db.INV_labels.Where(k => k.delete_flag == false && k.flag > 0 && k.status == 1 && k.created_time > WeekStartTime && k.created_time < WeekEndTime).Count();
                Models.TodayCount = db.INV_labels.Where(k => k.delete_flag == false && k.flag > 0 && k.status == 1 && k.created_time > StartTime).Count();
            }
            return Models;
        }
        public void AddOrUpdate(LabelsModel Models)
        {
            Random r = new Random();
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_INV_Labels.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.ProductsId = Models.ProductId;
                    table.ProductsSNId = Models.ProductSNId;
                    table.Length = Models.Length;
                    table.Width = Models.Width;
                    table.Height = Models.Height;
                    table.INVId = Models.INVId;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.ColorId = Models.ColorId;
                    table.Color = Models.Color;
                    table.CreateTime = DateTime.Now;
                    table.Grade = Models.Grade;
                    table.FatherId = Models.FatherId;
                    table.ProductSN = Models.ProductXL + Models.ProductSN + Models.WoodNameXL + Models.Grade+ r.Next(100, 1000);
                }
                else
                {
                    for (int i = 0; i < Models.qty; i++)
                    {
                        XNGYP_INV_Labels table = new XNGYP_INV_Labels();
                        table.SN= "XN" + GenerateTimeStamp()+i+"_"+Models.Grade;
                        table.ProductsId = Models.ProductId;
                        table.ProductsSNId = Models.ProductSNId;
                        table.Length = Models.Length;
                        table.Width = Models.Width;
                        table.Height = Models.Height;
                        table.INVId = Models.INVId;
                        table.WoodId = Models.WoodId;
                        table.WoodName = Models.WoodName;
                        table.ColorId = Models.ColorId;
                        table.Color = Models.Color;
                        table.Company = "上海香凝工艺品有限公司";
                        table.Address = "上海市青浦区朱家角朱枫公路1355号";
                        table.WebSite = "www.xiangninghm.com";
                        table.Status = 1;
                        table.CreateTime = DateTime.Now;
                        table.InputDateTime = DateTime.Now;
                        table.DeleteFlag = false;
                        table.Flag = 3;
                        table.ContractDetailId = 0;
                        table.WIPContractIid = 0;
                        table.Grade = Models.Grade;
                        table.FatherId = Models.FatherId;
                        table.ProductSN = Models.ProductXL+Models.ProductSN+Models.WoodNameXL+Models.Grade+ i.ToString("000");
                        db.XNGYP_INV_Labels.Add(table);
                    }
                }
                db.SaveChanges();
            }
        }
        public void Edit(LabelsModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_INV_Labels.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.Length = Models.Length;
                    table.Width = Models.Width;
                    table.Height = Models.Height;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.ColorId = Models.ColorId;
                    table.Color = Models.Color;
                    table.Grade = Models.Grade;
                    table.ProductSN = table.XNGYP_Products_SN.SN + table.XNGYP_Products_SN1.SN + new ContractHeaderDal().GetWoodSN(Models.WoodId.Value) + Models.Grade;
                }
                db.SaveChanges();
            }
        }
        public void EditCost(LabelsModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_INV_Labels.Where(k => k.Id == Models.Id).SingleOrDefault();
                    if (Models.BQPrice > Models.CCprice)
                    {
                        table.BQPrice = Models.BQPrice;
                    }
                }
                db.SaveChanges();
            }
        }
        public void EditF(LabelsModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.INV_labels.Where(k => k.id == Models.Id).SingleOrDefault();
                    if (Models.BQPrice > Models.CCprice)
                    {
                        table.BQPrice = Models.BQPrice;
                    }
                }
                db.SaveChanges();
            }
        }
        public LabelsModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_INV_Labels.Where(k => k.Id == Id)
                              select new LabelsModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductsId,
                                  ProductName = p.XNGYP_Products.name,
                                  ProductSNId = p.ProductsSNId,
                                  Color = p.Color,
                                  WoodName = p.WoodName,
                                  INVId = p.INVId,
                                  INVName = p.INV_Name.Name,
                                  InputDateTime = p.InputDateTime,
                                  SN = p.SN,
                                  Length = p.Length,
                                  Width = p.Width,
                                  Height = p.Height,
                                  WoodId = p.WoodId,
                                  ColorId = p.ColorId,
                                  Grade = p.Grade,
                                  ProductSN = p.ProductSN,
                                  FatherId = p.FatherId,
                                  CCprice = p.CCprice,
                                  BQPrice = p.BQPrice,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public LabelsModel GetFDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.INV_labels.Where(k => k.id == Id)
                              select new LabelsModel
                              {
                                  Id = p.id,
                                  ProductId = p.product_id,
                                  ProductName = p.SYS_product.name,
                                  Picture = p.SYS_product.picture,
                                  ProductXL = p.SYS_product.SYS_product_SN.name,
                                  Color = p.color,
                                  WoodName = p.INV_wood_type.name,
                                  INVId = p.inv_id,
                                  INVName = p.INV_inventories.name,
                                  InputDateTime = p.InputDateTime,
                                  SN = p.SN,
                                  Style = p.style,
                                  ProductSN = p.product_SN,
                                  CreateTime = p.created_time,
                                  CCprice = p.CCprice,
                                  BQPrice = p.BQPrice,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public void DeleteMore(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Labels.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        //移库操作
        public void MoveINV(string ListId,int INVId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Labels.Where(k => k.Id == Id).SingleOrDefault();
                        tables.INVId = INVId;
                    }
                }
                db.SaveChanges();
            }
        }
        public DataTable ToExcelOut(SLabelsModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime).AddDays(-1);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductsSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? SModel.FatherId == p.FatherId : true
                            where SModel.INVId > 0 ? SModel.INVId == p.INVId : true
                            where !string.IsNullOrEmpty(SModel.ProductName) ? p.XNGYP_Products.name.Contains(SModel.ProductName) : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new LabelsModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductsId,
                                ProductName = p.XNGYP_Products.name,
                                ProductXL = p.XNGYP_Products_SN.name,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                INVId = p.INVId,
                                INVName = p.INV_Name.Name,
                                InputDateTime = p.InputDateTime,
                                SN = p.SN,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                WoodId = p.WoodId,
                                ColorId = p.ColorId,
                                Status = p.Status,
                                flag = p.Flag,
                                Grade = p.Grade,
                                ProductSN = p.ProductSN,
                                FatherId = p.FatherId,
                            }).ToList();
            if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("标签编码", typeof(string));
                    Exceltable.Columns.Add("产品编码", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("长", typeof(string));
                    Exceltable.Columns.Add("宽", typeof(string));
                    Exceltable.Columns.Add("高", typeof(string));
                    Exceltable.Columns.Add("所入仓库", typeof(string));
                    Exceltable.Columns.Add("进库日期", typeof(string));
                    Exceltable.Columns.Add("状态", typeof(string));
                    Exceltable.Columns.Add("所属方式", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["标签编码"] = item.SN;
                        row["产品编码"] = item.ProductSN;
                        row["产品名称"] = item.ProductName;
                        row["材质"] = item.WoodName;
                        row["色号"] = item.Color;
                        row["长"] = item.Length;
                        row["宽"] = item.Width;
                        row["高"] = item.Height;
                        row["所入仓库"] = item.INVName;
                        row["进库日期"] = Convert.ToDateTime(item.InputDateTime).ToString("yyyy-MM-dd"); ;
                        row["状态"] = item.Status != null && item.Status == 2 ? "已出库" : item.Status == 1 ? "已入库" : "未确认";
                        row["所属方式"] = item.flag != null && item.flag == 1 ? "销售产品" : item.flag != null && item.flag == 2 ? "预投产品" : "盘点产品";

                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public List<LabelsModel> GetWorkLabelsList(SLabelsModel SModel)
        {

            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Flag != 1 && k.Status==1)
                            where SModel.WoodId != null && SModel.WoodId > 0 ? SModel.WoodId == p.WoodId : true
                            where SModel.ProductId != null && SModel.ProductId > 0 ? p.ProductsId == SModel.ProductId : true
                            orderby p.InputDateTime descending
                            select new LabelsModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductsId,
                                ProductName = p.XNGYP_Products.name,
                                ProductSN = p.ProductSN,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                INVId = p.INVId,
                                INVName = p.INV_Name.Name,
                                InputDateTime = p.InputDateTime,
                                SN = p.SN,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                WoodId = p.WoodId,
                                ColorId = p.ColorId,
                                Status = p.Status,
                                flag = p.Flag,
                                Grade = p.Grade,
                            }).ToList();
                return List;
            }
        }
        //绑定合同操作
        public void BindLabels(string ListId, int CRM_Id)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                int UpdateCount = ArrId.Count() - 1;
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Labels.Where(k => k.Id == Id).SingleOrDefault();
                        tables.ContractDetailId = CRM_Id;
                        tables.Flag = 1;
                        tables.WIPContractIid = null;
                    }
                }
                int LabelsCount = UpdateCount;
                int qty = 1;
                var CRMTables = db.Contract_Detail.Where(k => k.Id == CRM_Id).SingleOrDefault();
                CRMTables.LabelseCount = UpdateCount+ CRMTables.LabelseCount??0;
                qty = CRMTables.Qty??0;
                if ((qty - CRMTables.LabelseCount) <= 0)//判断是否已经没了
                {
                    CRMTables.Status = 4;
                }
                db.SaveChanges();
            }
        }
        public void CheckMore(string ListId, int InvId,int Grade)
        {
            using (var db = new XNGYPEntities())
            {
                int i = 1;
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Labels.Where(k => k.Id == Id).SingleOrDefault();
                        tables.INVId = InvId;
                        tables.Status = 1;
                        tables.InputDateTime = DateTime.Now;
                        tables.CheckDate = DateTime.Now;
                        tables.ProductSN = tables.XNGYP_Products_SN.SN + tables.XNGYP_Products_SN1.SN + new ContractHeaderDal().GetWoodSN(tables.WoodId.Value) + Grade;
                        tables.SN = "XN" + new LabelsDal().GenerateTimeStamp() + i+ "_" + Grade;
                        tables.Grade = Grade;
                        int CRMId = tables.ContractDetailId ?? 0;
                        int WIPId = tables.WIPContractIid ?? 0;
                        //判断是否是销售产品、预投产品，然后设置状态
                        if (CRMId > 0)
                        {
                            var CRMTable = db.Contract_Detail.Where(k => k.Id == CRMId).FirstOrDefault();
                            if (CRMTable != null)
                            {
                                CRMTable.Status = 4;
                            }
                        }
                        if (WIPId > 0)
                        {
                            var WIPTable = db.XNGYP_WIP_PreCast.Where(k => k.Id == WIPId).FirstOrDefault();
                            if (WIPTable != null)
                            {
                                WIPTable.Staute = 4;
                            }
                        }
                        int WorkOrderId = tables.WorkOrderId ?? 0;
                        var Worktable = db.XNGYP_WorkOrder.Where(k => k.Id == WorkOrderId).FirstOrDefault();
                        if (Worktable != null)
                        {
                            Worktable.ClosedFlag = true;//关闭工单
                        }
                        i++;

                      new CostDal().AddGYPCostNew(Id);
                    }
                }
                db.SaveChanges();
            }
        }
        //送货维修操作
        public void DeliveryMore(string ListId)
        {
            Random r = new Random();
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int CRMId = 0;
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Labels.Where(k => k.Id == Id).SingleOrDefault();
                        CRMId = tables.ContractDetailId ?? 0;
                        if (CRMId > 0)
                        {
                            XNGYP_Delivery HTables = new XNGYP_Delivery();
                            HTables.CDeatailId = tables.ContractDetailId;
                            HTables.OperatorId = new UserDal().GetCurrentUserName().UserId;
                            HTables.Operator= new UserDal().GetCurrentUserName().UserName;
                            HTables.LabelsId = Id;
                            HTables.CreateTime = DateTime.Now;
                            HTables.DeliverTime = DateTime.Now;
                            HTables.DeleteFlag = false;
                            HTables.Status = 0;
                            db.XNGYP_Delivery.Add(HTables);

                            tables.OutDate = DateTime.Now;
                            tables.OutUserId= HTables.OperatorId;
                            tables.OutUserName = HTables.Operator;
                            tables.Status = 9;

                            var CRMTable = db.Contract_Detail.Where(k => k.Id == CRMId).FirstOrDefault();
                            CRMTable.Status = 5;
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        
    }
}
