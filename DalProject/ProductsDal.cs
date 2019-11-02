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
                                XLSecName=p.XNGYP_Products_SN1.name
                            }).ToList();
                
                return List;
            }
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
                    table.FatherId = Models.FatherId;
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
                    table.FatherId = Models.FatherId;
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
                                  volume = p.volume,
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
                                FatherId = p.FatherId,
                                FatherName = p.XNGYP_Products_SN2.name,
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
                    table.FatherId = Models.FatherId;
                }
                else
                {
                    XNGYP_Products_SN table = new XNGYP_Products_SN();
                    table.name = Models.name;
                    table.SN = Models.SN;
                    table.remark = Models.remark;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    table.FatherId = Models.FatherId;
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
                                  FatherId=p.FatherId,
                                  
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
                var list = (from p in db.XNGYP_Products.Where(k => k.delete_flag == false )
                            where ProSN > 0 ? p.FatherId == ProSN : true
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
                int i = 1;
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.length + "_" + item.width + "_" + item.height;
                    var IstrValue = item.Id;
                    if (i == 1)
                    {
                        NewItme += "<option value=" + IstrValue + " selected='selected'>" + strText + "</option>";
                    }
                    else { NewItme += "<option value=" + IstrValue + ">" + strText + "</option>"; }
                    
                    i++;
                }
                return NewItme;
            }
        }
        public string GetSecSNDrolistByFatherId(int? FatherId)
        {
            using (var db = new XNGYPEntities())
            {
                var list = (from p in db.XNGYP_Products_SN.Where(k => k.delete_flag == false)
                            where FatherId > 0 ? p.FatherId == FatherId : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.name,
                                label = p.SN,
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.label;
                    var IstrValue = item.Id;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        public string GetDrolistByFatherId(int? FatherId, string SelectedId)
        {
            using (var db = new XNGYPEntities())
            {
                var list = (from p in db.XNGYP_Products_SN.Where(k => k.delete_flag == false)
                            where FatherId > 0 ? p.FatherId == FatherId : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.name,
                                label = p.SN,
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.label;
                    var IstrValue = item.Id;
                    if (item.label == SelectedId)
                    { NewItme += "<option value=" + IstrValue + " selected='selected'>" + strText + "</option>"; }
                    else { NewItme += "<option value=" + IstrValue + ">" + strText + "</option>"; }

                }
                return NewItme;
            }
        }
    }
}
