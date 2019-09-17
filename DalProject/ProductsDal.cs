using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class ProductsDal
    {
        public List<ProductsNameModel> GetPageList()
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Products.Where(k => k.delete_flag == false)
                            orderby p.created_time
                            select new ProductsNameModel
                            {
                                Id = p.Id,
                                name = p.name,
                                ProductsSNId = p.ProductsSNId,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                picture=p.picture,
                                ProductsSNName = p.XNGYP_Products_SN.name,
                                remark=p.remark,
                                paper_path=p.paper_path,
                                BOM_path=p.BOM_path,
                                created_time=p.created_time,
                                volume=p.volume,
                            }).ToList();
                
                return List;
            }
        }
        public List<SelectListItem> GetPorductsSNDroList(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择系列", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<XNGYP_Products_SN> model = db.XNGYP_Products_SN.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public void AddOrUpdate(ProductsNameModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Products.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.name = Models.name;
                    table.ProductsSNId = Models.ProductsSNId;
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.picture = Models.picture;
                    table.paper_path = Models.paper_path;
                    table.BOM_path = Models.BOM_path;
                    table.remark = Models.remark;
                    table.volume = Models.volume;
                }
                else
                {
                    XNGYP_Products table = new XNGYP_Products();
                    table.name = Models.name;
                    table.ProductsSNId = Models.ProductsSNId;
                    table.length = Models.length;
                    table.width = Models.width;
                    table.height = Models.height;
                    table.picture = Models.picture;
                    table.paper_path = Models.paper_path;
                    table.BOM_path = Models.BOM_path;
                    table.volume = Models.volume;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.XNGYP_Products.Add(table);
                }
                db.SaveChanges();
            }
        }
        public ProductsNameModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Products.Where(k => k.Id == Id)
                              select new ProductsNameModel
                              {
                                  Id = p.Id,
                                  name = p.name,
                                  ProductsSNId = p.ProductsSNId,
                                  length = p.length,
                                  width = p.width,
                                  height = p.height,
                                  picture = p.picture,
                                  remark = p.remark,
                                  paper_path = p.paper_path,
                                  BOM_path = p.BOM_path,
                                  created_time = p.created_time,
                                  volume=p.volume,
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
                        var tables = db.XNGYP_Products.Where(k => k.Id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public List<ProductsSNModel> GetSNPageList()
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Products_SN.Where(k => k.delete_flag == false)
                            orderby p.created_time
                            select new ProductsSNModel
                            {
                                Id = p.Id,
                                name = p.name,
                                SN = p.SN,
                                remark = p.remark,
                                created_time = p.created_time,
                            }).ToList();

                return List;
            }
        }
        
        public void AddOrUpdateSN(ProductsSNModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Products_SN.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.name = Models.name;
                    table.SN = Models.SN;
                    table.remark = Models.remark;
                }
                else
                {
                    XNGYP_Products_SN table = new XNGYP_Products_SN();
                    table.name = Models.name;
                    table.SN = Models.SN;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.XNGYP_Products_SN.Add(table);
                }
                db.SaveChanges();
            }
        }
        public ProductsSNModel GetSNDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Products_SN.Where(k => k.Id == Id)
                              select new ProductsSNModel
                              {
                                  Id = p.Id,
                                  SN=p.SN,
                                  name = p.name,
                                  remark = p.remark,
                                  created_time = p.created_time,
                              }).SingleOrDefault();
                return tables;
            }
        }

        public void DeleteSNMore(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.XNGYP_Products_SN.Where(k => k.Id == Id).SingleOrDefault();
                        tables.delete_flag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public string GetProNameDrolistBySN(int? ProSN)
        {
            using (var db = new XNGYPEntities())
            {
                var list = (from p in db.XNGYP_Products.Where(k => k.delete_flag == false)
                            where ProSN > 0 ? p.ProductsSNId == ProSN : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.name,
                                length = p.length,
                                width = p.width,
                                height = p.height
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.length + "_" + item.width + "_" + item.height;
                    var IstrValue = item.Id;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
    }
}
