using ModelProject;
using System;
using System.Web.Mvc;
using ServiceProject;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class LabelsController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly LabelsService LSer = new LabelsService();
        private static readonly ToExcel ESer = new ToExcel();
        [Authorize]
        public ActionResult Index(SLabelsModel Models)
        {
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        [Authorize]
        public ActionResult Cost(SLabelsModel Models)
        {
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        public ActionResult UserIndex()
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
        public ActionResult Edit(int Id)
        {
            LabelsModel Models = new LabelsModel();
            Models.qty = 1;
            if (Id > 0)
            {
                Models = LSer.GetDetailById(Id);
            }
            Models.XLDroList = CHSer.GetProSNDrolist(Models.ProductSNId);
            Models.CKDroList = CHSer.GetCKDrolist(Models.INVId, 4);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            Models.SHDroList = CHSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        public ActionResult EditCost(int Id)
        {
            LabelsModel Models = new LabelsModel();
            if (Id > 0)
            {
                Models = LSer.GetDetailById(Id);
            }
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostEdit(LabelsModel Models)
        {
            if (LSer.Edit(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        [ValidateInput(false)]
        public ActionResult PostEditCost(LabelsModel Models)
        {
            if (LSer.EditCost(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
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
            ESer.CreateExcel(models, "工艺品库存" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
        public ActionResult WorkLabels(SLabelsModel SModel,int CRMId,int Qty)
        {
            var Models = LSer.GetWorkLabelsList(SModel);
            ViewBag.CRMId = CRMId;
            ViewBag.Qty = Qty;
            return View(Models); 
        }
        //绑定库存产品
        public ActionResult BindLabels(string ListId, int CRM_Id)
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
