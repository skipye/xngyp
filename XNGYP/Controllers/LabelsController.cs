using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XiangNingSale.Controllers
{
    public class LabelsController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly LabelsService LSer = new LabelsService();
        public ActionResult Index()
        {
            SLabelsModel Models = new SLabelsModel();
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        public ActionResult PageList(SLabelsModel SModels)
        {
            var PageList = LSer.GetLabelsList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            LabelsModel Models = new LabelsModel();
            Models.qty = 1;
            if (Id > 0)
            {
                Models = LSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(LabelsModel Models)
        {
            if (LSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
    }
}
