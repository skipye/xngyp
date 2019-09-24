using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using ModelProject;
using System.Web.Mvc;

namespace DalProject
{
    public class CustomerDal
    {
        public List<CustomerModel> GetPageList(SCustomerModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1900-01-01");
            DateTime EndTime = Convert.ToDateTime("2900-12-30");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime);
            }
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Customers.Where(k => k.DeleteFlag == false)
                            where !string.IsNullOrEmpty(SModel.Name) ? p.Name.Contains(SModel.Name) : true
                            where SModel.DepartmentId != null && SModel.DepartmentId > 0 ? p.DepartmentId == SModel.DepartmentId : true
                            where SModel.BelongUserId != null && SModel.BelongUserId > 0 ? p.BelongUserId == SModel.BelongUserId : true
                            where p.CreateTime >= StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new CustomerModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                ShortName = p.ShortName,
                                Address = p.Address,
                                Address_Delivery = p.Address_Delivery,
                                CreateTime = p.CreateTime,
                                LinkMan = p.LinkMan,
                                LinkTel = p.LinkTel,
                                Email = p.Email,
                                BelongUserId = p.BelongUserId,
                                BelongUserName = p.BelongUserName,
                                DepartmentId = p.DepartmentId,
                                Department = p.Department,
                            }).ToList();
                return List;
            }
        }

        public void AddOrUpdate(CustomerModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id != null && Models.Id > 0)
                {
                    var table = db.XNGYP_Customers.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.Name = Models.Name;
                    table.ShortName = Models.ShortName;
                    table.Address = Models.Address;
                    table.Address_Delivery = Models.Address_Delivery;
                    table.Email = Models.Email;
                    table.LinkMan = Models.LinkMan;
                    table.LinkTel = Models.LinkTel;
                    table.BelongUserId = Models.BelongUserId;
                    table.BelongUserName = Models.BelongUserName;
                    table.DepartmentId = Models.DepartmentId;
                    table.Department = Models.Department;
                    db.SaveChanges();
                }
                else
                {
                    XNGYP_Customers table = new XNGYP_Customers();
                    table.Name = Models.Name;
                    table.ShortName = Models.ShortName;
                    table.Address = Models.Address;
                    table.Address_Delivery = Models.Address_Delivery;
                    table.Email = Models.Email;
                    table.LinkMan = Models.LinkMan;
                    table.LinkTel = Models.LinkTel;
                    table.BelongUserId = Models.BelongUserId;
                    table.BelongUserName = Models.BelongUserName;
                    table.DepartmentId = Models.DepartmentId;
                    table.Department = Models.Department;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    db.XNGYP_Customers.Add(table);

                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public CustomerModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Customers.Where(k => k.Id == Id)
                              select new CustomerModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  ShortName = p.ShortName,
                                  Address = p.Address,
                                  Address_Delivery = p.Address_Delivery,
                                  CreateTime = p.CreateTime,
                                  LinkMan = p.LinkMan,
                                  LinkTel = p.LinkTel,
                                  Email = p.Email,
                                  BelongUserId = p.BelongUserId,
                                  BelongUserName = p.BelongUserName,
                                  DepartmentId = p.DepartmentId,
                                  Department = p.Department,
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
                        var tables = db.XNGYP_Customers.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void UpdateBelong(BelongModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Customers.Where(k => k.Id == Models.Id).SingleOrDefault();
                    
                    table.BelongUserId = Models.BelongUserId;
                    table.BelongUserName = Models.BelongUserName;
                    table.DepartmentId = Models.DepartmentId;
                    table.Department = Models.Department;
                    db.SaveChanges();
                }
            }
        }
        public List<SelectListItem> GetCustomerDrolist(int? pId, int? UserId, int? DepartmentId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择客户", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<CRMItem> model = (from p in db.XNGYP_Customers.Where(b => b.DeleteFlag == false)
                                       where UserId > 0 ? p.BelongUserId == UserId.Value : true
                                       where DepartmentId > 0 ? p.DepartmentId == DepartmentId.Value : true
                                       select new CRMItem
                                            {
                                                Id=p.Id,
                                                Name=p.Name
                                            }).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text =  item.Name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public string GetCuDrolistByCId(int? CId)
        {
            using (var db = new XNGYPEntities())
            {
                var list = (from p in db.XNGYP_Customers.Where(k => k.DeleteFlag == false)
                            where CId > 0 ? p.Id == CId : true
                            orderby p.CreateTime descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.ShortName,
                                tel = p.LinkTel,
                                Address = p.Address,
                                department=p.LinkMan,
                            }).FirstOrDefault();
                var strText = list.Name + "_" + list.tel + "_" + list.department + "_" + list.Address;
                return strText;
            }
        }
    }
}
