using ModelProject;
using ServiceProject;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class AdminController : Controller
    {
        private static readonly CostService CSer = new CostService();
        private static readonly ContractHeaderService CHSer = new ContractHeaderService();
        private static readonly ToExcel ESer = new ToExcel();
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
    }
}
