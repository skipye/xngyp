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
    public class CostController : Controller
    {
        private static readonly CostService CSer = new CostService();
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        public ActionResult Index()
        {
            SCostModel SModels = new SCostModel();
            SModels.XLDroList = CHSer.GetProSNDrolist(SModels.ProductSNId);
            SModels.MCDroList = CHSer.GetWoodDrolist(SModels.WoodId);
            return View(SModels);
        }
        public ActionResult PageList(SCostModel SModels)
        {
            var PageList = CSer.GetPageList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            CostModel Models = new CostModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(CostModel Models)
        {
            if (CSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }

        //删除多个
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
