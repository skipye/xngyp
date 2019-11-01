using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using System.Web.Mvc;

namespace DalProject
{
    public class ContractHeaderDal
    {
        public ContractModel GetPageList(SContractHeaderModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.Contract_Header.Where(k => k.DeleteFlag == false)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where SModel.CheckState == 1 ? p.Status == SModel.CheckState : true
                            where SModel.DepartmentId>0 ? p.SaleDepartmentId == SModel.DepartmentId : true
                            where SModel.SaleUserId > 0 ? p.SaleUserId == SModel.SaleUserId : true
                            where !string.IsNullOrEmpty(SModel.UserName) ? p.XNGYP_Customers.Name.Contains(SModel.UserName) : true
                            where SModel.FR_flag >= 0 ? p.FRFlag == SModel.FR_flag : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new ContractHeaderModel
                            {
                                Id = p.Id,
                                SN = p.SN,
                                Customer = p.XNGYP_Customers.Name,
                                CustomerId = p.CustomerId,
                                TelPhone = p.DeliveryLinkTel,
                                DeliveryDate = p.DeliveryDate,
                                DeliveryAddress = p.DeliveryAddress,
                                Amount = p.Amount,
                                Status = p.Status,
                                Prepay = p.Prepay,
                                SaleUserId = p.SaleUserId,
                                SaleUserName = p.SaleUserName,
                                SaleDepartmentId = p.SaleDepartmentId,
                                SaleDepartment = p.SaleDepartment,
                                CheckedUserName = p.CheckedUserName,
                                FRFlag = p.FRFlag,
                                OrderTime = p.HTDate,
                                CheckedTime=p.CheckedTime,
                                CreateTime = p.CreateTime,
                                CWCheckStatus=p.CWCheckStatus,
                                SHFlag=p.SHFlag,
                                DeliverChannel=p.DeliverChannel,
                                ZTDFlag=p.ZTDFlag,
                                SaleFlag=p.SaleFlag,
                            }).ToList();
                ContractModel Models = new ContractModel();
                Models.data = List;
                Models.HTTotail = List.Sum(k => k.Amount);
                return Models;
            }
        }
        
        public void AddOrUpdate(ContractHeaderModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                if (Models.Id > 0)
                {
                    var table = db.Contract_Header.Where(k => k.Id == Models.Id).SingleOrDefault();
                    table.CustomerId = Models.CustomerId;
                    table.DeliveryDate = Models.DeliveryDate;
                    table.DeliveryAddress = Models.DeliveryAddress;
                    table.DeliveryLinkTel = Models.DeliveryLinkTel;
                    table.DeliveryLinkMan = Models.DeliveryLinkMan;
                    table.Prepay = Models.Prepay;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                    table.DeliverChannel = Models.DeliverChannel;
                    table.SHFlag = Models.SHFlag;
                    table.ZTDFlag = Models.ZTDFlag;
                    table.RealPrice = Models.RealPrice;
                    table.FRStatus = Models.FRStatus;
                    table.SaleFlag = Models.SaleFlag;
                }
                else
                {
                    Contract_Header table = new Contract_Header();
                    table.SN = Models.SN;
                    table.CustomerId = Models.CustomerId;
                    table.DeliveryDate = Models.DeliveryDate;
                    table.DeliveryAddress = Models.DeliveryAddress;
                    table.DeliveryLinkTel = Models.DeliveryLinkTel;
                    table.DeliveryLinkMan = Models.DeliveryLinkMan;
                    table.Amount = 0;
                    table.Prepay = Models.Prepay;
                    table.SaleUserId = Models.SaleUserId;
                    table.SaleUserName = Models.SaleUserName;
                    table.SaleDepartmentId = Models.SaleDepartmentId;
                    table.SaleDepartment = Models.SaleDepartment;
                    table.Status = 0;
                    table.HTDate = Convert.ToDateTime(Models.HTDate);
                    table.FRFlag = 0;
                    table.CreateTime = DateTime.Now;
                    table.DeleteFlag = false;
                    table.CWCheckStatus = false;
                    table.DeliverChannel = Models.DeliverChannel;
                    table.SHFlag = Models.SHFlag;
                    table.ZTDFlag = Models.ZTDFlag;
                    table.RealPrice = Models.RealPrice;
                    table.FRStatus = Models.FRStatus;
                    table.SaleFlag = Models.SaleFlag;
                    db.Contract_Header.Add(table);

                    FinanceModel FModels = new FinanceModel();
                    FModels.Id = Models.Id;
                    FModels.PayStatus = Models.FRStatus;
                    FModels.Amount = Models.RealPrice;
                }
                db.SaveChanges();
            }
        }
        public ContractHeaderModel GetDetailById(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = (from p in db.Contract_Header.Where(k => k.DeleteFlag == false && k.Id == Id)

                              orderby p.CreateTime descending
                              select new ContractHeaderModel
                              {
                                  Id = p.Id,
                                  SN = p.SN,
                                  Customer = p.XNGYP_Customers.Name,
                                  CustomerId = p.CustomerId,
                                  DeliveryDate = p.DeliveryDate,
                                  Amount = p.Amount,
                                  Status = p.Status,
                                  Prepay = p.Prepay,
                                  DeliveryAddress = p.DeliveryAddress,
                                  SaleUserId = p.SaleUserId,
                                  SaleUserName = p.SaleUserName,
                                  SaleDepartmentId = p.SaleDepartmentId,
                                  SaleDepartment = p.SaleDepartment,
                                  CheckedUserName = p.CheckedUserName,
                                  FRFlag = p.FRFlag,
                                  OrderTime = p.HTDate,
                                  CheckedTime = p.CheckedTime,
                                  DeliveryLinkMan=p.DeliveryLinkMan,
                                  DeliveryLinkTel=p.DeliveryLinkTel,
                                  SHFlag = p.SHFlag,
                                  DeliverChannel=p.DeliverChannel,
                                  ZTDFlag = p.ZTDFlag,
                                  RealPrice = p.RealPrice,
                                  FRStatus = p.FRStatus,
                                  SaleFlag = p.SaleFlag,
                              }).SingleOrDefault();
                return tables;
            }
        }
       
        public void Delete(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.DeleteFlag = true;
                    }
                }
                db.SaveChanges();
            }
        }
        public void Checked(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.Status = 1;
                        tables.CheckedUserId = new UserDal().GetCurrentUserName().UserId;
                        tables.CheckedUserName = new UserDal().GetCurrentUserName().UserName;
                        tables.CheckedTime = DateTime.Now;
                    }
                }
                db.SaveChanges();
            }
        }
        public void CWChecked(string ListId)
        {
            using (var db = new XNGYPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.Contract_Header.Where(k => k.Id == Id).SingleOrDefault();
                        tables.CWCheckStatus = true;
                        tables.CWCheckId = new UserDal().GetCurrentUserName().UserId;
                        tables.CWCheckName = new UserDal().GetCurrentUserName().UserName;
                        tables.CWCheckTime = DateTime.Now;

                        AddFOrder(Id);//添加家具订单
                    }
                }

                db.SaveChanges();
            }
        }

        //根据日期获取合同总个数
        public int GetCRMHTCount(DateTime CreateTime)
        {
            using (var db = new XNGYPEntities())
            {
                var List = db.Contract_Header.Where(k => k.CreateTime > CreateTime).Count();
                return List;
            }
        }
        public List<SelectListItem> GetProSNDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择系列一级", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<XNGYP_Products_SN> model = db.XNGYP_Products_SN.Where(b => b.delete_flag == false && b.FatherId ==null).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name+"_"+item.SN, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetFatherProSNDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择系列二级", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<XNGYP_Products_SN> model = db.XNGYP_Products_SN.Where(b => b.delete_flag == false && b.FatherId >0).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name + "_" + item.SN, Value = item.Id.ToString() });
                }
            }
            return items;
        }
        public List<SelectListItem> GetProFSNDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择产品系列", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_product_SN> model = db.SYS_product_SN.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        //产品区域
        public List<SelectListItem> GetProAreaDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择产品区域", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_product_area> model = db.SYS_product_area.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetWoodDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择材质", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_wood_type> model = db.INV_wood_type.Where(b => b.delete_flag == false && b.SN!=null).OrderBy(k => k.SN).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name + "_" + item.SN, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        //根据材质ID获取材质标签
        public string GetWoodSN(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var model = db.INV_wood_type.Where(b => b.delete_flag == false && b.id == Id).Select(k => k.SN).FirstOrDefault();
                return model;
            }
            
        }
        public List<SelectListItem> GetColorDrolist(int? pId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择色号", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<SYS_colors> model = db.SYS_colors.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetCKDrolist(int? pId, int? CKType)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择仓库", Value = "" });
            using (var db = new XNGYPEntities())
            {
                List<INV_Name> model = db.INV_Name.Where(b => b.DeleteFlag == false).OrderBy(k => k.CreateTime).ToList();
                if (CKType != null && CKType > 0)
                {
                    model = db.INV_Name.Where(b => b.DeleteFlag == false && b.Type == CKType).OrderBy(k => k.CreateTime).ToList();
                }
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text =item.Name, Value = item.Id.ToString(), Selected = pId.HasValue && item.Id.Equals(pId) });
                }
            }
            return items;
        }
        public List<SelectListItem> GetFCKDrolist(int? pId, int? CKType)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "请选择仓库", Value = "" });
            using (var db = new XNERPEntities())
            {
                List<INV_inventories> model = db.INV_inventories.Where(b => b.delete_flag == false).OrderBy(k => k.created_time).ToList();
                if (CKType != null && CKType > 0)
                {
                    model = db.INV_inventories.Where(b => b.delete_flag == false && b.type == CKType).OrderBy(k => k.created_time).ToList();
                }
                foreach (var item in model)
                {
                    items.Add(new SelectListItem() { Text = item.name, Value = item.id.ToString(), Selected = pId.HasValue && item.id.Equals(pId) });
                }
            }
            return items;
        }
        public string GetFProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.SYS_product.Where(k => k.delete_flag == false)
                            where ProSN > 0 ? p.product_SN_id == ProSN : true
                            where ProProArea > 0 ? p.product_area_id == ProProArea : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.id,
                                Name = p.name,
                                length = p.length,
                                width = p.width,
                                height = p.height
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.length + "_" + item.width + "_" + item.height;
                    var IstrValue = item.Id;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            using (var db = new XNGYPEntities())
            {
                var list = (from p in db.XNGYP_Products.Where(k => k.delete_flag == false)
                            where ProSN > 0 ? p.ProductsSNId == ProSN : true
                            orderby p.created_time descending
                            select new CRMItem
                            {
                                Id = p.Id,
                                Name = p.name,
                                length = p.length,
                                width = p.width,
                                height = p.height
                            }).ToList();
                string NewItme = "";
                foreach (var item in list)
                {
                    var strText = item.Name + "_" + item.length + "_" + item.width + "_" + item.height;
                    var IstrValue = item.Id;
                    NewItme += "<option value=" + IstrValue + ">" + strText + "</option>";
                }
                return NewItme;
            }
        }
        public void AddProducts(ContractProductsModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                decimal price = 0;
                if (Models.price > 0)
                { price = Models.price.Value; }

                Contract_Detail table = new Contract_Detail();
                table.ContractHeadId = Models.ContractHeadId;
                table.ProductId = Models.ProductId;
                table.ProductSN = Models.ProductXL+"_"+Models.ProductSN;
                table.ProductName = Models.ProductName;
                table.ColorId = Models.ColorId;
                table.Color = Models.Color;
                table.WoodId = Models.WoodId;
                table.WoodName = Models.WoodName;
                table.CustomFlag = Models.CustomFlag;
                table.length = Models.length;
                table.width = Models.width;
                table.height = Models.height;
                table.Price = price;
                table.hardware_part = Models.hardware_part;
                table.decoration_part = Models.decoration_part;
                table.req_others = Models.req_others;
                table.CreateTime = DateTime.Now;
                table.DeleteFlag = false;
                table.Status = 0;
                table.Qty = Models.Qty;
                table.LabelseCount = 0;
                db.Contract_Detail.Add(table);

                var HeadTable = db.Contract_Header.Where(k => k.Id == Models.ContractHeadId).SingleOrDefault();
                HeadTable.Amount = HeadTable.Amount + price * Models.Qty;
                db.SaveChanges();

            }
        }
        public void AddFProducts(FContractProductsModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                decimal price = 0;
                if (Models.Price > 0)
                { price = Models.Price.Value; }

                Contract_FDetail table = new Contract_FDetail();
                table.ContractHeadId = Models.FContractHeadId;
                table.ProductId = Models.FProductId;
                table.ProductArea = Models.ProductArea;
                table.ProductSN = Models.FProductXL;
                table.ProductName = Models.FProductName;
                table.ColorId = Models.FColorId;
                table.Color = Models.FColor;
                table.WoodId = Models.FWoodId;
                table.WoodName = Models.FWoodName;
                table.CustomFlag = Models.CustomFlag;
                table.length = Models.Length;
                table.width = Models.Width;
                table.height = Models.Height;
                table.Price = price;
                table.price_recommend = 0;
                table.Qty = Models.FQty;
                table.Unit = "件";
                table.hardware_part = Models.hardware_part;
                table.decoration_part = Models.decoration_part;
                table.req_others = Models.Others;
                table.CreateTime = DateTime.Now;
                table.DeleteFlag = false;
                table.Status = 0;
                db.Contract_FDetail.Add(table);

                var HeadTable = db.Contract_Header.Where(k => k.Id == Models.FContractHeadId).FirstOrDefault();
                HeadTable.Amount = HeadTable.Amount + price * Models.FQty;
                db.SaveChanges();

            }
        }
        public void DeleteOne(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = db.Contract_Detail.Where(k => k.Id == Id).SingleOrDefault();
                tables.DeleteFlag = true;

                var HeadId = tables.ContractHeadId;
                var HeadTable = db.Contract_Header.Where(k => k.Id == HeadId).FirstOrDefault();
                if(HeadTable.Amount>= tables.Price.Value*tables.Qty) { 
                   HeadTable.Amount = HeadTable.Amount - tables.Price.Value * tables.Qty;
                }
                db.SaveChanges();
            }
        }
        //删除家具产品
        public void DeleteFProduct(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var tables = db.Contract_FDetail.Where(k => k.Id == Id).SingleOrDefault();
                tables.DeleteFlag = true;

                var HeadId = tables.ContractHeadId;
                var HeadTable = db.Contract_Header.Where(k => k.Id == HeadId).FirstOrDefault();
                if (HeadTable.Amount >= tables.Price.Value * tables.Qty)
                {
                    HeadTable.Amount = HeadTable.Amount - tables.Price.Value * tables.Qty;
                }
                db.SaveChanges();
            }
        }
        public List<ContractProductsModel> GetProductListByOrder(int HTId)
        {
            List<ContractProductsModel> Models = new List<ContractProductsModel>();
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.Contract_Detail.Where(k => k.DeleteFlag == false && k.ContractHeadId == HTId)
                            orderby p.CreateTime descending
                            select new ContractProductsModel
                            {
                                Id = p.Id,
                                ContractHeadId = p.ContractHeadId,
                                ProductName = p.ProductName,
                                ProductSN = p.ProductSN,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                CustomFlag = p.CustomFlag,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                price = p.Price,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                Status = p.Contract_Header.Status,
                                Qty=p.Qty,
                                IsJJ=1,
                            }).ToList();
                var FList= (from p in db.Contract_FDetail.Where(k => k.DeleteFlag == false && k.ContractHeadId == HTId)
                            orderby p.CreateTime descending
                            select new ContractProductsModel
                            {
                                Id = p.Id,
                                ContractHeadId = p.ContractHeadId,
                                ProductName = p.ProductName,
                                ProductSN = p.ProductSN,
                                Color = p.Color,
                                WoodName = p.WoodName,
                                CustomFlag = p.CustomFlag,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                price = p.Price,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                Status = p.Contract_Header.Status,
                                Qty = p.Qty,
                                IsJJ=2,
                            }).ToList();
                Models = List.Union(FList).ToList<ContractProductsModel>();
                return Models;
            }
        }
        //添加家具订单
        public void AddFOrder(int Id)
        {
            ContractHeaderModel CHModels = new ContractHeaderModel();
            List<ContractProductsModel> CHDetailModels = new List<ContractProductsModel>();
            bool IsF = false;
            using (var db = new XNGYPEntities())
            {
                var CHDetail = (from p in db.Contract_FDetail.Where(k => k.ContractHeadId == Id && k.DeleteFlag == false)
                                orderby p.CreateTime descending
                                select new ContractProductsModel
                                {
                                    ColorId = p.ColorId,
                                    Color = p.Color,
                                    ProductId = p.ProductId,
                                    WoodId = p.WoodId,
                                    CustomFlag = p.CustomFlag,
                                    length = p.length,
                                    width = p.width,
                                    height = p.height,
                                    price = p.Price,
                                    Qty = p.Qty,
                                    hardware_part = p.hardware_part,
                                    decoration_part = p.decoration_part,
                                    req_others = p.req_others,
                                }).ToList();
                if (CHDetail != null && CHDetail.Any())
                {
                    CHDetailModels = CHDetail;
                    var CHTabl = (from p in db.Contract_Header.Where(k => k.Id == Id)
                                  select new ContractHeaderModel
                                  {
                                      SN = p.SN,
                                      OrderTime = p.HTDate,
                                      DeliveryDate = p.DeliveryDate,
                                      DeliverChannel = p.DeliverChannel,
                                      DeliveryAddress = p.DeliveryAddress,
                                      Customer = p.DeliveryLinkMan,
                                      TelPhone = p.DeliveryLinkTel,
                                  }).SingleOrDefault();
                    CHModels = CHTabl;
                    IsF = true;
                }
            }
            if (IsF == true) { 
                using (var db = new XNERPEntities())
                {
                    CRM_contract_header table = new CRM_contract_header();
                    table.SN = CHModels.SN;
                    table.HTDate = Convert.ToDateTime(CHModels.OrderTime);
                    table.customer_id = 213;
                    table.delivery_date = CHModels.DeliveryDate;
                    table.amount = 0;
                    table.delivery_channel = CHModels.DeliverChannel;
                    table.freight_carrier = CHModels.FreightCarrier;
                    table.prepay = 0;
                    table.measure_flag = CHModels.MeasureFlag;
                    table.delivery_address = CHModels.DeliveryAddress;
                    table.Linkman = CHModels.Customer;
                    table.Linktel = CHModels.TelPhone;
                    table.signed_user_id = 0;
                    table.signed_department_id = 0;
                    table.department = "唐锐";
                    table.SaleName = "唐锐";
                    table.status = 0;
                    table.created_time = DateTime.Now;
                    table.delete_flag = false;
                    db.CRM_contract_header.Add(table);
                    db.SaveChanges();
                    var HeaderId = table.id;
                    if (CHDetailModels != null && CHDetailModels.Any())
                    {
                        foreach (var item in CHDetailModels)
                        {
                            CRM_contract_detail Detailtable = new CRM_contract_detail();
                            Detailtable.header_id = HeaderId;
                            Detailtable.color_id = item.ColorId ?? 0;
                            Detailtable.color = item.Color;
                            Detailtable.product_id = item.ProductId??0;
                            Detailtable.wood_type_id = item.WoodId ?? 0;
                            Detailtable.custom_flag = item.CustomFlag;
                            Detailtable.length = item.length;
                            Detailtable.width = item.width;
                            Detailtable.height = item.height;
                            Detailtable.price = 0;
                            Detailtable.qty = item.Qty ?? 0;
                            Detailtable.hardware_part = item.hardware_part;
                            Detailtable.decoration_part = item.decoration_part;
                            Detailtable.req_others = item.req_others;
                            Detailtable.created_time = DateTime.Now;
                            Detailtable.delete_flag = false;
                            Detailtable.status = 0;
                            db.CRM_contract_detail.Add(Detailtable);
                        }

                    }
                    db.SaveChanges();
                }
            }
        }


    }
}
