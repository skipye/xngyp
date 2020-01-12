using ModelProject;
using ServiceProject;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class AdminController : Controller
    {
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly DeliveryService DSer = new DeliveryService();
        private static readonly LabelsService LSer = new LabelsService();
        private static readonly SemiService SSer = new SemiService();
        private static readonly FinanceService FSer = new FinanceService();
        private static readonly UserService USer = new UserService();
        private static readonly WorkOrderService WSer = new WorkOrderService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _Header()
        {
            return View();
        }
        public ActionResult _Menu()
        {
            return View();
        }
        public ActionResult _Footer()
        {
            return View();
        }
        public ActionResult _Meta()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        //出库情况
        public ActionResult _CK()
        {
            CKModel CModel = new CKModel();
            CModel.FCK = DSer.GetFDeliveCount();
            CModel.CK = DSer.GetDeliveCount();
            return View(CModel);
        }
        //家具出库列表
        public ActionResult FDeliveryList(SLabelsModel SModel)
        {
            var List = DSer.GetFDeliveryList(SModel);
            return View(List);
        }
        public ActionResult DeliveryList(SLabelsModel SModel)
        {
            SModel.Status = 1;
            var List = DSer.GetDeliveryList(SModel);
            return View(List);
        }
        //库存情况
        public ActionResult _KC()
        {
            KCModel KModel = new KCModel();
            KModel.FKC = LSer.GetFLabelsCount();
            KModel.KC = LSer.GetLabelsCount();
            KModel.FSKC = SSer.GetFSemiCount();
            KModel.SKC = SSer.GetSemiCount();
            return View(KModel);
        }
        public ActionResult FLabelsList(SLabelsModel SModel)
        {
            var List = LSer.GetFLabelsList(SModel);
            return View(List);
        }
        public ActionResult LabelsList(SLabelsModel SModel)
        {
            var List = LSer.GetLabelsList(SModel);
            return View(List);
        }
        //销售情况
        public ActionResult _XS()
        {
            XSModel Model = new XSModel();
            Model.FSXHT = CHSer.GetFHTCount();
            Model.FSX = CHSer.GetFXSCount();
            Model.SXHT = CHSer.GetHTCount();
            Model.SX = CHSer.GetXSCount();
            return View(Model);
        }
        public ActionResult FHTList(SContractHeaderModel SModel)
        {
            var List = CHSer.GetFPageList(SModel).data;
            return View(List);
        }
        public ActionResult HTList(SContractHeaderModel SModel)
        {
            SModel.CheckState = 1;
            var List = CHSer.GetPageList(SModel).data;
            return View(List);
        }
        //收款情况
        public ActionResult _SK()
        {
            SKModel Model = new SKModel();
            Model.FSK = FSer.GetFSKCount();
            Model.SK = FSer.GetSKCount();
            return View(Model);
        }
        public ActionResult FSKList(SContractHeaderModel SModel)
        {
            var List = FSer.GetFSKList(SModel);
            return View(List);
        }
        public ActionResult SKList(SContractHeaderModel SModel)
        {
            var List = FSer.GetSKList(SModel);
            return View(List);
        }
        public ActionResult _HRList()
        {
            var List = USer.GetHRList();
            return View(List);
        }
        public ActionResult _GX()
        {
            GXCountModel Models = new GXCountModel();
            Models.FGX = WSer.GetFGXCount();
            Models.GX = WSer.GetGXCount();
            return View(Models);
        }
        public ActionResult FGXList(SWorkFromModel SModel)
        {
            SModel.Status = 1;
            var List = WSer.GetFFlowList(SModel);
            return View(List);
        }
        public ActionResult GXList(SWorkFromModel SModel)
        {
            SModel.Status = 1;
            var List = WSer.GetFlowList(SModel);
            return View(List);
        }
    }
}
