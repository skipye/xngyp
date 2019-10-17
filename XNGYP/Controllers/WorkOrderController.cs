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
        public ActionResult AddFrom(string ListId)
        {
            WorkFromModel models = new WorkFromModel();
            models.ListId = ListId;
            return View(models);
        }
        [HttpPost]
        public ActionResult AddFromPost(WorkFromModel Models,string ListId)
        {
            if (WSer.AddWorlFrom(Models,ListId) == true)
            {
                return Content("1");
            }
            else { return Content("0"); }
        }
        public ActionResult UserDroListByJob(string Job)
        {
            var List = WSer.GetUserDrolistByJob(Job);
            return Content(List.ToString());
        }
        public ActionResult Show(int Id)
        {
            var Models = WSer.GetWorkOrderEvenList(Id);
            return View(Models);
        }
    }
}
