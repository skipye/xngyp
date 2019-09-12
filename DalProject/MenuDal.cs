﻿using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DalProject
{
    public class MenuDal
    {
        public List<MenuModel> GetPageList(SMenuModel SModel)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_Menu.Where(k => k.DeleteFlag == true)
                            where !string.IsNullOrEmpty(SModel.Name) ? p.Name.Contains(SModel.Name) : true
                            where SModel.TypeId != null && SModel.TypeId > 0 ? p.Id == SModel.TypeId : true
                            orderby p.Rank
                            select new MenuModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Rank = p.Rank,
                                CreateTime = p.CreateTime,
                                ParentId = p.ParentId,
                                ParentName = p.XNGYP_Menu2.Name,
                                Action=p.Action,
                                Controller=p.Controller,
                                Icon=p.Icon,
                            }).ToList();
                
                return List;
            }
        }
        public List<SelectListItem> GetParentType(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择父类别", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<XNGYP_Menu> model = db.XNGYP_Menu.Where(b => b.DeleteFlag == true && b.ParentId == null).OrderBy(k => k.Rank).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.Name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                    List<XNGYP_Menu> childrenmodel = db.XNGYP_Menu.Where(b => b.DeleteFlag == true && b.ParentId == item.Id).OrderBy(k => k.Rank).ToList();
                    if (childrenmodel != null && childrenmodel.Any())
                    {
                        foreach (var Citem in childrenmodel)
                        {
                            items.Add(new SelectListItem() { Text = "----└" + Citem.Name, Value = Citem.Id.ToString(), Selected = pId.HasValue && Citem.Id.Equals(pId) });
                        }
                    }
                }
            }
            return items;
        }
       
        //新增和修改仓库设置
        public void AddOrUpdate(MenuModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.XNGYP_Menu.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.ParentId = Models.ParentId;
                    table.Name = Models.Name;
                    table.Rank = Models.Rank;
                    table.ParentId = Models.ParentId;
                    table.Action = Models.Action;
                    table.Controller = Models.Controller;
                    table.Remark = Models.Remark;
                    table.Icon = Models.Icon;
                }
                else
                {
                    XNGYP_Menu table = new XNGYP_Menu();
                    table.Name = Models.Name;
                    table.Rank = Models.Rank;
                    table.ParentId = Models.ParentId;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = true;
                    table.Action = Models.Action;
                    table.Controller = Models.Controller;
                    table.Remark = Models.Remark;
                    table.Icon = Models.Icon;
                    db.XNGYP_Menu.Add(table);
                }
                db.SaveChanges();
            }
        }
        //根据文章Id获取内容
        public MenuModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Menu.Where(k => k.Id == Id)
                              select new MenuModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Rank = p.Rank,
                                  CreateTime = p.CreateTime,
                                  ParentId = p.ParentId,
                                  Action = p.Action,
                                  Controller = p.Controller,
                                  Remark=p.Remark,
                                  Icon = p.Icon,
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
                        var tables = db.XNGYP_Menu.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = false;
                    }
                }
                db.SaveChanges();
            }
        }
        public List<MenuItemModel> GetMenuItemList(string UserMenuList)
        {
            List<MenuItemModel> Models = new List<MenuItemModel>();
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.XNGYP_Menu.Where(k => k.DeleteFlag == true && k.ParentId == null)
                              orderby p.Rank
                              select new MenuItemModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Icon = p.Icon,
                                  SonItemList = p.XNGYP_Menu1.Where(k => k.DeleteFlag == true).OrderBy(k => k.Rank).Select(k => new MenuSonItemModel
                                  {
                                      Id = k.Id,
                                      Name = k.Name,
                                      Action = k.Action,
                                      Controller = k.Controller,
                                  })
                              }).ToList();

                if (!string.IsNullOrEmpty(UserMenuList))
                {
                    MenuItemModel PModels = new MenuItemModel();
                    foreach (var NItem in tables)
                    {
                        var PMenuId = "$" + NItem.Id + ",";
                        if (UserMenuList.Contains(PMenuId) == true)
                        {
                            PModels = NItem;
                            //LSonModel = NItem.SonItemList;
                            //PModels.SonItemList = new List<MenuSonItemModel>();
                        }
                        else { continue ; }
                        List<MenuSonItemModel> LLSonModel = new List<MenuSonItemModel>();
                        if(PModels.SonItemList!=null && PModels.SonItemList.Any())
                        { 
                            foreach (var SNItem in PModels.SonItemList)
                            {
                                var SMenuId = "$" + SNItem.Id + ",";
                                if (UserMenuList.Contains(SMenuId) == true)
                                {
                                    LLSonModel.Add(SNItem);
                                }
                                PModels.SonItemList = LLSonModel;
                            }
                            Models.Add(PModels);
                        }
                    }
                }
                else { Models = tables; }
                return Models;

            }
        }

    }
}
