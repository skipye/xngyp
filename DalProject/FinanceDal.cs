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
                if (table != null)
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
                        table.FirstOrDefault().amount = Models.Amount + table.FirstOrDefault().amount;
                        table.FirstOrDefault().operator_id = Models.operator_id;
                        table.FirstOrDefault().operator_name = Models.operator_name;
                        Pay = table.FirstOrDefault().amount ?? 0;
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
                if (Models.Amount > 0 && Pay >= Prepay && Pay < Total)//更新合同的付款状态
                {
                    OrderTable.FR_flag = 1;
                }
                if (Pay >= Total)
                { OrderTable.FR_flag = 2; }
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
    }
}
