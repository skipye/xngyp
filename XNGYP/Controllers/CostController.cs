using ModelProject;
using ServiceProject;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class CostController : Controller
    {
        private static readonly CostService CSer = new CostService();
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        [Authorize]
        public ActionResult Index(SCostModel SModels)
        {
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
        public ActionResult FIndex()
        {
            SCostModel SModels = new SCostModel();
            SModels.XLDroList = CHSer.GetProFSNDrolist(SModels.ProductSNId);
            SModels.MCDroList = CHSer.GetWoodDrolist(SModels.WoodId);
            return View(SModels);
        }
        public ActionResult FPageList(SCostModel SModels)
        {
            var PageList = CSer.GetFPageList(SModels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult AddF(int? Id)
        {
            CostModel Models = new CostModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetFDetailById(Id.Value);
            }
            Models.XLDroList = CHSer.GetProFSNDrolist(Models.ProductSNId);
            Models.MCDroList = CHSer.GetWoodDrolist(Models.WoodId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAddF(CostModel Models)
        {
            if (CSer.AddOrUpdateF(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }

        //删除多个
        public ActionResult DeleteF(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (CSer.DeleteFMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult AddFCost()
        {
            if (CSer.UpdateFCost() == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        public ActionResult AddGYPCost()
        {
            if (CSer.AddGYPCost() == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
        public Decimal? GetChuChangPrice(int ProductId, int WoodId)
        {
            try { return CSer.GetChuChangPrice(ProductId, WoodId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Decimal? GetGYPCCPrice(int ProductId, int WoodId)
        {
            try { return CSer.GetGYPCCPrice(ProductId, WoodId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ActionResult UpdateGYPSN()
        {
            if (CSer.UpdateGYPSN() == true)
            {
                return Content("1");
            }
            else return Content("0");
        }
    }
}
