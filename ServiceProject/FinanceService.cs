using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;

namespace ServiceProject
{
    public class FinanceService
    {
        private static readonly FinanceDal FDal = new FinanceDal();
        public bool AddOrUpdate(FinanceModel Models)
        {
            try { FDal.AddOrUpdate(Models);return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //收款记录
        public List<FinanceModel> GetFKShowList(int Id)
        {
            try { return FDal.GetFKShowList(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
