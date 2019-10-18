using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class SemiController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly SemiService LSer = new SemiService();
        private static readonly ToExcel ESer = new ToExcel();
        public ActionResult Index()
        {
            SSemiModel Models = new SSemiModel();
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        public ActionResult PageList(SSemiModel SModels)
        {
            var PageList = LSer.GetSemiList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            SemiModel Models = new SemiModel();
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
        public ActionResult PostAdd(SemiModel Models)
        {
            if (LSer.AddOrUpdate(Models) == true)
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
                if (LSer.DeleteMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult MoveINV(string ListId)
        {
            SemiModel Models = new SemiModel();
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
        public void ToExcelOut(SSemiModel SModels)
        {
            var models = LSer.ToExcelOut(SModels);
            ESer.CreateExcel(models, "成品库库存" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
