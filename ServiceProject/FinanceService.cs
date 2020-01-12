using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;

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
        public List<FinanceModel> GetFSKList(SContractHeaderModel SModel)
        {
            try { return FDal.GetFSKList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<FinanceModel> GetSKList(SContractHeaderModel SModel)
        {
            try { return FDal.GetSKList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PriceModel GetFSKCount()
        {
            try { return FDal.GetFSKCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PriceModel GetSKCount()
        {
            try { return FDal.GetSKCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdateF(FinanceModel Models)
        {
            try { FDal.AddOrUpdateF(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //收款记录
        public List<FinanceModel> GetFFKShowList(int Id)
        {
            try { return FDal.GetFFKShowList(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CWFOrderCheck(string ListId, int CheckedId)
        {
            try { FDal.CWFOrderCheck(ListId, CheckedId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
