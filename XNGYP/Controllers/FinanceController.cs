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
    public class FinanceController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly FinanceService FSer = new FinanceService();
        private static readonly ContractHeaderService NSer = new ContractHeaderService();
        [Authorize]
        public ActionResult Order()
        {
            SContractHeaderModel SModels = new SContractHeaderModel();
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModels.StartTime))
            {
                SModels.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModels.EndTime))
            {
                SModels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            SModels.DepartmentDroList = USer.GetDepartmentDrolist(SModels.DepartmentId);
            return View(SModels);
        }
        public ActionResult Money()
        {
            SContractHeaderModel SModels = new SContractHeaderModel();
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModels.StartTime))
            {
                SModels.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModels.EndTime))
            {
                SModels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            SModels.DepartmentDroList = USer.GetDepartmentDrolist(SModels.DepartmentId);
            return View(SModels);
        }
        public ActionResult PageList(SContractHeaderModel SRmodels)
        {
            SRmodels.CheckState = 1;
            var models = NSer.GetPageList(SRmodels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(models),
                ContentType = "application/json"
            };
        }
        public ActionResult FR(int Id)
        {
            FinanceModel Models = new FinanceModel();
            Models.Id = Id;
            return View(Models);
        }
        public ActionResult FostFR(FinanceModel Models)
        {
            Models.operator_id = USer.GetCurrentUserName().UserId;
            Models.operator_name = USer.GetCurrentUserName().UserName;
            if (FSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult Show(int Id)
        {
            var Models = FSer.GetFKShowList(Id);
            return View(Models);
        }

    }
}
