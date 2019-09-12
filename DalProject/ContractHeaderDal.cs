﻿using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using System.Web.Mvc;

namespace DalProject
{
    public class ContractHeaderDal
    {
        public ContractModel GetPageList(SContractHeaderModel SModel)
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
                var List = (from p in db.Contract_Header.Where(k => k.DeleteFlag == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where SModel.CheckState == 1 ? p.Status == SModel.CheckState : true
                            where SModel.DepartmentId>0 ? p.SaleDepartmentId == SModel.DepartmentId : true
                            where SModel.SaleUserId > 0 ? p.SaleUserId == SModel.SaleUserId : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.XNGYP_Customers.Name.Contains(SModel.UserName) : true
                            where SModel.FR_flag >= 0 ? p.FRFlag == SModel.FR_flag : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new ContractHeaderModel
                            {
                                Id = p.Id,
                                SN = p.SN,
                                Customer = p.XNGYP_Customers.Name,
                                CustomerId = p.CustomerId,
                                TelPhone = p.DeliveryLinkTel,
                                DeliveryDate = p.DeliveryDate,
                                DeliveryAddress = p.DeliveryAddress,
                                Amount = p.Amount,
                                Status = p.Status,
                                Prepay = p.Prepay,
                                SaleUserId = p.SaleUserId,
                                SaleUserName = p.SaleUserName,
                                SaleDepartmentId = p.SaleDepartmentId,
                                SaleDepartment = p.SaleDepartment,
                                CheckedUserName = p.CheckedUserName,
                                FRFlag = p.FRFlag,
                                OrderTime = p.HTDate,
                                CheckedTime=p.CheckedTime,
                                CreateTime = p.CreateTime,
                                CWCheckStatus=p.CWCheckStatus
                            }).ToList();
                ContractModel Models = new ContractModel();
                Models.data = List;
                Models.HTTotail = List.Sum(k => k.Amount);
                return Models;
            }
        }
        
        public void AddOrUpdate(ContractHeaderModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.Contract_Header.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.CustomerId = Models.CustomerId;
                    table.DeliveryDate = Models.DeliveryDate;
                    table.Prepay = Models.Prepay;
                    table.DeliveryAddress = Models.DeliveryAddress;
                    table.SaleUserId = Models.SaleUserId;
                    table.SaleUserName = Models.SaleUserName;
                    table.SaleDepartmentId = Models.SaleDepartmentId;
                    table.SaleDepartment = Models.SaleDepartment;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                }
                else
                {
                    Contract_Header table = new Contract_Header();
                    table.SN = Models.SN;
                    table.CustomerId = Models.CustomerId;
                    table.DeliveryDate = Models.DeliveryDate;
                    table.Amount = 0;
                    table.Prepay = Models.Prepay;
                    table.DeliveryAddress = Models.DeliveryAddress;
                    table.SaleUserId = Models.SaleUserId;
                    table.SaleUserName = Models.SaleUserName;
                    table.SaleDepartmentId = Models.SaleDepartmentId;
                    table.SaleDepartment = Models.SaleDepartment;
                    table.Status = 0;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                    table.FRFlag = 0;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    table.CWCheckStatus = false;
                    db.Contract_Header.Add(table);
                }
                db.SaveChanges();

            }
        }

        public ContractHeaderModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.Contract_Header.Where(k => k.DeleteFlag == false && k.Id == Id)

                              orderby p.CreateTime descending
                              select new ContractHeaderModel
                              {
                                  Id = p.Id,
                                  SN = p.SN,
                                  Customer = p.XNGYP_Customers.Name,
                                  CustomerId = p.CustomerId,
                                  DeliveryDate = p.DeliveryDate,
                                  Amount = p.Amount,
                                  Status = p.Status,
                                  Prepay = p.Prepay,
                                  DeliveryAddress = p.DeliveryAddress,
                                  SaleUserId = p.SaleUserId,
                                  SaleUserName = p.SaleUserName,
                                  SaleDepartmentId = p.SaleDepartmentId,
                                  SaleDepartment = p.SaleDepartment,
                                  CheckedUserName = p.CheckedUserName,
                                  FRFlag = p.FRFlag,
                                  OrderTime = p.HTDate,
                                  CheckedTime = p.CheckedTime,
                              }).SingleOrDefault();
                return tables;
            }
        }
       
        public void Delete(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void Checked(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.Status = 1;
                        tables.CheckedUserId = new UserDal().GetCurrentUserName().UserId;
                        tables.CheckedUserName = new UserDal().GetCurrentUserName().UserName;
                        tables.CheckedTime = DateTime.Now;
                    }
                }

                db.SaveChanges();
            }
        }
        public void CWChecked(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.CWCheckStatus = true;
                        tables.CWCheckId = new UserDal().GetCurrentUserName().UserId;
                        tables.CWCheckName = new UserDal().GetCurrentUserName().UserName;
                        tables.CWCheckTime = DateTime.Now;
                    }
                }

                db.SaveChanges();
            }
        }

        //根据日期获取合同总个数
        public int GetCRMHTCount(DateTime CreateTime)
        {
            using (var db = new XNGYPEntities())
            {
                var List = db.Contract_Header.Where(k => k.CreateTime > CreateTime && k.DeleteFlag==false).Count();
                return List;
            }
        }
        public List<SelectListItem> GetProSNDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择产品系列", Value = "" });
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
       
        public List<SelectListItem> GetWoodDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择材质", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_wood_type> model = db.INV_wood_type.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetColorDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择色号", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_colors> model = db.SYS_colors.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
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
        public void AddProducts(ContractProductsModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                decimal price = 0;
                if (Models.price > 0)
                { price = Models.price.Value; }

                Contract_Detail table = new Contract_Detail();
                table.ContractHeadId = Models.ContractHeadId;
                table.ProductId = Models.ProductId;
                table.ProductSN = Models.ProductSN;
                table.ProductName = Models.ProductName;
                table.Color = Models.Color;
                table.WoodName = Models.WoodName;
                table.CustomFlag = Models.CustomFlag;
                table.length = Models.length;
                table.width = Models.width;
                table.height = Models.height;
                table.Price = price;
                table.hardware_part = Models.hardware_part;
                table.decoration_part = Models.decoration_part;
                table.req_others = Models.req_others;
                table.CreateTime = DateTime.Now;
                table.DeleteFlag = false;
                table.Status = 0;
                db.Contract_Detail.Add(table);

                var HeadTable = db.Contract_Header.Where(k => k.Id == Models.ContractHeadId).SingleOrDefault();
                HeadTable.Amount = HeadTable.Amount + price * Models.qty;
                db.SaveChanges();

            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = db.Contract_Detail.Where(k => k.Id == Id).SingleOrDefault();
                tables.DeleteFlag = true;

                var HeadId = tables.ContractHeadId;
                var HeadTable = db.Contract_Header.Where(k => k.Id == HeadId).FirstOrDefault();
                HeadTable.Amount = HeadTable.Amount - tables.Price.Value;

                db.SaveChanges();
            }
        }
        public List<ContractProductsModel> GetProductListByOrder(int HTId)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.Contract_Detail.Where(k => k.DeleteFlag == false && k.ContractHeadId == HTId)
                            orderby p.CreateTime descending
                            select new ContractProductsModel
                            {
                                Id = p.Id,
                                ContractHeadId = p.ContractHeadId,
                                ProductName = p.ProductName,
                                ProductSN = p.ProductSN,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                CustomFlag = p.CustomFlag,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                price = p.Price,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                status = p.Contract_Header.Status,
                            }).ToList();
                return List;
            }
        }
    }
}
