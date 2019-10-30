using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class SaleStartController : Controller
    {
        private static readonly SaleStartService SSSer = new SaleStartService();
        private static readonly WorkOrderService WOSer = new WorkOrderService();
        [Authorize]
        public ActionResult Index()
        {
            SContractProductsModel Models = new SContractProductsModel();
            return View(Models);
        }
        public ActionResult PageList(SContractProductsModel SModels)
        {
            var PageList = SSSer.GetPageList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Work()
        {
            SWorkOrderModel Models = new SWorkOrderModel();
            return View(Models);
        }
        public ActionResult WorkPage(int? Status)
        {
            var PageList = WOSer.GetPageList(Status,true);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
    }
}
