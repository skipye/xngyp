using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using System.Web.Mvc;

namespace DalProject
{
    public class SaleStartDal
    {
        public List<ContractProductsModel> GetPageList(SContractProductsModel SModel)
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
                var List = (from p in db.Contract_Detail.Where(k => k.DeleteFlag == false && k.Contract_Header.Status==1 && k.Contract_Header.CWCheckStatus==1)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.Contract_Header.SN.Contains(SModel.SN) : true
                            where !string.IsNullOrEmpty(SModel.ProductName) ? p.ProductName.Contains(SModel.ProductName) : true
                            where !string.IsNullOrEmpty(SModel.Customer) ? p.Contract_Header.XNGYP_Customers.Name.Contains(SModel.Customer) : true
                            where SModel.SaleFlag!=null && SModel.SaleFlag==1?p.Contract_Header.SaleFlag==1:true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new ContractProductsModel
                            {
                                Id = p.Id,
                                SN = p.Contract_Header.SN,
                                Customer = p.Contract_Header.XNGYP_Customers.Name,
                                ProductSN = p.ProductSN,
                                ProductId=p.ProductId,
                                ProductName = p.ProductName,
                                delivery_date = p.Contract_Header.DeliveryDate,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                WoodName = p.WoodName,
                                WoodId=p.WoodId,
                                Color = p.Color,
                                Status = p.Status,
                                Qty = p.Qty,
                                LabelsCount = db.XNGYP_INV_Labels.Where(k => k.ProductsId == p.ProductId && k.WoodId == p.WoodId && k.Status == 1 && k.Flag!=2 && k.DeleteFlag == false).Count(),
                                SemiCount = db.XNGYP_INV_Semi.Where(k => k.ProductId == p.ProductId && k.WoodId == p.WoodId && k.Status == 1 && k.Flag != 1 && k.DeleteFlag == false).Count(),
                                
                            }).ToList();
                return List;
            }
        }
        public List<ContractProductsModel> GetBindOrderPageList(SContractProductsModel SModel)
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
                var List = (from p in db.Contract_Detail.Where(k => k.DeleteFlag == false && k.Contract_Header.Status == 1 && k.Status!=4)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.Contract_Header.SN.Contains(SModel.SN) : true
                            where !string.IsNullOrEmpty(SModel.ProductName) ? p.ProductName.Contains(SModel.ProductName) : true
                            where !string.IsNullOrEmpty(SModel.Customer) ? p.Contract_Header.XNGYP_Customers.Name.Contains(SModel.Customer) : true
                            where SModel.SaleFlag != null && SModel.SaleFlag == 1 ? p.Contract_Header.SaleFlag == 1 : true
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new ContractProductsModel
                            {
                                Id = p.Id,
                                SN = p.Contract_Header.SN,
                                Customer = p.Contract_Header.XNGYP_Customers.Name,
                                ProductSN = p.ProductSN,
                                ProductId = p.ProductId,
                                ProductName = p.XNGYP_Products.name,
                                delivery_date = p.Contract_Header.DeliveryDate,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                WoodName = p.WoodName,
                                WoodId = p.WoodId,
                                Color = p.Color,
                                Status = p.Status,
                                Qty = p.Qty,
                                LabelsCount = db.XNGYP_INV_Labels.Where(k => k.ProductsId == p.ProductId && k.WoodId == p.WoodId && k.Status == 1 && k.Flag != 2 && k.DeleteFlag == false).Count(),
                                SemiCount = db.XNGYP_INV_Semi.Where(k => k.ProductId == p.ProductId && k.WoodId == p.WoodId && k.Status == 1 && k.Flag != 1 && k.DeleteFlag == false).Count(),
                                DDOrder=p.Contract_Header.DDOrder,

                            }).ToList();
                return List;
            }
        }
    }
}
