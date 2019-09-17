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
    public class WarehouseController : Controller
    {
        private static readonly INVService CSer = new INVService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PageList()
        {
            var PageList = CSer.GetPageList();
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            INV_NameModel Models = new INV_NameModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetDetailById(Id.Value);
            }
            Models.TypeDroList = CSer.GetType(Models.TypeId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(INV_NameModel Models)
        {
            if (CSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult Delete(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (CSer.DeleteMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }

    }
}
