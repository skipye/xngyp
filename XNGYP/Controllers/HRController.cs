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
                SModel.columnCount = datetime.AddDays(1 - datetime.Day).AddDays(-1).Day;
                SModel.StrColumn = "";
                for (int j = 1; j <= SModel.columnCount; j++)
                {
                    string Dian = ",";
                    if (j == Convert.ToInt32(SModel.columnCount))
                    { Dian = ""; }
                    SModel.StrColumn += "{ \"data\": \"d"+j+"\", \"defaultContent\": \"\", \"className\": \"Stextoverflow\" }"+ Dian + "";
                }
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
        
    }
}
