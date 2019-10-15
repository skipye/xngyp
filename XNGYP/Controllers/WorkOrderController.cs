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
    public class WorkOrderController : Controller
    {
        private static readonly WorkOrderService WSer = new WorkOrderService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageList(int? Status, bool IsSale)
        {
            var PageList = WSer.GetPageList(Status, IsSale);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(string ListId)
        {
            if (WSer.AddWorkOrder(ListId) == true)
            {
                return Content("1");
            }
            else { return Content("0"); }
        }
    
    }
}
