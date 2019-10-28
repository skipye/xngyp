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
        private static readonly LabelsService LSer = new LabelsService();
        private static readonly ToExcel ESer = new ToExcel();
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
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (LSer.DeleteMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult MoveINV(string ListId)
        {
            LabelsModel Models = new LabelsModel();
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.ListId = ListId;
            return View(Models);
        }
        public ActionResult PostMoveINV(string ListId,int INVId)
        {
            if (LSer.MoveINV(ListId, INVId) == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        public void ToExcelOut(SLabelsModel SModels)
        {
            var models = LSer.ToExcelOut(SModels);
            ESer.CreateExcel(models, "成品库库存" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public ActionResult WorkLabels(SLabelsModel SModel,int CRMId,int Qty)
        {
            var Models = LSer.GetWorkLabelsList(SModel);
            ViewBag.CRMId = CRMId;
            ViewBag.Qty = Qty;
            return View(Models); 
        }
        //绑定库存产品
        public ActionResult CheckLabels(string ListId, int CRM_Id)
        {
            if (LSer.BindLabels(ListId, CRM_Id) == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        //产品确认
        public ActionResult Check(string ListId)
        {
            SemiModel Models = new SemiModel();
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.ListId = ListId;
            return View(Models);
        }
        public ActionResult PostCheck(string ListId, int INVId,int Grade)
        {
            if (LSer.CheckMore(ListId, INVId, Grade) == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        public ActionResult Delivery(string ListId)
        {
            if (LSer.DeliveryMore(ListId) == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
    }
}
