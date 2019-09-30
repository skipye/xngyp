using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class PreCastController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly WorkOrderService WOSer = new WorkOrderService();
        private static readonly PreCastService LSer = new PreCastService();
        public ActionResult Index()
        {
            SPreCastModel Models = new SPreCastModel();
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        public ActionResult PageList(SPreCastModel SModels)
        {
            var PageList = LSer.GetList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            PreCastModel Models = new PreCastModel();
            if (Id > 0)
            {
                Models = LSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(PreCastModel Models)
        {
            if (LSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult Work()
        {
            SWorkOrderModel Models = new SWorkOrderModel();
            return View(Models);
        }
        public ActionResult WorkPage(int? Status)
        {
            var PageList = WOSer.GetPageList(Status, false);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
    }
}
