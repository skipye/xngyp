﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    public class CRMItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string tel { get; set; }
        public int? department_id { get; set; }
        public string department { get; set; }
        public DateTime? time1 { get; set; }
        public DateTime? time2 { get; set; }
        public DateTime? time3 { get; set; }
        public DateTime? time4 { get; set; }
        public DateTime? time5 { get; set; }
        public DateTime? time6 { get; set; }
        public string label { get; set; }
        public string unit { get; set; }
    }
    public class CRMByUser
    {
        public int KHId { get; set; }
        public string ListUserId { get; set; }
        public List<CRMItem> SaleUserList { get; set; }
        
    }
    public class CRM_KHModel
    {
        public int id { get; set; }
        public string SN { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public string address { get; set; }
        public string address_delivery { get; set; }
        public string linkman { get; set; }
        public string tel { get; set; }
        public string remark { get; set; }
        public DateTime? created_time { get; set; }
        public int UserId { get; set; }
    }
    public class SCRM_KHModel
    {
        public string name { get; set; }
    }
    //带销售总额的model
    public class ContractModel
    {
        public List<ContractHeaderModel> data { get; set; }
        public decimal? HTTotail { get; set; }
    }
    //销售合同表
    public class ContractHeaderModel
    {
        public int Id { get; set; }
        public string SN { get; set; }
        public int? CustomerId { get; set; }
        public string Customer { get; set; }
        public string TelPhone { get; set; }
        public DateTime? DeliveryDate { get; set; }//送货日期
        public decimal? Amount { get; set; }//合同总金额
        public string DeliverChannel { get; set; }//送货通道：楼梯/电梯
        public string FreightCarrier { get; set; }//运费承担方：甲/乙
        public decimal? Prepay_percent { get; set; }//预付款比例
        public decimal? Prepay { get; set; }//预付款金额
        public bool? MeasureFlag { get; set; }//是否需要上门测量
        public string DeliveryAddress { get; set; }//送货地址
        public string DeliveryLinkTel { get; set; }//送货地址
        public string DeliveryLinkMan { get; set; }//送货地址
        public int? SaleUserId { get; set; }//销售人员ID
        public string SaleUserName { get; set; }
        public int? SaleDepartmentId { get; set; }//部门ID
        public string SaleDepartment { get; set; }//部门
        public DateTime? CreateTime { get; set; }
        public DateTime? CheckedTime { get; set; }
        public int? Status { get; set; }
        public string Remark { get; set; }
        public int? FRFlag { get; set; }
        public string CheckedUserName { get; set; }
        public string HTDate { get; set; }
        public DateTime? OrderTime { get; set; }
        public List<SelectListItem> CustomerDroList { get; set; }
        public string OrderMun { get; set; }
        public List<DeliveryModel> DePro { get; set; }//送货Model
        public int? CWCheckStatus { get; set; }
        public int? SHFlag { get; set; }
        public string ZTDFlag { get; set; }
        public decimal? RealPrice { get; set; }//实收款金额
        public int? FRStatus { get; set; }
        public List<ContractProductsModel> HTProList { get; set; }
        public int? SaleFlag { get; set; }
        public string DDOrder { get; set; }
        public string YDOrder { get; set; }
        public int? HTProCount { get; set; }
        public int? HTFProCount { get; set; }
    }
    public class SContractHeaderModel
    {
        public int? FR_flag { get; set; }
        public string SN { get; set; }
        public int CheckState { get; set; }
        public string UserName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int status { get; set; }
        public int FRstatus { get; set; }
        public int? DepartmentId { get; set; }
        public int? SaleUserId { get; set; }
        public List<SelectListItem> DepartmentDroList { get; set; }
    }
    public class ContractProductsModel
    {
        public int Id { get; set; }
        public int? ContractHeadId { get; set; }
        public string FatherSN { get; set; }
        public string ProductXLSN { get; set; }
        public int? ProductId { get; set; }
        public int ProductSNId { get; set; }
        public string ProductSN { get; set; }
        public string ProductXL { get; set; }
        public string ProductName { get; set; }
        public string ProductArea { get; set; }
        public int? ColorId { get; set; }
        public string Color { get; set; }
        public int? WoodId { get; set; }
        public string WoodName { get; set; }
        public string WoodSN { get; set; }
        public bool? CustomFlag { get; set; }//是否定制
        public decimal? length { get; set; }//送货通道：楼梯/电梯
        public decimal? width { get; set; }//预付款比例
        public decimal? height { get; set; }//预付款金额
        public decimal? price { get; set; }//预付款金额
        public int? Qty { get; set; }
        public string hardware_part { get; set; }
        public string decoration_part { get; set; }
        public string req_carve { get; set; }
        public string req_others { get; set; }
        public DateTime? created_time { get; set; }
        public string SN { get; set; }
        public string Customer { get; set; }
        public DateTime? delivery_date { get; set; }
        public DateTime? checked_date { get; set; }
        public string Remark { get; set; }
        public int? SemiCount { get; set; }
        public int? LabelsCount { get; set; }
        public decimal amount { get; set; }//合同总金额
        public decimal? prepay { get; set; }//预付款金额
        public decimal FR_contract { get; set; }
        public List<SelectListItem> ProXLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public List<SelectListItem> ProNameDroList { get; set; }
        public List<SelectListItem> WoodDroList { get; set; }
        public List<SelectListItem> colorDroList { get; set; }
        public List<SelectListItem> FatherDroList { get; set; }
        public int? WorkCount { get; set; }
        public int? Status { get; set; }
        public string OrderNum { get; set; }
        public int? FatherId { get; set; }
        public int IsJJ { get; set; }
        public string DDOrder { get; set; }
        public string YDOrder { get; set; }
    }
    public class FContractProductsModel
    {
        public int Id { get; set; }
        public int? FContractHeadId { get; set; }
        public string HeaderName { get; set; }
        public int? FProductId { get; set; }
        public int? ProductAreaId { get; set; }
        public int ProductXLId { get; set; }
        public string FProductXL { get; set; }
        public string FProductName { get; set; }
        public string ProductArea { get; set; }
        public int FColorId { get; set; }
        public string FColor { get; set; }
        public int? FWoodId { get; set; }
        public string FWoodName { get; set; }
        public bool? CustomFlag { get; set; }//是否定制
        public decimal? Length { get; set; }//送货通道：楼梯/电梯
        public decimal? Width { get; set; }//预付款比例
        public decimal? Height { get; set; }//预付款金额
        public decimal? Price { get; set; }//预付款金额
        public int? FQty { get; set; }
        public string hardware_part { get; set; }
        public string decoration_part { get; set; }
        public string req_carve { get; set; }
        public string Others { get; set; }//其它要求
        public DateTime? CreateTime { get; set; }
        public string SN { get; set; }
        public string Customer { get; set; }
        public DateTime? delivery_date { get; set; }
        public DateTime? checked_date { get; set; }
        public string Remark { get; set; }
        public int? SemiCount { get; set; }
        public int? LabelsCount { get; set; }
        public decimal amount { get; set; }//合同总金额
        public decimal? prepay { get; set; }//预付款金额
        public decimal FR_contract { get; set; }
        public List<SelectListItem> ProXLDroList { get; set; }
        public List<SelectListItem> AreaDroList { get; set; }
        public List<SelectListItem> ProNameDroList { get; set; }
        public List<SelectListItem> WoodDroList { get; set; }
        public List<SelectListItem> ColorDroList { get; set; }
        public int? WorkCount { get; set; }
        public int? Status { get; set; }
    }
    public class SContractProductsModel
    {
        public string SN { get; set; }
        public string Customer { get; set; }
        public string ProductName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? SaleFlag { get; set; }
        public bool? IsKG { get; set; }
    }
    public class FRContract
    {
        public decimal Totailamount { get; set; }
    }
    public class DeliveryModel
    {
        public int id { get; set; }
        public string productXL { get; set; }
        public string productName { get; set; }
        public string woodName { get; set; }
        public decimal? length { get; set; }//送货通道：楼梯/电梯
        public decimal? width { get; set; }//预付款比例
        public decimal? height { get; set; }//预付款金额
        public int? qty { get; set; }
        public int HTHeadId { get; set; }
        public int HTDetailId { get; set; }
    }
}
