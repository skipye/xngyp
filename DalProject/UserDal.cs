using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelProject;
using DataBase;
using System.Web.Security;
using System.Web.Mvc;

namespace DalProject
{
    public class UserDal
    {
        //操作日志
        public void AddWorkLogs(WorkLogsModel tables)
        {
            using (var db = new XNGYPEntities())
            {
                var WorkLogs = new WorkLogs();
                WorkLogs.UserId = tables.UserId;
                WorkLogs.UserName = tables.UserName;
                WorkLogs.MSG = tables.MSG;
                WorkLogs.MSGStatus = tables.MSGStatus;
                WorkLogs.CreateTime = DateTime.Now;
                db.WorkLogs.Add(WorkLogs);
                db.SaveChanges();
            }
        }
        public List<WorkLogsModel> GetLogsPageList()
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.WorkLogs
                            orderby p.Id descending
                            select new WorkLogsModel
                            {
                                UserName = p.UserName,
                                MSG = p.MSG,
                                MSGStatus = p.MSGStatus,
                                CreateTime = p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        public List<UsersModel> GetPageList(SUsersModel SModel)
        {
            
            using (var db = new XNHREntities())
            {
                var List = (from p in db.ehr_employee.Where(k => k.status == 1)
                            where !string.IsNullOrEmpty(SModel.Name) ? p.name.Contains(SModel.Name) : true
                            orderby p.lawdate descending
                            select new UsersModel
                            {
                                Id = p.id,
                                Name = p.name,
                                Telphone = p.tel,
                                jobtime = p.jobtime,
                                officialtime = p.officialtime,
                                departmentname = p.departmentname,
                            }).ToList();
                return List;
            }
        }
        //用户登录
        public LoginModel IsLogin(LoginModel models)
        {
            LoginModel ReturnModel = new LoginModel();
            using (var db = new XNHREntities())
            {
                ReturnModel = (from p in db.ehr_employee.Where(k => k.password == models.PassWord && k.status == 1)
                              where !string.IsNullOrEmpty(models.UserName) ? p.name == models.UserName : true
                              where !string.IsNullOrEmpty(models.Telephone) ? p.tel == models.Telephone : true
                              select new LoginModel {
                                  UserName=p.name,
                                  UserId=p.id,
                                  departmentId=p.department,
                                  department=p.departmentname,
                                  IsLogin = true,
                              }
                             ).FirstOrDefault();
            }
            if (ReturnModel == null)
            {
                using (var db = new SaleHREntities())
                {
                    ReturnModel = (from p in db.ehr_employee.Where(k => k.password == models.PassWord && k.status == 1)
                                   where !string.IsNullOrEmpty(models.UserName) ? p.name == models.UserName : true
                                   where !string.IsNullOrEmpty(models.Telephone) ? p.tel == models.Telephone : true
                                   select new LoginModel
                                   {
                                       UserName = p.name,
                                       UserId = p.id,
                                       departmentId = p.department,
                                       department = p.departmentname,
                                       IsLogin=true,
                                   }
                                 ).FirstOrDefault();
                }
            }
            if (ReturnModel != null)
            {

                WorkLogsModel WModels = new WorkLogsModel();
                WModels.UserId = ReturnModel.UserId;
                WModels.UserName = ReturnModel.UserName;
                WModels.MSG = "用户登录";
                WModels.MSGStatus = 1;
                AddWorkLogs(WModels);
            }
            else { ReturnModel.IsLogin = false; }

            return ReturnModel;
        }

        public UserCurrentModel GetCurrentUserName()
        {
            UserCurrentModel models = new UserCurrentModel();
            var List = FormsAuthentication.GetAuthCookie("XNGYPCookie", false);
            if (System.Web.HttpContext.Current.User.Identity.Name.Contains("|") == false)
            {
                return new UserCurrentModel();
            }
            else
            {
                var CurrentModels = System.Web.HttpContext.Current.User.Identity.Name.Split('|');

                models.UserName = CurrentModels[0];
                models.UserId = Convert.ToInt32(CurrentModels[1]);
                models.departmentId = Convert.ToInt32(CurrentModels[2]);
                models.department = CurrentModels[3];
                return models;
            }
        }
        public void AddOrUpdate(UsersModel Models)
        {
            using (var db = new XNHREntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.ehr_employee.Where(k => k.id == Models.Id).SingleOrDefault();
                    table.tel = Models.Telphone;
                    table.name = Models.Name;
                    table.password = Models.Password;

                }
                else
                {
                    ehr_employee table = new ehr_employee();
                    table.tel = Models.Telphone;
                    table.name = Models.Name;
                    table.password = Models.Password;
                    db.ehr_employee.Add(table);
                }
                db.SaveChanges();
            }
        }
        public UsersModel GetDetailById(int Id)
        {
            using (var db = new XNHREntities())
            {
                var tables = (from p in db.ehr_employee.Where(k => k.id == Id)
                              select new UsersModel
                              {
                                  Id = p.id,
                                  Name = p.name,
                                  Telphone = p.tel,
                                  Password = p.password,
                                  Password2 = p.password,
                              }).SingleOrDefault();
                return tables;
            }
        }
       
        public List<SelectListItem> GetUserDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择用户", Value = "" });
            List<UsersModel> UserModel = new List<UsersModel>();
            List<UsersModel> Result = new List<UsersModel>();
            using (var db = new XNHREntities())
            {
                UserModel = (from p in db.ehr_employee.Where(b => b.status == 1)
                             orderby p.department
                             select new UsersModel
                             {
                                Id=p.id,
                                Name=p.name,
                                departmentname=p.departmentname,
                                departmentId=p.department,
                                Telphone =p.tel,
                                Flag=1,
                             }).ToList();
            }
            using (var db = new SaleHREntities())
            {
                var NewUserModel = (from p in db.ehr_employee.Where(b => b.status == 1)
                             orderby p.department
                             select new UsersModel
                             {
                                 Id = p.id,
                                 Name = p.name,
                                 departmentname = p.departmentname,
                                 departmentId = p.department,
                                 Telphone = p.tel,
                                 Flag = 2,
                             }).ToList();
                 Result = UserModel.Union(NewUserModel).ToList<UsersModel>();
            }
            foreach (var item in Result)
            {
                items.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
            }
            return items;
        }
        public List<SelectListItem> GetDepartmentDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择部门", Value = "" });
            using (var db = new XNHREntities())
            {
                List<ehr_department> model = db.ehr_department.OrderBy(k => k.id).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = "╋" + item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
    }
}
