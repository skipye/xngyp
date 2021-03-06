﻿using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBase;
using System.Web.Mvc;
using System.Data;
using Common;

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
                            where SModel.DepartmentId>0 ? p.SaleDepartmentId == 357 || p.SaleDepartmentId == 359 || p.SaleDepartmentId == 348: true
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
                                Remark=p.Remark,
                                DDOrder=p.DDOrder,
                                HTProCount=p.Contract_Detail.Where(k=>k.DeleteFlag==false).Sum(k=>k.Qty),
                                HTFProCount=p.Contract_FDetail.Where(k => k.DeleteFlag == false).Sum(k => k.Qty),
                            }).ToList();
                ContractModel Models = new ContractModel();
                Models.data = List;
                Models.HTTotail = List.Sum(k => k.Amount);
                return Models;
            }
        }
        public AdminModel GetHTCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            AdminModel Models = new AdminModel();
            using (var db = new XNGYPEntities())
            {
                Models.TotalCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1).Count();
                Models.YearCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > YearStartTime && k.CreateTime < YearEndTime).Count();
                Models.MonthCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > MonthStartTime && k.CreateTime < MonthEndTime).Count();
                Models.WeekCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > WeekStartTime && k.CreateTime < WeekEndTime).Count();
                Models.TodayCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > StartTime).Count();
            }
            return Models;
        }
        public PriceModel GetXSCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            PriceModel Models = new PriceModel();
            using (var db = new XNGYPEntities())
            {
                Models.TotalCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1).Sum(k => k.Amount)??0;
                Models.YearCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > YearStartTime && k.CreateTime < YearEndTime).Sum(k => k.Amount) ?? 0;
                Models.MonthCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > MonthStartTime && k.CreateTime < MonthEndTime).Sum(k => k.Amount) ?? 0;
                Models.WeekCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > WeekStartTime && k.CreateTime < WeekEndTime).Sum(k => k.Amount) ?? 0;
                Models.TodayCount = db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status == 1 && k.CreateTime > StartTime).Sum(k => k.Amount) ?? 0;
            }
            return Models;
        }
        public DataTable ToExcelOut(SContractHeaderModel SModel)
        {
            DataTable Exceltable = new DataTable();
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
                var List = (from p in db.Contract_Header.Where(k => k.DeleteFlag == false && k.Status==1)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where SModel.CheckState == 1 ? p.Status == SModel.CheckState : true
                            where SModel.DepartmentId > 0 ? p.SaleDepartmentId == SModel.DepartmentId : true
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
                                CheckedTime = p.CheckedTime,
                                CreateTime = p.CreateTime,
                                CWCheckStatus = p.CWCheckStatus,
                                SHFlag = p.SHFlag,
                                DeliverChannel = p.DeliverChannel,
                                ZTDFlag = p.ZTDFlag,
                                SaleFlag = p.SaleFlag,
                                Remark = p.Remark,
                                DDOrder = p.DDOrder,
                            }).ToList();
                if (List != null && List.Any())
                {
                    Exceltable.Columns.Add("客户名称", typeof(string));
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("合同时间", typeof(string));
                    Exceltable.Columns.Add("发货方式", typeof(string));
                    Exceltable.Columns.Add("合同总金额", typeof(string));
                    Exceltable.Columns.Add("应收款", typeof(string));
                    Exceltable.Columns.Add("付款状态", typeof(string));
                    Exceltable.Columns.Add("是否审核", typeof(string));
                    Exceltable.Columns.Add("销售人员", typeof(string));
                    Exceltable.Columns.Add("财务审核", typeof(string));
                    Exceltable.Columns.Add("销售方式", typeof(string));
                    Exceltable.Columns.Add("操作时间", typeof(string));
                    foreach (var item in List)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["客户名称"] = item.Customer;
                        row["合同编号"] = item.SN;
                        row["合同时间"] = Convert.ToDateTime(item.OrderTime).ToString("yyyy-MM-dd");
                        row["发货方式"] = item.SHFlag==1? "自提" : item.SHFlag == 2 ? "邮寄" : "送货";
                        row["合同总金额"] = item.Amount;
                        row["应收款"] = item.Prepay;
                        row["付款状态"] = item.FRFlag == 1 ? "已收预付款" : item.FRFlag == 2 ? "已收全款" : "未付款";
                        row["是否审核"] = item.Status==1? "是" : item.Status == 2? "被驳回" : "否";
                        row["销售人员"] = item.SaleUserName;
                        row["财务审核"] = item.CWCheckStatus == 1 ? "是" : item.CWCheckStatus == 2 ? "被驳回" : "否";
                        row["销售方式"] = item.SaleFlag == 1 ? "来源线上" : "来源线下";
                        row["操作时间"] = Convert.ToDateTime(item.CreateTime).ToString("yyyy-MM-dd");

                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }
        public ContractModel GetFPageList(SContractHeaderModel SModel)
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
            ContractModel Models = new ContractModel();
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_header.Where(k => k.delete_flag == false && k.status==1)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new ContractHeaderModel
                            {
                                Id = p.id,
                                SN = p.SN,
                                Customer = p.CRM_customers.name,
                                Amount = p.amount,
                                Status = p.status,
                                Prepay = p.prepay,
                                CheckedUserName = p.check_user_name,
                                FRFlag = p.FR_flag,
                                OrderTime = p.HTDate,
                                CreateTime = p.created_time,
                                CWCheckStatus = p.CWStatus,
                                Remark = p.Remark,
                            }).ToList();
                Models.data = List;
                Models.HTTotail = List.Sum(k => k.Amount);
            }
            using (var db = new XNFinanceEntities())
            {
                if (Models.data != null && Models.data.Any())
                {
                    foreach (var item in Models.data)
                    {
                        item.RealPrice = db.FR_contract.Where(k => k.contract_id == item.Id).Sum(k => k.amount)??0;
                    }
                }
            }
            return Models;
        }
        public AdminModel GetFHTCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            AdminModel Models = new AdminModel();
            using (var db = new XNERPEntities())
            {
                Models.TotalCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1).Count();
                Models.YearCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > YearStartTime && k.created_time < YearEndTime).Count();
                Models.MonthCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > MonthStartTime && k.created_time < MonthEndTime).Count();
                Models.WeekCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > WeekStartTime && k.created_time < WeekEndTime).Count();
                Models.TodayCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > StartTime).Count();
            }
            return Models;
        }
        public PriceModel GetFXSCount()
        {
            DateTime TimeNow = DateTime.Now;
            DateTime YearStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime YearEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Year, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime MonthEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Month, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekStartTime = Convert.ToDateTime(DataTimeManager.GetTimeStartByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime WeekEndTime = Convert.ToDateTime(DataTimeManager.GetTimeEndByType(DataTimeType.Week, TimeNow).ToString("yyyy-MM-dd"));
            DateTime StartTime = Convert.ToDateTime(TimeNow.ToString("yyyy-MM-dd"));
            PriceModel Models = new PriceModel();
            using (var db = new XNERPEntities())
            {
                var MTabel = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > MonthStartTime && k.created_time < MonthEndTime).Select(k => k.amount);
                var WTabel = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > WeekStartTime && k.created_time < WeekEndTime).Select(k => k.amount);
                var DTabel = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > StartTime).Select(k => k.amount);
                Models.TotalCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1).Sum(k=>k.amount);
                Models.YearCount = db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1 && k.created_time > YearStartTime && k.created_time < YearEndTime).Sum(k => k.amount);
                Models.MonthCount = MTabel!=null && MTabel.Any()? MTabel.Sum():0;
                Models.WeekCount = WTabel != null && WTabel.Any() ? WTabel.Sum() : 0;
                Models.TodayCount = DTabel != null && DTabel.Any() ? DTabel.Sum() : 0;
            }
            return Models;
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
                    table.DDOrder = Models.DDOrder;
                    table.YDOrder = Models.YDOrder;
                    table.Remark = Models.Remark;
                    table.FreightCarrier = Models.FreightCarrier;
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
                    table.CWCheckStatus = 0;
                    table.DeliverChannel = Models.DeliverChannel;
                    table.SHFlag = Models.SHFlag;
                    table.ZTDFlag = Models.ZTDFlag;
                    table.RealPrice = Models.RealPrice;
                    table.FRStatus = Models.FRStatus;
                    table.SaleFlag = Models.SaleFlag;
                    table.DDOrder = Models.DDOrder;
                    table.YDOrder = Models.YDOrder;
                    table.Remark = Models.Remark;
                    table.FreightCarrier = Models.FreightCarrier;
                    db.Contract_Header.Add(table);

                    //如有新的订单发生通知
                    OrderModel OModels = new OrderModel();
                    OModels.OrderSN = table.SN;
                    OModels.SaleName = table.SaleUserName;
                    OModels.SendTel = "18321909838";
                    SendSMSDal.Send(OModels,1);//销售经理
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
                                  DeliveryLinkMan = p.DeliveryLinkMan,
                                  DeliveryLinkTel = p.DeliveryLinkTel,
                                  SHFlag = p.SHFlag,
                                  DeliverChannel = p.DeliverChannel,
                                  ZTDFlag = p.ZTDFlag,
                                  RealPrice = p.RealPrice,
                                  FRStatus = p.FRStatus,
                                  SaleFlag = p.SaleFlag,
                                  YDOrder = p.YDOrder,
                                  DDOrder=p.DDOrder,
                                  CreateTime=p.CreateTime,
                                  FreightCarrier = p.FreightCarrier,
                                  Remark = p.Remark,
                       }).SingleOrDefault();
                return tables;
            }
        }
        public ContractHeaderModel GetFDetailById(int Id)
        {
            using (var db = new XNERPEntities())
            {
                var tables = (from p in db.CRM_contract_header.Where(k => k.delete_flag == false && k.id == Id)

                              orderby p.created_time descending
                              select new ContractHeaderModel
                              {
                                  Id = p.id,
                                  SN = p.SN,
                                  Customer = p.CRM_customers.name,
                                  DeliveryDate = p.delivery_date,
                                  Amount = p.amount,
                                  DeliveryAddress = p.delivery_address,
                                  OrderTime = p.HTDate,
                                  DeliverChannel = p.delivery_channel,
                                  CreateTime = p.created_time,
                                  FreightCarrier = p.freight_carrier,
                                  Remark = p.Remark,
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
        public void Checked(string ListId, int CheckedId)
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
                        tables.Status = CheckedId;
                        tables.CheckedUserId = new UserDal().GetCurrentUserName().UserId;
                        tables.CheckedUserName = new UserDal().GetCurrentUserName().UserName;
                        tables.CheckedTime = DateTime.Now;

                        if (CheckedId == 1)
                        {
                            OrderModel OModels = new OrderModel();
                            OModels.OrderSN = tables.SN;
                            if (tables.SaleFlag == 1) {
                                OModels.Custmor = tables.XNGYP_Customers.Name;
                                OModels.SendTel = "13636445362";
                                SendSMSDal.Send(OModels, 4);//仓库
                            }
                            else { 
                                //如有新的订单发生通知
                                
                                OModels.SendTel = "18621809046";
                                SendSMSDal.Send(OModels, 2);//财务
                            }
                        }
                        
                    }
                }
                db.SaveChanges();
            }
        }
        public void CWChecked(string ListId,int CheckedId)
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
                        tables.CWCheckStatus = CheckedId;
                        tables.CWCheckId = new UserDal().GetCurrentUserName().UserId;
                        tables.CWCheckName = new UserDal().GetCurrentUserName().UserName;
                        tables.CWCheckTime = DateTime.Now;
                        if (CheckedId == 1)
                        {
                            OrderModel OModels = new OrderModel();
                            OModels.OrderSN = tables.SN;

                            if (tables.SaleFlag == 1)
                            {
                                OModels.Custmor = tables.XNGYP_Customers.Name;
                                OModels.SendTel = "13636445362";
                                SendSMSDal.Send(OModels, 4);//仓库
                            }
                            else { OModels.SendTel = "13524680161"; SendSMSDal.Send(OModels, 3); }//厂长
                            AddFOrder(Id);//添加家具订单
                        }
                        if (CheckedId == 2)
                        {
                            tables.Status = 0;
                        }
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
            string SN = Models.SN;
            if (!string.IsNullOrEmpty(Models.OrderNum))
            {
                SN = Models.OrderNum.ToUpper();
            }
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
                table.SN = SN;
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
                                SN=p.SN,
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
        public List<ContractProductsModel> GetFProductListByOrder(int HTId)
        {
            using (var db = new XNERPEntities())
            {
                var List = (from p in db.CRM_contract_detail.Where(k => k.delete_flag == false && k.header_id == HTId)
                            orderby p.created_time descending
                            select new ContractProductsModel
                            {
                                Id = p.id,
                                ProductName = p.SYS_product.name,
                                Color = p.color,
                                WoodName = p.INV_wood_type.name,
                                length = p.length,
                                width = p.width,
                                height = p.height,
                                price = p.price,
                                hardware_part = p.hardware_part,
                                decoration_part = p.decoration_part,
                                req_others = p.req_others,
                                Qty = p.qty,
                            }).ToList();
                
                return List;
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
                                      FreightCarrier = p.FreightCarrier,
                                      Remark = p.Remark
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
                    table.Remark = CHModels.Remark;
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

                    OrderModel OModels = new OrderModel();
                    OModels.OrderSN = table.SN;
                    OModels.SaleName = "唐锐";
                    OModels.SendTel = "18017153881";
                    SendSMSDal.Send(OModels, 1);
                    

                }
            }
        }
        //导出
        public DataTable ToFExcelOut(SContractHeaderModel SModel)
        {
            DataTable Exceltable = new DataTable();
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
            ContractModel Models = new ContractModel();
            using (var db = new XNERPEntities())
            {
                var list = (from p in db.CRM_contract_header.Where(k => k.delete_flag == false && k.status == 1)
                            where !string.IsNullOrEmpty(SModel.SN) ? p.SN.Contains(SModel.SN) : true
                            where p.created_time > StartTime
                            where p.created_time < EndTime
                            orderby p.created_time descending
                            select new ContractHeaderModel
                            {
                                Id = p.id,
                                SN = p.SN,
                                Customer = p.CRM_customers.name,
                                Amount = p.amount,
                                Status = p.status,
                                Prepay = p.prepay,
                                CheckedUserName = p.check_user_name,
                                FRFlag = p.FR_flag,
                                OrderTime = p.HTDate,
                                CreateTime = p.created_time,
                                CWCheckStatus = p.CWStatus,
                                Remark = p.Remark,
                            }).ToList();
                
                if (list != null && list.Any())
                {
                    Exceltable.Columns.Add("客户名称", typeof(string));
                    Exceltable.Columns.Add("合同编号", typeof(string));
                    Exceltable.Columns.Add("合同时间", typeof(string));
                    Exceltable.Columns.Add("合同总金额", typeof(string));
                    Exceltable.Columns.Add("预付款", typeof(string));
                    Exceltable.Columns.Add("已收款", typeof(string));
                    Exceltable.Columns.Add("尾款", typeof(string));
                    Exceltable.Columns.Add("付款状态", typeof(string));
                    Exceltable.Columns.Add("审核人员", typeof(string));
                    Exceltable.Columns.Add("操作时间", typeof(string));
                    Exceltable.Columns.Add("备注", typeof(string));
                    
                    foreach (var item in list)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["客户名称"] = item.Customer;
                        row["合同编号"] = item.SN;
                        row["合同时间"] = item.OrderTime;
                        row["合同总金额"] = item.Amount;
                        row["预付款"] = item.Prepay;
                        row["已收款"] = item.RealPrice;
                        row["尾款"] = item.Amount - item.RealPrice;
                        row["付款状态"] = item.FRFlag==1? "已付预付款": item.FRFlag == 2? "已付全款" : "未付款";
                        row["审核人员"] = item.CheckedUserName;
                        row["操作时间"] = item.CreateTime;
                        row["备注"] = item.Remark;
                        
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }

    }
}
