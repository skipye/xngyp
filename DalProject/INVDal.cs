using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class INVDal
    {
        public List<INV_NameModel> GetPageList()
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.INV_Name.Where(k => k.DeleteFlag == false)
                            orderby p.CreateTime
                            select new INV_NameModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Remark = p.Remark,
                                CreateTime = p.CreateTime,
                                TypeId = p.Type,
                                Address=p.Address,
                                TypeName = p.INV_Name_Type.Name,
                            }).ToList();
                
                return List;
            }
        }
        public List<SelectListItem> GetType(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择仓库类别", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<INV_Name_Type> model = db.INV_Name_Type.Where(b => b.DeleteFlag == false).OrderBy(k => k.CreateTime).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.Name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public void AddOrUpdate(INV_NameModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.INV_Name.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.Type = Models.TypeId;
                    table.Name = Models.Name;
                    table.Remark = Models.Remark;
                    table.Address = Models.Address;
                }
                else
                {
                    INV_Name table = new INV_Name();
                    table.Name = Models.Name;
                    table.Remark = Models.Remark;
                    table.Address = Models.Address;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    table.Type = Models.TypeId;
                    db.INV_Name.Add(table);
                }
                db.SaveChanges();
            }
        }
        public INV_NameModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.INV_Name.Where(k => k.Id == Id)
                              select new INV_NameModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Remark = p.Remark,
                                  CreateTime = p.CreateTime,
                                  Address = p.Address,
                                  TypeId=p.Type
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
                        var tables = db.INV_Name.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        
    }
}
