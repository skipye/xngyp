﻿using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class DeliveryDal
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
                            }).ToList();
                return List;
            }
        }
        public void AddOrUpdate(LabelsModel Models)
        {
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
                    table.ProductSN = Models.ProductXL + Models.ProductSN + Models.WoodNameXL + Models.Grade;
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
                        table.ProductSN = Models.ProductXL+Models.ProductSN+Models.WoodNameXL+Models.Grade;
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
                        var tables = db.XNGYP_Delivery.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                        int LablesId = tables.LabelsId??0;
                        if (LablesId > 0)
                        {
                            var LaTable = db.XNGYP_INV_Labels.Where(k => k.Id == LablesId).FirstOrDefault();
                            LaTable.Status = 1;
                            LaTable.OutDate = DateTime.Now;
                        }
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
                var List = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Flag != 2 && k.Status==1)
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
                CRMTables.LabelseCount = UpdateCount;
                qty = CRMTables.Qty??0;
                if ((qty - LabelsCount) <= 0)//判断是否已经没了
                {
                    CRMTables.Status = 4;
                }
                db.SaveChanges();
            }
        }
        public void CheckMore(string ListId, string OrderNum,DateTime DeliveryTime)
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
                        var tables = db.XNGYP_Delivery.Where(k => k.Id == Id).SingleOrDefault();
                        tables.OrderNum = OrderNum;
                        tables.Status = 1;
                        tables.DeliverTime = DeliveryTime;
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
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        //送货列表
        public List<LabelsModel> GetDeliveryList(SLabelsModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Delivery.Where(k => k.DeleteFlag==false)
                            where !string.IsNullOrEmpty(SModel.CustomerName) ? p.Contract_Detail.Contract_Header.XNGYP_Customers.Name.Contains(SModel.CustomerName) : true
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.Contract_Detail.Contract_Header.SN.Contains(SModel.HTSN) : true
                            where SModel.INVId != null && SModel.INVId > 0 ? SModel.INVId == p.XNGYP_INV_Labels.INVId : true
                            where p.DeliverTime > StartTime
                            where p.DeliverTime < EndTime
                            orderby p.CreateTime descending
                            select new LabelsModel
                            {
                                Id = p.Id,
                                CRM_SN = p.Contract_Detail.Contract_Header.SN,
                                CRM_HTId = p.Contract_Detail.Contract_Header.Id,
                                ProductName = p.XNGYP_INV_Labels.XNGYP_Products.name,
                                ProductXL = p.XNGYP_INV_Labels.XNGYP_Products.XNGYP_Products_SN.name,
                                WoodName = p.XNGYP_INV_Labels.WoodName,
                                INVId = p.XNGYP_INV_Labels.INVId,
                                INVName = p.XNGYP_INV_Labels.INV_Name.Name,
                                CreateTime = p.CreateTime,
                                Color = p.XNGYP_INV_Labels.Color,
                                CustomerName = p.Contract_Detail.Contract_Header.XNGYP_Customers.Name,
                                OrderNum = p.OrderNum,
                                DeliveryTime = p.DeliverTime,
                                Status=p.Status,
                            }).ToList();
                return List;
            }
        }
    }
}
