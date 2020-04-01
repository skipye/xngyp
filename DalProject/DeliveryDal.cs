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
                var List = (from p in db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status>=1)
                            where !string.IsNullOrEmpty(SModel.CustomerName) ? p.Contract_Detail.Contract_Header.XNGYP_Customers.Name.Contains(SModel.CustomerName) : true
                            where !string.IsNullOrEmpty(SModel.HTSN) ? p.Contract_Detail.Contract_Header.SN.Contains(SModel.HTSN) : true
                            where SModel.INVId != null && SModel.INVId > 0 ? SModel.INVId == p.XNGYP_INV_Labels.INVId : true
                            where SModel.Status != null && SModel.Status > 0 ? SModel.Status == p.Status : true
                            where p.DeliverTime > StartTime
                            where p.DeliverTime < EndTime
                            orderby p.CreateTime descending
                            select new LabelsModel
                            {
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
                                Status = p.Status,
                                SalePrice=p.Contract_Detail.Price,
                                WoodId=p.XNGYP_INV_Labels.WoodId,
                                ProductId=p.XNGYP_INV_Labels.ProductsId,
                            }).ToList();
                if (List != null && List.Any())
                {
                    
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("订货人", typeof(string));
                    Exceltable.Columns.Add("产品名称", typeof(string));
                    Exceltable.Columns.Add("送货单号", typeof(string));
                    Exceltable.Columns.Add("材质", typeof(string));
                    Exceltable.Columns.Add("色号", typeof(string));
                    Exceltable.Columns.Add("所出仓库", typeof(string));
                    Exceltable.Columns.Add("送货日期", typeof(string));
                    Exceltable.Columns.Add("出售价格", typeof(string));
                    Exceltable.Columns.Add("人工成本", typeof(string));
                    Exceltable.Columns.Add("材料成本", typeof(string));
                    Exceltable.Columns.Add("辅料成本", typeof(string));
                    Exceltable.Columns.Add("总成本", typeof(string));
                    Exceltable.Columns.Add("毛利", typeof(string));
                    foreach (var item in List)
                    {
                        var n = (from p in db.XNGYP_Products_Price.Where(k => k.WoodId == item.WoodId && k.ProductId == item.ProductId)
                                 select new CostModel
                                 {
                                     PersonPrice= p.KLPrice + p.DHPrice + p.MGQPrice + p.MGPrice + p.GMPrice + p.YQPrice + p.PJPrice,
                                     MCPrice = p.MCPrice,
                                     FLPrice = p.FLPrice,
                                     CostCprice = p.CostCprice,
                                 }).FirstOrDefault();
                        if (n == null)
                        {
                            n = new CostModel();
                        }
                        DataRow row = Exceltable.NewRow();
                        row["合同编号"] = item.CRM_SN;
                        row["订货人"] = item.CustomerName;
                        row["产品名称"] = item.ProductName;
                        row["送货单号"] = item.OrderNum;
                        row["材质"] = item.WoodName;
                        row["色号"] = item.Color;
                        row["所出仓库"] = item.INVName;
                        row["送货日期"] = Convert.ToDateTime(item.DeliveryTime).ToString("yyyy-MM-dd"); 
                        row["出售价格"] = item.SalePrice;
                        row["人工成本"] = n.PersonPrice;
                        row["材料成本"] = n.MCPrice;
                        row["辅料成本"] = n.FLPrice;
                        row["总成本"] = n.CostCprice;
                        row["毛利"] = item.SalePrice - n.CostCprice;

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
        //工艺品送货列表
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
                            where SModel.Status != null && SModel.Status > 0 ? SModel.Status == p.Status : true
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
        //家具送货列表
        public List<LabelsModel> GetFDeliveryList(SLabelsModel SModel)
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
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_delivery_tmp_header.Where(k => k.status == false && k.delete_flag==true)
                            where p.DeliverTime > StartTime
                            where p.DeliverTime < EndTime
                            orderby p.DeliverTime descending
                            select new LabelsModel
                            {
                                Id = p.id,
                                CRM_SN = p.CRM_contract_detail.CRM_contract_header.SN,
                                ProductName = p.CRM_contract_detail.SYS_product.name,
                                ProductXL = p.CRM_contract_detail.SYS_product.SYS_product_SN.name,
                                WoodName = p.CRM_contract_detail.INV_wood_type.name,
                                CustomerName = p.CRM_contract_detail.CRM_contract_header.CRM_customers.name,
                                OrderNum = p.OrderNum,
                                DeliveryTime = p.DeliverTime,
                            }).ToList();
                return List;
            }
        }
        //打印送货单
        public ContractHeaderModel PrintDelivery(string ListId)
        {
            ContractHeaderModel Models = new ContractHeaderModel();
            using (var db = new XNGYPEntities())
            {
                List<DeliveryModel> ListD = new List<DeliveryModel>();
                string[] ArrId = ListId.Split('$');
                int j = 1;
                List<string> list = new List<string>();
                for (int i = 0; i < ArrId.Length; i++)//遍历数组成员
                {
                    if (list.IndexOf(ArrId[i].ToLower()) == -1)//对每个成员做一次新数组查询如果没有相等的则加到新数组
                        list.Add(ArrId[i]);
                }
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_Delivery.Where(k => k.Id == Id).SingleOrDefault();

                        int HeadId = tables.Contract_Detail.ContractHeadId??0;
                        int DetailId = tables.CDeatailId??0;

                        Models.Customer = tables.Contract_Detail.Contract_Header.DeliveryLinkMan;
                        Models.DeliveryAddress = tables.Contract_Detail.Contract_Header.DeliveryAddress;
                        Models.SN = tables.Contract_Detail.Contract_Header.SN;
                        Models.TelPhone = tables.Contract_Detail.Contract_Header.DeliveryLinkTel;
                        Models.OrderMun = tables.OrderNum;
                        Models.DeliveryDate = tables.Contract_Detail.Contract_Header.DeliveryDate;

                        var OldCount = ListD.Where(k => k.HTHeadId == HeadId && k.HTDetailId == DetailId).SingleOrDefault();//每次查询，重复的不要算进去

                        if (OldCount == null)
                        {
                            DeliveryModel DeModel = new DeliveryModel();
                            DeModel.productName = tables.XNGYP_INV_Labels.XNGYP_Products.name;
                            DeModel.productXL = tables.XNGYP_INV_Labels.XNGYP_Products.XNGYP_Products_SN.name;
                            DeModel.woodName = tables.XNGYP_INV_Labels.WoodName;
                            DeModel.length = tables.XNGYP_INV_Labels.Length;
                            DeModel.width = tables.XNGYP_INV_Labels.Width;
                            DeModel.height = tables.XNGYP_INV_Labels.Height;
                            DeModel.qty = j;
                            DeModel.HTHeadId = HeadId;
                            DeModel.HTDetailId = DetailId;
                            ListD.Add(DeModel);
                            
                        }
                        else { OldCount.qty = j + 1; }
                    }
                }
                Models.DePro = ListD;
            }
            return Models;
        }

        //获取当前家具送货的情况
        public AdminModel GetFDeliveCount()
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
                Models.TotalCount = db.CRM_delivery_tmp_header.Where(k => k.delete_flag == true && k.status == false).Count();
                Models.YearCount = db.CRM_delivery_tmp_header.Where(k => k.delete_flag == true && k.status == false && k.DeliverTime > YearStartTime && k.DeliverTime<YearEndTime).Count();
                Models.MonthCount = db.CRM_delivery_tmp_header.Where(k => k.delete_flag == true && k.status == false && k.DeliverTime > MonthStartTime && k.DeliverTime < MonthEndTime).Count();
                Models.WeekCount = db.CRM_delivery_tmp_header.Where(k => k.delete_flag == true && k.status == false && k.DeliverTime > WeekStartTime && k.DeliverTime < WeekEndTime).Count();
                Models.TodayCount = db.CRM_delivery_tmp_header.Where(k => k.delete_flag == true && k.status == false && k.DeliverTime > StartTime).Count();
            }
            return Models;
        }
        //获取当前工艺品送货的情况
        public AdminModel GetDeliveCount()
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
                Models.TotalCount = db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status == 1).Count();
                Models.YearCount = db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status == 1 && k.DeliverTime > YearStartTime && k.DeliverTime < YearEndTime).Count();
                Models.MonthCount = db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status == 1 && k.DeliverTime > MonthStartTime && k.DeliverTime < MonthEndTime).Count();
                Models.WeekCount = db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status == 1 && k.DeliverTime > WeekStartTime && k.DeliverTime < WeekEndTime).Count();
                Models.TodayCount = db.XNGYP_Delivery.Where(k => k.DeleteFlag == false && k.Status == 1 && k.DeliverTime > StartTime).Count();
            }
            return Models;
        }
    }
}
