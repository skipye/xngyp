using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class PreCastDal
    {
        
        public List<PreCastModel> GetList(SPreCastModel SModel)
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
                var List = (from p in db.XNGYP_WIP_PreCast.Where(k => k.DeleteFlag == false)
                            where SModel.ProductSNId != null && SModel.ProductSNId > 0 ? SModel.ProductSNId == p.ProductSNId : true
                            where SModel.WoodId > 0 ? SModel.WoodId == p.WoodId : true
                            where !string.IsNullOrEmpty(SModel.ProductName) ? p.ProductName.Contains(SModel.ProductName) : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new PreCastModel
                            {
                                Id = p.Id,
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                Color = p.ColorName,
                                ColorId=p.ColorId,
                                WoodId=p.WoodId,
                                WoodName = p.WoodName,
                                Length = p.Length,
                                Width = p.Width,
                                Height = p.Height,
                                Staute = p.Staute,
                                CreateTime=p.CreateTime,
                                Qty=p.Qty,
                            }).ToList();
                return List;
            }
        }
        public void AddOrUpdate(PreCastModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_WIP_PreCast.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.ProductName = Models.ProductName;
                    table.ProductId = Models.ProductId;
                    table.ProductSNId = Models.ProductSNId;
                    table.Length = Models.Length;
                    table.Width = Models.Width;
                    table.Height = Models.Height;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.ColorId = Models.ColorId;
                    table.ColorName = Models.Color;
                    table.Qty = Models.Qty;
                }
                else
                {
                    XNGYP_WIP_PreCast table = new XNGYP_WIP_PreCast();
                    table.ProductName = Models.ProductName;
                    table.ProductId = Models.ProductId;
                    table.ProductSNId = Models.ProductSNId;
                    table.Length = Models.Length;
                    table.Width = Models.Width;
                    table.Height = Models.Height;
                    table.WoodId = Models.WoodId;
                    table.WoodName = Models.WoodName;
                    table.ColorId = Models.ColorId;
                    table.ColorName = Models.Color;
                    table.Staute = 1;
                    table.Qty = Models.Qty;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    db.XNGYP_WIP_PreCast.Add(table);
                }
                db.SaveChanges();
            }
        }
        public PreCastModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_WIP_PreCast.Where(k => k.Id == Id)
                              select new PreCastModel
                              {
                                  Id = p.Id,
                                  ProductId = p.ProductId,
                                  ProductName = p.ProductName,
                                  Color = p.ColorName,
                                  ColorId = p.ColorId,
                                  WoodId = p.WoodId,
                                  WoodName = p.WoodName,
                                  Length = p.Length,
                                  Width = p.Width,
                                  Height = p.Height,
                                  Staute = p.Staute,
                                  CreateTime = p.CreateTime,
                                  Qty=p.Qty,
                              }).SingleOrDefault();
                return tables;
            }
        }
    }
}
