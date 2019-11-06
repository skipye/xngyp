using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class DeliveryController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly DeliveryService LSer = new DeliveryService();
        private static readonly ToExcel ESer = new ToExcel();
        [Authorize]
        public ActionResult Index()
        {
            SLabelsModel SModel = new SLabelsModel();
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModel.StartTime))
            {
                SModel.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModel.EndTime))
            {
                SModel.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            SModel.CKDroList = CHSer.GetCKDrolist(SModel.INVId, 4);
            return View(SModel);
        }
        public ActionResult PageList(SLabelsModel SModels)
        {
            var PageList = LSer.GetDeliveryList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        
        public ActionResult Delete(string ListId)
        {
            if (LSer.DeleteMore(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
            
        }
        public void ToExcelOut(SLabelsModel SModels)
        {
            var models = LSer.ToExcelOut(SModels);
            ESer.CreateExcel(models, "成品库库存" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        
        //产品确认
        public ActionResult Check(string ListId)
        {
            LabelsModel Models = new LabelsModel();
            Models.ListId = ListId;
            return View(Models);
        }
        public ActionResult PostCheck(string ListId, string OrderNum, DateTime DeliveryTime)
        {
            if (LSer.CheckMore(ListId, OrderNum, DeliveryTime) == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        //打印送货单
        public ActionResult Print(string ListId)
        {
            var models = LSer.PrintDelivery(ListId);
            return View(models);
        }
    }
}
