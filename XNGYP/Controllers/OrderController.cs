﻿using ModelProject;
using ServiceProject;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XNGYP.Controllers
{
    public class OrderController : Controller
    {
        private static readonly UserService USer = new UserService();
        private static readonly CustomerService CSer = new CustomerService();
        private static readonly ContractHeaderService NSer = new ContractHeaderService();
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
            //SModels.DepartmentDroList = USer.GetDepartmentDrolist(SModels.DepartmentId);
            return View(SModels);
        }
        public ActionResult PageList(SContractHeaderModel SRmodels)
        {
            var models = NSer.GetPageList(SRmodels);
            return new ContentResult
            {
                Content = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(models),
                ContentType = "application/json"
            };
        }
        public ActionResult Add(int? Id)
        {
            DateTime dt = new DateTime(DateTime.Now.Year, 1, 1);
            ContractHeaderModel Models = new ContractHeaderModel();
            if (Id != null && Id > 0)
            {
                Models = NSer.GetDetailById(Id.Value);
                Models.HTDate = Convert.ToDateTime(Models.OrderTime).ToString("yyyy-MM-dd");
            }
            else
            {
                int Count = NSer.GetCRMHTCount(dt) + 1;

                Models.SN = "GY00" + DateTime.Now.ToString("yy") + Count.ToString("0000");
                Models.HTDate = DateTime.Now.ToString("yyyy-MM-dd");
                Models.DeliveryDate= DateTime.Now.AddDays(3);
            }
            int UserId = USer.GetCurrentUserName().UserId;
            Models.CustomerDroList = CSer.GetCustomerDrolist(Models.CustomerId, UserId, 0);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAdd(ContractHeaderModel Models)
        {
            Models.SaleUserId = USer.GetCurrentUserName().UserId;
            Models.SaleUserName = USer.GetCurrentUserName().UserName;
            Models.SaleDepartmentId = USer.GetCurrentUserName().departmentId;
            Models.SaleDepartment = USer.GetCurrentUserName().department;
            Models.DeliveryLinkMan = Models.Customer;
            int CustomId = 0;
            if (Models.CustomerId == null || Models.CustomerId <= 0)//添加客户信息
            {
                CustomerModel CModels = new CustomerModel();
                CModels.Name = Models.Customer;
                CModels.ShortName = Models.Customer;
                CModels.Address = Models.DeliveryAddress;
                CModels.Address_Delivery = Models.DeliveryAddress;
                CModels.LinkMan = Models.DeliveryLinkMan;
                CModels.LinkTel = Models.DeliveryLinkTel;
                CModels.BelongUserId = Models.SaleUserId;
                CModels.BelongUserName = Models.SaleUserName;
                CModels.DepartmentId = Models.SaleDepartmentId;
                CModels.Department = Models.SaleDepartment;
                CSer.AddOrUpdate(CModels, out CustomId);
                Models.CustomerId = CustomId;
            }
            if (NSer.AddOrUpdate(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        [Authorize]
        public ActionResult UserIndex()
        {
            SContractHeaderModel SModels = new SContractHeaderModel();
            SModels.SaleUserId = USer.GetCurrentUserName().UserId;
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModels.StartTime))
            {
                SModels.StartTime = datetime.AddDays(1 - datetime.Day).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModels.EndTime))
            {
                SModels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModels);
        }
        [Authorize]
        public ActionResult OrderSearch()
        {
            SContractHeaderModel SModels = new SContractHeaderModel();
            SModels.SaleUserId = USer.GetCurrentUserName().UserId;
            DateTime datetime = DateTime.Now;
            if (string.IsNullOrEmpty(SModels.StartTime))
            {
                SModels.StartTime = datetime.AddDays(1 - datetime.Day).AddMonths(-3).ToString("yyyy-MM-dd");
            }
            if (string.IsNullOrEmpty(SModels.EndTime))
            {
                SModels.EndTime = datetime.AddDays(1 - datetime.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            return View(SModels);
        }
        public ActionResult DepartmentIndex()
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
            return View(SModels);
        }
        public ActionResult Delete(string ListId)
        {
            if (NSer.Delete(ListId) == true)
            {
                return Content("True");
            }
            else return Content("False");
            
        }
        public ActionResult Checked(string ListId, int CheckedId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (NSer.Checked(ListId, CheckedId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        public ActionResult CWChecked(string ListId, int CheckedId)
        {
            if (string.IsNullOrEmpty(ListId) == true)
            {
                return Content("False");
            }
            else
            {
                if (NSer.CWChecked(ListId, CheckedId) == true)
                {
                    return Content("True");
                }
                else return Content("False");
            }
        }
        //添加产品
        public ActionResult AddProducts(int Id)
        {
            ViewBag.CHeadId = Id;
            return View();
        }
        //添加工艺品产品
        public ActionResult AddGYPProducts(int Id)
        {
            ContractProductsModel Models = new ContractProductsModel();
            Models.ContractHeadId = Id;
            Models.ProXLDroList = NSer.GetProSNDrolist(Models.ProductSNId);
            Models.FatherDroList = NSer.GetFatherProSNDrolist(Models.FatherId);
            Models.WoodDroList = NSer.GetWoodDrolist(Models.WoodId);
            Models.colorDroList = NSer.GetColorDrolist(Models.ColorId);
            return View(Models);
        }
        [ValidateInput(false)]
        public ActionResult PostAddProducts(ContractProductsModel Models)
        {
            if (NSer.AddProducts(Models) == true)
            {
                return Content("1");
            }
            else { return View(Models); }
        }
        public ActionResult ProductListByOrder(int Id)
        {
            var PageList = NSer.GetProductListByOrder(Id);
            return View(PageList);
        }
        public ActionResult DeleteProduct(int Id)
        {
            if (NSer.DeleteOne(Id) == true)
            {
                return Content("1");
            }
            else { return Content("0"); }
        }
        public ActionResult Show(int Id)
        {
            var Models = NSer.GetDetailById(Id);
            return View(Models);
        }
        public ActionResult Print(int Id)
        {
            ContractHeaderModel Models = new ContractHeaderModel();
            Models= NSer.GetDetailById(Id);
            Models.HTProList = NSer.GetProductListByOrder(Id);
            return View(Models);
        }
        [Authorize]
        //绑定订单
        public ActionResult BindOrder()
        {
            SContractProductsModel model = new SContractProductsModel();
            return View(model);
        }
    }
}
