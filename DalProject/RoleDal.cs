using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DalProject
{
    public class RoleDal
    {
        public List<RoleModel> GetPageList(SRoleModel SModel)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Role.Where(k => k.DeleteFlag == true)
                            where !string.IsNullOrEmpty(SModel.Name) ? p.UserName.Contains(SModel.Name) : true
                            orderby p.Id
                            select new RoleModel
                            {
                                Id = p.Id,
                                UserId = p.UserId,
                                UserName = p.UserName,
                                CreateTime = p.CreateTime,
                                MenuList = p.MenuList,
                            }).ToList();
                return List;
            }
        }
       
        //新增和修改仓库设置
        public void AddOrUpdate(RoleModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Role.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.UserId = Models.UserId;
                    table.UserName = Models.UserName;
                    table.MenuList = Models.MenuList;
                }
                else
                {
                    XNGYP_Role table = new XNGYP_Role();
                    table.UserId = Models.UserId;
                    table.UserName = Models.UserName;
                    table.MenuList = Models.MenuList;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = true;
                    db.XNGYP_Role.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public RoleModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Role.Where(k => k.Id == Id)
                              select new RoleModel
                              {
                                  Id = p.Id,
                                  UserId = p.UserId,
                                  UserName = p.UserName,
                                  CreateTime = p.CreateTime,
                                  MenuList = p.MenuList,
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
                        var tables = db.XNGYP_Role.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = false;
                    }
                }
                db.SaveChanges();
            }
        }
        public string GetUserMenuByUserId(int UserId)
        {
            using (var db = new XNGYPEntities())
            {
                string Return = "";
                var StrMenu = db.XNGYP_Role.Where(k => k.UserId == UserId).FirstOrDefault();
                if(StrMenu!=null)
                { Return= StrMenu.MenuList; }
                return Return;
            }
        }
        
    }
}
