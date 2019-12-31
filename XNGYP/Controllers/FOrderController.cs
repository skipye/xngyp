using ModelProject;
using ServiceProject;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class FOrderController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly CustomerService CSer = new CustomerService();
        private static readonly ContractHeaderService NSer = new ContractHeaderService();
        private static readonly ToExcel ESer = new ToExcel();
        [Authorize]
        public ActionResult Index()
        {
            SContractHeaderModel SModels = new SContractHeaderModel();
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModels.StartTime))
            {
                SModels.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModels.EndTime))
            {
                SModels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            SModels.DepartmentDroList = USer.GetDepartmentDrolist(SModels.DepartmentId);
            return View(SModels);
        }
        public ActionResult FPageList(SContractHeaderModel SRmodels)
        {
            var models = NSer.GetFPageList(SRmodels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(models),
                ContentType = "application/json"
            };
        }
        public ActionResult AddF(int? Id)
        {
            FContractProductsModel Models = new FContractProductsModel();
            Models.FContractHeadId = Id;
            Models.ProXLDroList = NSer.GetProFSNDrolist(Models.ProductXLId);
            Models.AreaDroList = NSer.GetProAreaDrolist(Models.ProductAreaId);
            Models.WoodDroList = NSer.GetWoodDrolist(Models.FWoodId);
            Models.ColorDroList = NSer.GetColorDrolist(Models.FColorId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAddProducts(FContractProductsModel Models)
        {
            if (NSer.AddFProducts(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        
        public ActionResult GetFProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            var List = NSer.GetFProNameDrolistBySNAndArea(ProSN, ProProArea);
            return Content(List.ToString());
        }
        
        
        public ActionResult ProductListByOrder(int Id)
        {
            var PageList = NSer.GetFProductListByOrder(Id);
            return View(PageList);
        }
        public ActionResult DeleteProduct(int Id)
        {
            if (NSer.DeleteFProduct(Id) == true)
            {
                return Content("1");
            }
            else { return Content("0"); }
        }
        public ActionResult Show(int Id)
        {
            var Models = NSer.GetFDetailById(Id);
            return View(Models);
        }
        public void ToExcelOut(SContractHeaderModel SModel)
        {
            var models = NSer.ToFExcelOut(SModel);
            ESer.CreateExcel(models, "家具订单" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
        }
    }
}
