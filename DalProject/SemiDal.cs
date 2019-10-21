using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class SemiDal
    {
        public string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public List<SemiModel> GetSemiList(SSemiModel SModel)
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
                var List = (from p in db.XNGYP_INV_Semi.Where(k => k.DeleteFlag == false && k.Status<=1)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? SModel.FatherId == p.FatherId : true
                            where SModel.INVId > 0 ? SModel.INVId == p.INVId : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new SemiModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                ProductSN = p.ProductSN,
                                Color = p.ColorName,
                                WoodName = p.WoodName,
                                INVId = p.INVId,
                                INVName = p.INV_Name.Name,
                                InputDateTime = p.InputDate,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                WoodId = p.WoodId,
                                ColorId = p.ColorId,
                                Status = p.Status,
                                flag = p.Flag,
                                Grade = p.Grade,
                                FatherId = p.FatherId,
                            }).ToList();
                return List;
            }
        }
        public void AddOrUpdate(SemiModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_INV_Semi.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.ProductId = Models.ProductId;
                    table.ProductSNId = Models.ProductSNId;
                    table.Length = Models.Length;
                    table.Width = Models.Width;
                    table.Height = Models.Height;
                    table.INVId = Models.INVId;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.ColorId = Models.ColorId;
                    table.ColorName = Models.Color;
                    table.CreateTime = DateTime.Now;
                    table.Grade = Models.Grade;
                    table.FatherId = Models.FatherId;
                    table.ProductSN = Models.ProductXL + Models.ProductSN + Models.WoodNameXL;
                }
                else
                {
                    for (int i = 0; i < Models.qty; i++)
                    {
                        XNGYP_INV_Semi table = new XNGYP_INV_Semi();
                        table.ProductId = Models.ProductId;
                        table.ProductSNId = Models.ProductSNId;
                        table.Length = Models.Length;
                        table.Width = Models.Width;
                        table.Height = Models.Height;
                        table.INVId = Models.INVId;
                        table.WoodId = Models.WoodId;
                        table.WoodName = Models.WoodName;
                        table.ColorId = Models.ColorId;
                        table.ColorName = Models.Color;
                        table.Status = 1;
                        table.CreateTime = DateTime.Now;
                        table.InputDate = DateTime.Now;
                        table.DeleteFlag = false;
                        table.Flag = 3;
                        table.Grade = Models.Grade;
                        table.FatherId = Models.FatherId;
                        table.ProductSN = Models.ProductXL + Models.ProductSN + Models.WoodNameXL;
                        db.XNGYP_INV_Semi.Add(table);
                    }
                }
                db.SaveChanges();
            }
        }
        public SemiModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_INV_Semi.Where(k => k.Id == Id)
                              select new SemiModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductId,
                                  ProductName = p.ProductName,
                                  ProductSNId = p.ProductSNId,
                                  Color = p.ColorName,
                                  WoodName = p.WoodName,
                                  INVId = p.INVId,
                                  INVName = p.INV_Name.Name,
                                  InputDateTime = p.InputDate,
                                  Length = p.Length,
                                  Width = p.Width,
                                  Height = p.Height,
                                  WoodId = p.WoodId,
                                  ColorId = p.ColorId,
                                  Grade = p.Grade,
                                  ProductSN = p.ProductSN,
                                  FatherId = p.FatherId,
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
                        var tables = db.XNGYP_INV_Semi.Where(k => k.Id == Id).SingleOrDefault();
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
                        var tables = db.XNGYP_INV_Semi.Where(k => k.Id == Id).SingleOrDefault();
                        tables.INVId = INVId;
                    }
                }
                db.SaveChanges();
            }
        }
        public DataTable ToExcelOut(SSemiModel SModel)
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
                var List = (from p in db.XNGYP_INV_Semi.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? SModel.FatherId == p.FatherId : true
                            where SModel.INVId > 0 ? SModel.INVId == p.INVId : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new SemiModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                Color = p.ColorName,
                                WoodName = p.WoodName,
                                INVId = p.INVId,
                                INVName = p.INV_Name.Name,
                                InputDateTime = p.InputDate,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                WoodId = p.WoodId,
                                ColorId = p.ColorId,
                                Status = p.Status,
                                flag = p.Flag,
                                Grade = p.Grade,
                                ProductSN = p.ProductSN,
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
        //半成品审核
        public void CheckMore(string ListId, int InvId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_INV_Semi.Where(k => k.Id == Id).SingleOrDefault();
                        tables.INVId = InvId;
                        tables.Status = 1;
                        tables.InputUserId = new UserDal().GetCurrentUserName().UserId;
                        tables.InputUserName= new UserDal().GetCurrentUserName().UserName;
                        tables.InputDate = DateTime.Now;
                        tables.ProductSN = tables.XNGYP_Products_SN.SN + tables.XNGYP_Products_SN1.SN + new ContractHeaderDal().GetWoodSN(tables.WoodId.Value);

                        int CRMId = tables.CDetailId??0;
                        int WIPId = tables.WPCastId??0;
                        //判断是否是销售产品、预投产品，然后设置状态
                        if (CRMId > 0)
                        {
                            var CRMTable = db.Contract_Detail.Where(k => k.Id == CRMId).FirstOrDefault();
                            if (CRMTable != null)
                            {
                                CRMTable.Status = 3;
                            }
                        }
                        if (WIPId > 0)
                        { var WIPTable= db.XNGYP_WIP_PreCast.Where(k => k.Id == WIPId).FirstOrDefault();
                            if (WIPTable != null)
                            {
                                WIPTable.Staute = 3;
                            }
                        }
                        int WorkOrderId = tables.WorkOrderId??0;
                        var Worktable = db.XNGYP_WorkOrder.Where(k => k.Id == WorkOrderId).FirstOrDefault();
                        if (Worktable != null)
                        {
                            Worktable.ClosedFlag = true;//关闭工单
                        }

                    }
                }
                db.SaveChanges();
            }
        }
        //安排生产
        public void AddWork(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var STables = db.XNGYP_INV_Semi.Where(k => k.Id == Id).SingleOrDefault();
                        STables.OutDate = DateTime.Now;
                        STables.OutUserId = new UserDal().GetCurrentUserName().UserId;
                        STables.OutUserName = new UserDal().GetCurrentUserName().UserName;
                        STables.Status = 2;
                        int WorkOrderId = STables.WorkOrderId??0;
                        var Worktable = db.XNGYP_WorkOrder.Where(k => k.Id == WorkOrderId).FirstOrDefault();
                        if (Worktable != null)
                        {
                            Worktable.ClosedFlag = false;//开启工单
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
