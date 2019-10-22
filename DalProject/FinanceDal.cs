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
                SFLtable.operator_id = Models.operator_id;
                SFLtable.operator_name = Models.operator_name;
                SFLtable.CreateTime = DateTime.Now;
                SFLtable.Remaks = Models.Remaks;
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
                    Stable.operator_id = Models.operator_id;
                    Stable.operator_name = Models.operator_name;
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
                                CreateTime=p.CreateTime
                            }).ToList();
                return List;
            }
        }
    }
}
