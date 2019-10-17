using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
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
                                WoodId = p.WoodId,
                                WoodName = p.WoodName,
                                MCPrice = p.MCPrice,
                                KLPrice = p.KLPrice,
                                DHPrice = p.DHPrice,
                                MGPrice = p.MGPrice,
                                GMPrice = p.GMPrice,
                                YQPrice = p.YQPrice,
                                FLPrice = p.FLPrice,
                                CreateTime=p.CreateTime
                            }).ToList();
                
                return List;
            }
        }
        //新增和修改仓库设置
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
                    table.MGPrice = Models.MGPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                }
                else
                {
                    XNGYP_Products_Price table = new XNGYP_Products_Price();
                    table.ProductId = Models.ProductId;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.MCPrice = Models.MCPrice;
                    table.KLPrice = Models.KLPrice;
                    table.DHPrice = Models.DHPrice;
                    table.MGPrice = Models.MGPrice;
                    table.GMPrice = Models.GMPrice;
                    table.YQPrice = Models.YQPrice;
                    table.FLPrice = Models.FLPrice;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
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
                                  MGPrice = p.MGPrice,
                                  GMPrice = p.GMPrice,
                                  YQPrice = p.YQPrice,
                                  FLPrice = p.FLPrice,
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
        

    }
}
