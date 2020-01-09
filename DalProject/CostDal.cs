using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class CostDal
    {
        public List<CostModel> GetPageList(SCostModel SModel)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Products_Price.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? p.XNGYP_Products.ProductsSNId == SModel.ProductSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? p.XNGYP_Products.FatherId == SModel.FatherId : true
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.Id == SModel.WoodId : true
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName=p.XNGYP_Products.name,
                                ProductSN=p.XNGYP_Products.XNGYP_Products_SN1.name,
                                WoodId = p.WoodId,
                                WoodName = p.WoodName,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGHPrice = p.MGPrice,
                                MGQPrice = p.MGQPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                PJPrice=p.PJPrice,
                                CreateTime=p.CreateTime,
                                CCprice = p.CCprice,
                                CostCprice = p.CostCprice,
                                PersonPrice = p.KLPrice + p.DHPrice + p.MGQPrice + p.MGPrice + p.GMPrice + p.YQPrice
                            }).ToList();
                
                return List;
            }
        }
        //导出工艺品成本
        public DataTable ToExcelOut(SCostModel SModel)
        {
            DataTable Exceltable = new DataTable();

            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Products_Price.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? p.XNGYP_Products.ProductsSNId == SModel.ProductSNId : true
                            where SModel.FatherId != null && SModel.FatherId > 0 ? p.XNGYP_Products.FatherId == SModel.FatherId : true
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.Id == SModel.WoodId : true
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.XNGYP_Products.name,
                                ProductSN = p.XNGYP_Products.XNGYP_Products_SN1.name,
                                WoodId = p.WoodId,
                                WoodName = p.WoodName,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGHPrice = p.MGPrice,
                                MGQPrice = p.MGQPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                PJPrice = p.PJPrice,
                                CreateTime = p.CreateTime,
                                CCprice = p.CCprice,
                                CostCprice = p.CostCprice,
                                PersonPrice = p.KLPrice + p.DHPrice + p.MGQPrice + p.MGPrice + p.GMPrice + p.YQPrice
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("系列", typeof(string));
                    Exceltable.Columns.Add("产品(ID)", typeof(string));
                    Exceltable.Columns.Add("材质(ID)", typeof(string));
                    Exceltable.Columns.Add("木材成本(元)", typeof(string));
                    Exceltable.Columns.Add("辅料成本(元)", typeof(string));
                    Exceltable.Columns.Add("开料成本", typeof(string));
                    Exceltable.Columns.Add("木工前段", typeof(string));
                    Exceltable.Columns.Add("雕花成本", typeof(string));
                    Exceltable.Columns.Add("木工后段", typeof(string));
                    Exceltable.Columns.Add("刮磨成本", typeof(string));
                    Exceltable.Columns.Add("油漆成本", typeof(string));
                    Exceltable.Columns.Add("人工成本(元)", typeof(string));
                    Exceltable.Columns.Add("总成本(元)", typeof(string));
                    Exceltable.Columns.Add("出厂价(元)", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["系列"] = item.ProductSN;
                        row["产品(ID)"] = item.ProductName + "(" + item.ProductId + ")";
                        row["材质(ID)"] = item.WoodName + "(" + item.WoodId + ")";
                        row["木材成本(元)"] = item.MCPrice;
                        row["辅料成本(元)"] = item.FLPrice;
                        row["开料成本"] = item.KLPrice;
                        row["木工前段"] = item.MGQPrice;
                        row["雕花成本"] = item.DHPrice;
                        row["木工后段"] = item.MGHPrice;
                        row["刮磨成本"] = item.GMPrice;
                        row["油漆成本"] = item.YQPrice;
                        row["人工成本(元)"] = item.PersonPrice;
                        row["总成本(元)"] = item.CostCprice;
                        row["出厂价(元)"] = item.CCprice;
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public void AddOrUpdate(CostModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Products_Price.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.PJPrice = Models.PJPrice;
                    table.MGQPrice = Models.MGQPrice;

                    table.CostCprice = table.MCPrice + table.KLPrice + table.DHPrice + table.MGPrice + table.GMPrice + table.YQPrice + table.FLPrice + table.MGQPrice + table.PJPrice;
                    table.CCprice = table.CostCprice * Convert.ToDecimal(1.6);

                    var GYPLables = db.XNGYP_INV_Labels.Where(k => k.ProductsId == table.ProductId && k.WoodId == table.WoodId).ToList();
                    if (GYPLables != null && GYPLables.Any())
                    {
                        foreach (var item in GYPLables)
                        {
                            item.CCprice = table.CCprice;
                            item.BQPrice = table.CCprice * Convert.ToDecimal(1.8);
                        }
                    }
                }
                else
                {
                    var OldCost = db.XNGYP_Products_Price.Where(k => k.ProductId == Models.ProductId && k.WoodId == Models.WoodId && k.DeleteFlag == false).SingleOrDefault();
                    if (OldCost != null) { return; }

                    XNGYP_Products_Price table = new XNGYP_Products_Price();
                    table.ProductId = Models.ProductId??0;
                    table.WoodId = Models.WoodId ?? 0;
                    table.WoodName = Models.WoodName;
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.PJPrice = Models.PJPrice;
                    table.MGQPrice = Models.MGQPrice;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    table.CostCprice = table.MCPrice + table.KLPrice + table.DHPrice + table.MGPrice + table.GMPrice + table.YQPrice + table.FLPrice + table.MGQPrice;
                    table.CCprice = table.CostCprice * Convert.ToDecimal(1.6);

                    var GYPLables = db.XNGYP_INV_Labels.Where(k => k.ProductsId == table.ProductId && k.WoodId == table.WoodId).ToList();
                    if (GYPLables != null && GYPLables.Any())
                    {
                        foreach (var item in GYPLables)
                        {
                            item.CCprice = table.CCprice;
                            item.BQPrice = table.CCprice * Convert.ToDecimal(1.8);
                        }
                    }

                    db.XNGYP_Products_Price.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public CostModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Products_Price.Where(k => k.Id == Id)
                              select new CostModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductId,
                                  WoodId = p.WoodId,
                                  WoodName = p.WoodName,
                                  MCPrice = p.MCPrice,
                                  KLPrice = p.KLPrice,
                                  DHPrice = p.DHPrice,
                                  MGQPrice = p.MGQPrice,
                                  MGHPrice=p.MGPrice,
                                  GMPrice = p.GMPrice,
                                  YQPrice = p.YQPrice,
                                  FLPrice = p.FLPrice,
                                  PJPrice=p.PJPrice,
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
                        var tables = db.XNGYP_Products_Price.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public List<CostModel> GetFPageList(SCostModel SModel)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SYS_product_Cost.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? p.SYS_product.product_SN_id == SModel.ProductSNId : true
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.WoodId == SModel.WoodId : true
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.SYS_product.name,
                                ProductSN=p.SYS_product.SYS_product_SN.name,
                                WoodId = p.WoodId,
                                WoodName = p.INV_wood_type.name,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGQPrice = p.MGQPrice,
                                MGHPrice = p.MGHPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                PJPrice = p.PJPrice,
                                CreateTime = p.CreateTime,
                                CCprice=p.CCprice,
                                CostCprice=p.CostCprice,
                                Volume=p.SYS_product.volume,
                                PersonPrice= p.PersonPrice,
                                MCFY=p.MCFY,
                                RGFY=p.RGFY,
                            }).ToList();
                return List;
            }
        }
        //导出家具成本
        public DataTable ToFExcelOut(SCostModel SModel)
        {
            DataTable Exceltable = new DataTable();
            
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SYS_product_Cost.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? p.SYS_product.product_SN_id == SModel.ProductSNId : true
                            where SModel.WoodId != null && SModel.WoodId > 0 ? p.WoodId == SModel.WoodId : true
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.SYS_product.name,
                                ProductSN = p.SYS_product.SYS_product_SN.name,
                                WoodId = p.WoodId,
                                WoodName = p.INV_wood_type.name,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGQPrice = p.MGQPrice,
                                MGHPrice = p.MGHPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                PJPrice = p.PJPrice,
                                CreateTime = p.CreateTime,
                                CCprice = p.CCprice,
                                CostCprice = p.CostCprice,
                                Volume = p.SYS_product.volume,
                                PersonPrice = p.PersonPrice,
                                MCFY = p.MCFY,
                                RGFY = p.RGFY,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("系列", typeof(string));
                    Exceltable.Columns.Add("产品(ID)", typeof(string));
                    Exceltable.Columns.Add("材质(ID)", typeof(string));
                    Exceltable.Columns.Add("材积", typeof(string));
                    Exceltable.Columns.Add("木材成本(元)", typeof(string));
                    Exceltable.Columns.Add("辅料成本(元)", typeof(string));
                    Exceltable.Columns.Add("开料成本", typeof(string));
                    Exceltable.Columns.Add("木工前段", typeof(string));
                    Exceltable.Columns.Add("雕花成本", typeof(string));
                    Exceltable.Columns.Add("木工后段", typeof(string));
                    Exceltable.Columns.Add("刮磨成本", typeof(string));
                    Exceltable.Columns.Add("油漆成本", typeof(string));
                    Exceltable.Columns.Add("安装成本", typeof(string));
                    Exceltable.Columns.Add("人工成本(元)", typeof(string));
                    Exceltable.Columns.Add("木材费用(元)", typeof(string));
                    Exceltable.Columns.Add("人工费用(元)", typeof(string));
                    Exceltable.Columns.Add("总成本(元)", typeof(string));
                    Exceltable.Columns.Add("出厂价(元)", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["系列"] = item.ProductSN;
                        row["产品(ID)"] = item.ProductName+"("+ item.ProductId + ")";
                        row["材质(ID)"] = item.WoodName + "(" + item.WoodId + ")";
                        row["材积"] = item.Volume;
                        row["木材成本(元)"] = item.MCPrice;
                        row["辅料成本(元)"] = item.FLPrice;
                        row["开料成本"] = item.KLPrice;
                        row["木工前段"] = item.MGQPrice;
                        row["雕花成本"] = item.DHPrice;
                        row["木工后段"] = item.MGHPrice;
                        row["刮磨成本"] = item.GMPrice;
                        row["油漆成本"] = item.YQPrice;
                        row["安装成本"] = item.PJPrice;
                        row["人工成本(元)"] = item.PersonPrice;
                        row["木材费用(元)"] = item.MCFY;
                        row["人工费用(元)"] = item.RGFY;
                        row["总成本(元)"] = item.CostCprice;
                        row["出厂价(元)"] = item.CCprice;
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        //根据产品ID和材质ID获取出厂价格
        public Decimal? GetChuChangPrice(int ProductId, int WoodId)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SYS_product_Cost.Where(k => k.DeleteFlag == false && k.WoodId==WoodId && k.ProductId==ProductId)
                            select p.CCprice
                            ).FirstOrDefault();
                return List;
            }
        }
        //根据产品ID和材质ID获取工艺品出厂价格
        public Decimal? GetGYPCCPrice(int ProductId, int WoodId)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Products_Price.Where(k => k.DeleteFlag == false && k.WoodId == WoodId && k.ProductId == ProductId)
                            select p.CCprice
                            ).FirstOrDefault();
                return List;
            }
        }
        public void AddOrUpdateF(CostModel Models)
        {
            using (var db = new XNERPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.SYS_product_Cost.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGQPrice = Models.MGQPrice;
                    table.MGHPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.PJPrice = Models.PJPrice;
                    table.PersonPrice= table.KLPrice + table.DHPrice + table.MGQPrice + table.GMPrice + table.YQPrice + table.MGHPrice+table.PJPrice;
                    table.CostCprice = table.MCPrice + table.FLPrice + table.PersonPrice;
                    table.CCprice = table.CostCprice * Convert.ToDecimal(1.6);
                    table.RGFY= table.PersonPrice * Convert.ToDecimal(0.6);
                    var MCDPrice = table.INV_wood_type.prcie;
                    var ProductAreaId = table.SYS_product.product_area_id;
                    var W_BZ = table.INV_wood_type.g_bz ?? 0;
                    
                    double CCL = 0.42;
                    if (ProductAreaId == 6)
                    {
                        CCL = 0.45;
                    }
                    double Woodunit = 0;//吨，材积/出材率*比重*数量
                    Woodunit = Convert.ToDouble(table.Volume) / CCL * Convert.ToDouble(W_BZ);
                    if (MCDPrice <= 20000)
                    {
                        table.MCFY = Convert.ToDecimal(Math.Floor(Woodunit * 20000 * 0.6 / 10) * 10);

                        table.CCprice = table.MCPrice + table.PersonPrice + table.MCFY + table.RGFY;
                    }
                    else { table.MCFY = 0; }

                    var GYPLables = db.INV_labels.Where(k => k.product_id == table.ProductId && k.wood_id == table.WoodId).ToList();
                    if (GYPLables != null && GYPLables.Any())
                    {
                        foreach (var item in GYPLables)
                        {
                            item.CCprice = table.CCprice;
                            item.BQPrice = table.CCprice * Convert.ToDecimal(2.5);
                        }
                    }
                }
                else
                {
                    var OldCost = db.SYS_product_Cost.Where(k => k.ProductId == Models.ProductId && k.WoodId == Models.WoodId && k.DeleteFlag == false).FirstOrDefault();
                    if (OldCost != null) { return; }
                    SYS_product_Cost table = new SYS_product_Cost();
                    table.ProductId = Models.ProductId;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGQPrice = Models.MGQPrice;
                    table.MGHPrice = Models.MGHPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.PJPrice = Models.PJPrice;
                    table.CCprice = Models.CCprice;
                    table.CCprice = Models.CCprice;
                    table.CostCprice = Models.CostCprice;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    table.PersonPrice = Models.PersonPrice; 
                    table.CostCprice = table.MCPrice + table.PersonPrice + table.FLPrice;
                    table.CCprice = Models.CostCprice;
                    table.RGFY = Models.RGFY;
                    table.MCFY = Models.MCFY;
                    var GYPLables = db.INV_labels.Where(k => k.product_id == table.ProductId && k.wood_id == table.WoodId).ToList();
                    if (GYPLables != null && GYPLables.Any())
                    {
                        foreach (var item in GYPLables)
                        {
                            item.CCprice = table.CCprice;
                            item.BQPrice = table.CCprice * Convert.ToDecimal(2.5);
                        }
                    }
                    db.SYS_product_Cost.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public CostModel GetFDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.SYS_product_Cost.Where(k => k.Id == Id)
                              select new CostModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductId,
                                  WoodId = p.WoodId,
                                  WoodName = p.WoodName,
                                  MCPrice = p.MCPrice,
                                  KLPrice = p.KLPrice,
                                  DHPrice = p.DHPrice,
                                  MGQPrice = p.MGQPrice,
                                  MGHPrice = p.MGHPrice,
                                  GMPrice = p.GMPrice,
                                  YQPrice = p.YQPrice,
                                  FLPrice = p.FLPrice,
                                  PJPrice=p.PJPrice,
                                  CCprice = p.CCprice,
                                  CostCprice = p.CostCprice,
                              }).SingleOrDefault();
                return tables;
            }
        }

        public void DeleteFMore(string ListId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.SYS_product_Cost.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        //把库存家具的所有产品计算成本
        public void AddFCost()
        {
            using (var db = new XNERPEntities())
            {
                var LablesTable = (from p in db.INV_labels.Where(k => k.delete_flag == false && k.CCprice==null && k.status==1)
                                   orderby p.created_time descending
                                   select new LabelsModel
                                   {
                                       Id=p.id,
                                       ProductId = p.product_id,
                                       WoodId = p.wood_id,
                                       ProductAreaId = p.SYS_product.SYS_product_area.id,
                                       Volume = p.SYS_product.volume ?? 0,
                                       W_BZ = p.INV_wood_type.g_bz ?? 0,
                                       W_price = p.INV_wood_type.prcie ?? 0,
                                       cc_prcie = p.INV_wood_type.cc_prcie ?? 0,
                                       PersonPrice = p.SYS_product.reserved1 ?? 0,
                                   }).ToList();
                double CCL = 0.42;
                int OldProductId = 0;
                int OldWoodId = 0;
                foreach (var item in LablesTable)
                {
                    if (item.ProductId == OldProductId && item.WoodId == OldWoodId)
                    {
                        continue;
                    }
                    OldProductId = item.ProductId;
                    OldWoodId = item.WoodId??0;
                    if (item.ProductAreaId == 6)
                    {
                        CCL = 0.45;
                    }
                    double Woodunit = 0;//吨，材积/出材率*比重*数量
                    double WoodCB = 0;//材料单价*吨数
                    double FLCB = 0;//辅料成本，辅料成本=材料成本*0.15
                    double CB = 0;//成本=材料成本+辅料成本+人工费
                    
                    Woodunit = Convert.ToDouble(item.Volume) / CCL * Convert.ToDouble(item.W_BZ);
                    WoodCB = Woodunit * Convert.ToDouble(item.W_price);
                    FLCB = WoodCB * 0.15;
                    CB = WoodCB + FLCB + Convert.ToDouble(item.PersonPrice ?? 0);
                    

                    CostModel Costm = new CostModel();
                    Costm.ProductId = item.ProductId;
                    Costm.WoodId = item.WoodId;
                    Costm.MCPrice = Convert.ToDecimal(Convert.ToDecimal(WoodCB).ToString("00"));
                    Costm.KLPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "开料");
                    Costm.FLPrice = 0;
                    Costm.MGQPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "木工前段");
                    Costm.MGHPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "木工后段");
                    Costm.DHPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "雕花");
                    Costm.GMPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "刮磨");
                    Costm.YQPrice = GetFCost(item.ProductId, item.WoodId ?? 0, "油漆");
                    Costm.PersonPrice = Convert.ToDecimal(Convert.ToDecimal(item.PersonPrice).ToString("00"));
                    Costm.RGFY = Costm.PersonPrice * Convert.ToDecimal(0.6);
                    if (Costm.KLPrice > 0 && Costm.MGQPrice > 0 && Costm.MGHPrice > 0 && Costm.DHPrice > 0 && Costm.GMPrice > 0 && Costm.YQPrice > 0)
                    {
                        CB = WoodCB + Convert.ToDouble(Costm.KLPrice) + Convert.ToDouble(Costm.MGQPrice) + Convert.ToDouble(Costm.MGHPrice) + Convert.ToDouble(Costm.DHPrice) + Convert.ToDouble(Costm.GMPrice) + Convert.ToDouble(Costm.YQPrice);
                    }
                    Costm.CostCprice = Convert.ToDecimal(Convert.ToDecimal(CB).ToString("00"));

                    Costm.CCprice = Convert.ToDecimal(Convert.ToDecimal(CB * 1.6).ToString("00"));
                    if (item.W_price <= 20000)
                    {
                        Costm.MCFY = Convert.ToDecimal(Math.Floor(Woodunit * 20000 * 0.6 / 10) * 10);

                        Costm.CCprice = Costm.MCPrice + Costm.PersonPrice + Costm.MCFY + Costm.RGFY;
                    }
                    else { Costm.MCFY = 0; }
                    
                    AddOrUpdateF(Costm);
                }
                db.SaveChanges();
            }
        }
        //根据产品ID和材质ID获取各个阶段成本
        public static decimal GetFCost(int ProductId,int WoodId,string GX)
        {
            using (var db = new XNERPEntities())
            {
                var Costtable = (from p in db.WIP_workflow.Where(k => k.delete_flag == false)
                                 where p.Product_Id == ProductId && p.Wood_Id == WoodId
                                 where p.name.Contains(GX)
                                 select p.cost).FirstOrDefault();
                return Costtable;
            }
        }
        //根据产品ID和材质ID获取各个阶段成本
        public static decimal GetCost(int ProductId, int WoodId, string GX)
        {
            using (var db = new XNGYPEntities())
            {
                var Costtable = (from p in db.XNGYP_WorkFrom.Where(k => k.DeleteFlag == false)
                                 where p.ProductId == ProductId && p.WoodId == WoodId
                                 where p.Name.Contains(GX)
                                 select p.cost).FirstOrDefault();
                return Costtable??0;
            }
        }
        //把库存工艺品的所有产品计算成本
        public void AddGYPCostNew(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var LablesTable = (from p in db.XNGYP_INV_Labels.Where(k => k.Id == Id)
                                   orderby p.CreateTime descending
                                   select new LabelsModel
                                   {
                                       Id = p.Id,
                                       ProductId = p.ProductsId,
                                       WoodId = p.WoodId,
                                       WoodName = p.WoodName,
                                   }).ToList();
                foreach (var item in LablesTable)
                {
                    
                    CostModel Costm = new CostModel();
                    Costm.ProductId = item.ProductId;
                    Costm.WoodId = item.WoodId;
                    Costm.WoodName = item.WoodName;
                    //Costm.MCPrice = Convert.ToDecimal(Convert.ToDecimal(WoodCB).ToString("00"));
                    Costm.KLPrice = GetCost(item.ProductId, item.WoodId ?? 0, "开料");
                    Costm.FLPrice = 0;
                    Costm.MGQPrice = GetCost(item.ProductId, item.WoodId ?? 0, "木工前段");
                    Costm.MGHPrice = GetCost(item.ProductId, item.WoodId ?? 0, "木工后段");
                    Costm.DHPrice = GetCost(item.ProductId, item.WoodId ?? 0, "雕花");
                    Costm.GMPrice = GetCost(item.ProductId, item.WoodId ?? 0, "刮磨");
                    Costm.YQPrice = GetCost(item.ProductId, item.WoodId ?? 0, "油漆");
                    Costm.PJPrice = GetCost(item.ProductId, item.WoodId ?? 0, "配件安装");
                    AddOrUpdate(Costm);
                }
            }
        }
        //把库存工艺品的所有产品计算成本
        public void AddGYPCost()
        {
            using (var db = new XNGYPEntities())
            {
                var LablesTable = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Status == 1)
                                   orderby p.CreateTime descending
                                   select new LabelsModel
                                   {
                                       Id = p.Id,
                                       ProductId = p.ProductsId,
                                       WoodId = p.WoodId,
                                       WoodName=p.WoodName,
                                   }).ToList();
                int OldProductId = 0;
                int OldWoodId = 0;
                foreach (var item in LablesTable)
                {
                    if (item.ProductId == OldProductId && item.WoodId == OldWoodId)
                    {
                        continue;
                    }
                    OldProductId = item.ProductId;
                    OldWoodId = item.WoodId ?? 0;

                    CostModel Costm = new CostModel();
                    Costm.ProductId = item.ProductId;
                    Costm.WoodId = item.WoodId;
                    Costm.WoodName = item.WoodName;
                    //Costm.MCPrice = Convert.ToDecimal(Convert.ToDecimal(WoodCB).ToString("00"));
                    Costm.KLPrice = GetCost(item.ProductId, item.WoodId ?? 0, "开料");
                    Costm.FLPrice = 0;
                    Costm.MGQPrice = GetCost(item.ProductId, item.WoodId ?? 0, "木工前段");
                    Costm.MGHPrice = GetCost(item.ProductId, item.WoodId ?? 0, "木工后段");
                    Costm.DHPrice = GetCost(item.ProductId, item.WoodId ?? 0, "雕花");
                    Costm.GMPrice = GetCost(item.ProductId, item.WoodId ?? 0, "刮磨");
                    Costm.YQPrice = GetCost(item.ProductId, item.WoodId ?? 0, "油漆");
                    Costm.PJPrice = GetCost(item.ProductId, item.WoodId ?? 0, "配件安装");
                    AddOrUpdate(Costm);
                }
                db.SaveChanges();
            }
        }
        //修改产品
        public void UpdateGYPSN()
        {
            Random r = new Random();
            using (var db = new XNGYPEntities())
            {
                var LablesTable = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false && k.Status == 1)
                                   orderby p.CreateTime descending
                                   select new LabelsModel
                                   {
                                       Id = p.Id,
                                       ProductId = p.ProductsId,
                                       WoodId = p.WoodId,
                                       WoodName = p.WoodName,
                                       ProductSN=p.ProductSN,
                                   }).ToList();
                foreach (var item in LablesTable)
                {
                    var NewLabes = db.XNGYP_INV_Labels.Where(k => k.Id == item.Id).SingleOrDefault();
                    if (!string.IsNullOrEmpty(item.ProductSN)) {

                        NewLabes.ProductSN = item.ProductSN.Substring(0,8);
                    }
                    
                }
                db.SaveChanges();
            }
        }
        //一键修改成本
        public void UpdateFCost()
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.SYS_product_Cost.Where(k => k.DeleteFlag == false && k.CCprice ==null)
                            orderby p.CreateTime
                            select new CostModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.SYS_product.name,
                                ProductSN = p.SYS_product.SYS_product_SN.name,
                                WoodId = p.WoodId,
                                WoodName = p.INV_wood_type.name,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGQPrice = p.MGQPrice,
                                MGHPrice = p.MGHPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                PJPrice=p.PJPrice,
                                CreateTime = p.CreateTime,
                                CCprice = p.CCprice,
                                CostCprice = p.CostCprice,
                                Volume = p.SYS_product.volume ?? 0,
                                PersonPrice = p.SYS_product.reserved1,
                                OldPersonPrice=p.PersonPrice,
                                MCDPrice=p.INV_wood_type.prcie,
                                ProductAreaId=p.SYS_product.product_area_id,
                                W_BZ = p.INV_wood_type.g_bz ?? 0,
                            }).ToList();
                foreach (var item in List)
                {
                    double CCL = 0.42;
                    if (item.ProductAreaId == 6)
                    {
                        CCL = 0.45;
                    }
                    double Woodunit = 0;//吨，材积/出材率*比重*数量

                    Woodunit = Convert.ToDouble(item.Volume) / CCL * Convert.ToDouble(item.W_BZ);
                    
                    var NewTable = db.SYS_product_Cost.Where(k => k.Id == item.Id).FirstOrDefault();
                    //NewTable.PersonPrice = item.PersonPrice;
                    //NewTable.FLPrice = 0;
                    NewTable.MCPrice = Convert.ToDecimal(Math.Floor(item.MCPrice.Value / 100) * 100);
                    NewTable.CostCprice = NewTable.MCPrice + NewTable.PersonPrice;
                    NewTable.CCprice = Convert.ToDecimal(Math.Floor((NewTable.CostCprice.Value * Convert.ToDecimal(1.6)) / 100) * 100);
                    NewTable.RGFY = Convert.ToDecimal(Math.Floor(item.OldPersonPrice.Value * Convert.ToDecimal(0.6) / 10) * 10);
                    if (item.MCDPrice <= 20000)
                    {
                        NewTable.MCFY = Convert.ToDecimal(Math.Floor(Woodunit * 20000 * 0.6 / 10) * 10);
                        
                        NewTable.CCprice = NewTable.MCPrice + NewTable.PersonPrice + NewTable.MCFY + NewTable.RGFY;
                    }
                    else { NewTable.MCFY = 0; }
                    
                    //NewTable.PersonPrice = Convert.ToDecimal(Math.Floor(item.PersonPrice.Value / 100) * 100);
                    
                    
                    NewTable.Volume = item.Volume;
                }
                db.SaveChanges();
            }
        }
    }
}
