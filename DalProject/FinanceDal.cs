﻿using Common;
using DataBase;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DalProject
{
    public class FinanceDal
    {
        
        public void AddOrUpdate(FinanceModel Models)
        {
            using (var db = new XNGYPEntities())
            {
                var OrderTable = db.Contract_Header.Where(k => k.Id == Models.Id).FirstOrDefault();
                var Total = OrderTable.Amount;
                var Prepay = OrderTable.Prepay;
                var Pay = Models.Amount;
                //添加付款操作日志
                XNGYP_FR_Logs SFLtable = new XNGYP_FR_Logs();
                SFLtable.HTId = Models.Id;
                SFLtable.HTSN = OrderTable.SN;
                SFLtable.Customer = OrderTable.XNGYP_Customers.Name;
                SFLtable.PayModel = Models.PayModel;
                SFLtable.Amount = Models.Amount;
                SFLtable.operator_id = new UserDal().GetCurrentUserName().UserId;
                SFLtable.operator_name = new UserDal().GetCurrentUserName().UserName;
                SFLtable.CreateTime = Models.CreateTime ?? DateTime.Now;
                SFLtable.Remaks = Models.Remaks;
                SFLtable.PayStatus = Models.PayStatus;
                db.XNGYP_FR_Logs.Add(SFLtable);

                var table = db.XNGYP_FR.Where(k => k.HTId == Models.Id).SingleOrDefault();//判断是否存在
                if (table != null && table.Id > 0)
                {
                    table.Amount = Models.Amount + table.Amount;
                    table.operator_id = Models.operator_id;
                    table.operator_name = Models.operator_name;

                    Pay = table.Amount;
                }
                else
                {
                    XNGYP_FR Stable = new XNGYP_FR();
                    Stable.HTId = Models.Id;
                    Stable.HTSN = OrderTable.SN;
                    Stable.Customer = OrderTable.XNGYP_Customers.Name;
                    Stable.Total = Total;
                    Stable.Prepay = Prepay;
                    Stable.Amount = Models.Amount;
                    Stable.operator_id = SFLtable.operator_id;
                    Stable.operator_name = SFLtable.operator_name;
                    Stable.CreateTime = DateTime.Now;
                    db.XNGYP_FR.Add(Stable);
                }
                if (Models.Amount > 0 && Pay >= Prepay && Pay < Total)//更新合同的付款状态
                {
                    OrderTable.FRFlag = 1;
                    
                }
                if (Pay >= Total)
                { OrderTable.FRFlag = 2; }


                db.SaveChanges();
            }
        }
        //收款记录
        public List<FinanceModel> GetFKShowList(int Id)
        {
            using (var db = new XNGYPEntities())
            {
                var List = (from p in db.XNGYP_FR_Logs.Where(k => k.HTId == Id)
                            orderby p.CreateTime descending
                            select new FinanceModel
                            {
                                Id = p.Id,
                                HTSN = p.HTSN,
                                Customer = p.Customer,
                                PayModel = p.PayModel,
                                operator_name = p.operator_name,
                                Amount = p.Amount,
                                Remaks = p.Remaks,
                                CreateTime=p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        public List<FinanceModel> GetSKList(SContractHeaderModel SModel)
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
                var List = (from p in db.XNGYP_FR_Logs
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new FinanceModel
                            {
                                Id = p.Id,
                                HTSN = p.HTSN,
                                Customer = p.Customer,
                                PayModel = p.PayModel,
                                operator_name = p.operator_name,
                                Amount = p.Amount,
                                Remaks = p.Remaks,
                                CreateTime = p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        public PriceModel GetSKCount()
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
                var MTabel = db.XNGYP_FR_Logs.Where(k => k.CreateTime > MonthStartTime && k.CreateTime < MonthEndTime).Select(k => k.Amount);
                var WTabel = db.XNGYP_FR_Logs.Where(k => k.CreateTime > WeekStartTime && k.CreateTime < WeekEndTime).Select(k => k.Amount);
                var DTabel = db.XNGYP_FR_Logs.Where(k => k.CreateTime > StartTime).Select(k => k.Amount);
                Models.TotalCount = db.XNGYP_FR_Logs.Sum(k => k.Amount);
                Models.YearCount = db.XNGYP_FR_Logs.Where(k => k.CreateTime > YearStartTime && k.CreateTime < YearEndTime).Sum(k => k.Amount);
                Models.MonthCount = MTabel != null && MTabel.Any() ? MTabel.Sum() : 0;
                Models.WeekCount = WTabel != null && WTabel.Any() ? WTabel.Sum() : 0;
                Models.TodayCount = DTabel != null && DTabel.Any() ? DTabel.Sum() : 0;
            }
            return Models;
        }
        public void AddOrUpdateF(FinanceModel Models)
        {
            decimal Total = 0;
            decimal Prepay = 0;
            decimal Pay = 0;
            string OrderSN = "";
            string Customer = "";
            DateTime HtDate = DateTime.Now;
            using (var db = new XNERPEntities())
            {
                var OrderTable = db.CRM_contract_header.Where(k => k.id == Models.Id).FirstOrDefault();
                Total = OrderTable.amount;
                Prepay = OrderTable.prepay??0;
                Pay = Models.Amount??0;
                OrderSN = OrderTable.SN;
                Customer = OrderTable.CRM_customers.name;
                HtDate = OrderTable.created_time;
            }
            using (var db = new XNFinanceEntities())
            {
                //添加付款操作日志
                FR_contract_logs SFLtable = new FR_contract_logs();
                SFLtable.HTId = Models.Id;
                SFLtable.HTSN = OrderSN;
                SFLtable.Customer = Customer;
                SFLtable.PayModel = Models.PayModel;
                SFLtable.Amount = Models.Amount;
                SFLtable.operator_id = new UserDal().GetCurrentUserName().UserId;
                SFLtable.operator_name = new UserDal().GetCurrentUserName().UserName;
                SFLtable.CreateTime = Models.CreateTime ?? DateTime.Now;
                SFLtable.Remaks = Models.Remaks;
                SFLtable.PayStatus = 1;
                db.FR_contract_logs.Add(SFLtable);

                var table = db.FR_contract.Where(k => k.contract_id == Models.Id).OrderBy(k=>k.created_time);//判断是否存在
                if (table != null && table.Count()>0)
                {
                    if (table.Count() > 1)
                    {
                        FR_contract Stable = new FR_contract();
                        Stable.contract_id = Models.Id;
                        Stable.SN = OrderSN;
                        Stable.customer = Customer;
                        Stable.total = Total;
                        Stable.amount = Models.Amount;
                        Stable.operator_id = SFLtable.operator_id;
                        Stable.operator_name = SFLtable.operator_name;
                        Stable.created_time = Models.CreateTime ?? DateTime.Now;
                        Stable.receive_date = HtDate;
                        db.FR_contract.Add(Stable);
                    }
                    else {
                        var NewTable = db.FR_contract.Where(k => k.contract_id == Models.Id).FirstOrDefault();
                        NewTable.amount = Models.Amount + NewTable.amount;
                        NewTable.operator_id = Models.operator_id;
                        NewTable.operator_name = Models.operator_name;
                        Pay = NewTable.amount ?? 0;
                    }
                }
                else
                {
                    FR_contract Stable = new FR_contract();
                    Stable.contract_id = Models.Id;
                    Stable.SN = OrderSN;
                    Stable.customer = Customer;
                    Stable.total = Total;
                    Stable.amount = Models.Amount;
                    Stable.operator_id = SFLtable.operator_id;
                    Stable.operator_name = SFLtable.operator_name;
                    Stable.created_time = Models.CreateTime??DateTime.Now;
                    Stable.receive_date = HtDate;
                    db.FR_contract.Add(Stable);
                }
                db.SaveChanges();
            }
            using (var db = new XNERPEntities())
            {
                var OrderTable = db.CRM_contract_header.Where(k => k.id == Models.Id).FirstOrDefault();
                if (Models.Amount >= 0 && Pay >= Prepay && Pay < Total)//更新合同的付款状态
                {
                    OrderTable.FR_flag = 1;
                    OrderTable.CWStatus = 1;
                }
                if (Pay >= Total)
                { OrderTable.FR_flag = 2; OrderTable.CWStatus = 1; }
                db.SaveChanges();
            }
        }
        //收款记录
        public List<FinanceModel> GetFFKShowList(int Id)
        {
            using (var db = new XNFinanceEntities())
            {
                var List = (from p in db.FR_contract_logs.Where(k => k.HTId == Id)
                            orderby p.CreateTime descending
                            select new FinanceModel
                            {
                                Id = p.Id,
                                HTSN = p.HTSN,
                                Customer = p.Customer,
                                PayModel = p.PayModel,
                                operator_name = p.operator_name,
                                Amount = p.Amount,
                                Remaks = p.Remaks,
                                CreateTime = p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        public List<FinanceModel> GetFSKList(SContractHeaderModel SModel)
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
            using (var db = new XNFinanceEntities())
            {
                var List = (from p in db.FR_contract_logs
                            where p.CreateTime > StartTime
                            where p.CreateTime < EndTime
                            orderby p.CreateTime descending
                            select new FinanceModel
                            {
                                Id = p.Id,
                                HTSN = p.HTSN,
                                Customer = p.Customer,
                                PayModel = p.PayModel,
                                operator_name = p.operator_name,
                                Amount = p.Amount,
                                Remaks = p.Remaks,
                                CreateTime = p.CreateTime,
                            }).ToList();
                return List;
            }
        }
        public PriceModel GetFSKCount()
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
            using (var db = new XNFinanceEntities())
            {
                var MTabel = db.FR_contract_logs.Where(k => k.CreateTime > MonthStartTime && k.CreateTime < MonthEndTime).Select(k => k.Amount);
                var WTabel = db.FR_contract_logs.Where(k => k.CreateTime > WeekStartTime && k.CreateTime < WeekEndTime).Select(k => k.Amount);
                var DTabel = db.FR_contract_logs.Where(k => k.CreateTime > StartTime).Select(k => k.Amount);
                Models.TotalCount = db.FR_contract_logs.Sum(k => k.Amount);
                Models.YearCount = db.FR_contract_logs.Where(k => k.CreateTime > YearStartTime && k.CreateTime < YearEndTime).Sum(k => k.Amount);
                Models.MonthCount = MTabel != null && MTabel.Any() ? MTabel.Sum() : 0;
                Models.WeekCount = WTabel != null && WTabel.Any() ? WTabel.Sum() : 0;
                Models.TodayCount = DTabel != null && DTabel.Any() ? DTabel.Sum() : 0;
            }
            return Models;
        }
        //家具订单审核
        public void CWFOrderCheck(string ListId, int CheckedId)
        {
            using (var db = new XNERPEntities())
            {
                string[] ArrId = ListId.Split('$');
                foreach (var item in ArrId)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        int Id = Convert.ToInt32(item);
                        var tables = db.CRM_contract_header.Where(k => k.id == Id).SingleOrDefault();
                        tables.CWStatus = CheckedId;
                        if (CheckedId == 2)
                        {
                            tables.status = 0;
                        }
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
