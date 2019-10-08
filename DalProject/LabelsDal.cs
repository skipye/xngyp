using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class LabelsDal
    {
        public static string GenerateTimeStamp()
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
                var List = (from p in db.XNGYP_INV_Labels.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductsSNId : true
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
                                WoodId=p.WoodId,
                                ColorId=p.ColorId,
                                Status=p.Status,
                                flag=p.Flag,
                                Grade = p.Grade,
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
                        table.Flag = 1;
                        table.ContractDetailId = 0;
                        table.WIPContractIid = 0;
                        table.Grade = Models.Grade;
                        db.XNGYP_INV_Labels.Add(table);
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
                                  Grade=p.Grade,
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
    }
}
