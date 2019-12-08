using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class HRController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly HRService LSer = new HRService();
        private static readonly ToExcel ESer = new ToExcel();
        [Authorize]
        public ActionResult Index(SHRTimesModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModel);
        }
        [Authorize]
        public ActionResult GRIndex(SHRTimesModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModel);
        }
        [Authorize]
        public ActionResult CWIndex(SHRTimesModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModel);
        }
        [Authorize]
        public ActionResult CWGRIndex(SHRTimesModel SModel)
        {
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).AddMonths(-1).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModel);
        }
        public ActionResult PageList(SHRTimesModel SModels)
        {
            var PageList = LSer.HRTimeList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Work(SHRTimesModel SModels)
        {
            SHRTimesModel models = new SHRTimesModel();
            models = LSer.GetDetailById(SModels);
            if (models != null)
            { SModels = models; }
            return View(SModels);
        }
        [ValidateInput(false)]
        public ActionResult PostWork(SHRTimesModel Models)
        {
            if (LSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult CWWork(int Id)
        {
            var models = LSer.GetCWTime(Id);
            return View(models);
        }
        [ValidateInput(false)]
        public ActionResult PostCWWork(HRTimesModel Models)
        {
            if (LSer.EditCWTime(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
    }
}
