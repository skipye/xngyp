using ModelProject;
using ServiceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class UsersController : Controller
    {
        private static readonly UserService USer = new UserService();
        [Authorize]
        public void AddWorkLogs(WorkLogsModel tables)
        {
            tables.UserId = USer.GetCurrentUserName().UserId;
            tables.UserName = USer.GetCurrentUserName().UserName;
            try { USer.AddWorkLogs(tables); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult Index()
        {
            SUsersModel SModels = new SUsersModel();
            return View(SModels);
        }
        public ActionResult PageList(SUsersModel SModels)
        {
            var PageList = USer.GetPageList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            UsersModel Models = new UsersModel();
            if (Id != null && Id > 0)
            {
                Models = USer.GetDetailById(Id.Value);
            }
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(UsersModel Models)
        {
            if (USer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult Logs()
        {
            return View();
        }
        public ActionResult LogsPgae()
        {
            var PageList = USer.GetLogsPageList();
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }

    }
}
