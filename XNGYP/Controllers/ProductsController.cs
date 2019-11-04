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
    public class ProductsController : Controller
    {
        private static readonly ProductsService CSer = new ProductsService();
        private static readonly ContractHeaderService NSer = new ContractHeaderService();
        [Authorize]
        public ActionResult Name()
        {
            return View();
        }
        public ActionResult NamePageList()
        {
            var PageList = CSer.GetPageList();
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult NameAdd(int? Id)
        {
            ProductsNameModel Models = new ProductsNameModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetDetailById(Id.Value);
            }
            Models.XLDroList = NSer.GetProSNDrolist(Models.ProductsSNId);
            Models.FatherDroList = NSer.GetFatherProSNDrolist(Models.FatherId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostNameAdd(ProductsNameModel Models)
        {
            if (CSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult NameDelete(string ListId)
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
        public ActionResult SN()
        {
            return View();
        }
        public ActionResult SNPageList()
        {
            var PageList = CSer.GetSNPageList();
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(PageList),
                ContentType = "application/json"
            };
        }
        public ActionResult SNAdd(int? Id)
        {
            ProductsSNModel Models = new ProductsSNModel();
            if (Id != null && Id > 0)
            {
                Models = CSer.GetSNDetailById(Id.Value);
            }
            Models.ProXLDroList = NSer.GetProSNDrolist(Models.Id);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostSNAdd(ProductsSNModel Models)
        {
            if (CSer.AddOrUpdateSN(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult SNDelete(string ListId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (CSer.DeleteSNMore(ListId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult GetProNameDrolistBySN(int? ProSN)
        {
            var List = CSer.GetProNameDrolistBySN(ProSN);
            return Content(List.ToString());
        }
        public ActionResult GetSecSNDrolistByFatherId(int? FatherId)
        {
            var List = CSer.GetSecSNDrolistByFatherId(FatherId);
            return Content(List.ToString());
        }
        public ActionResult GetDrolistByFatherId(int? FatherId, string SelectedId)
        {
            var List = CSer.GetDrolistByFatherId(FatherId, SelectedId);
            return Content(List.ToString());
        }
    }
}
