using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class SaleStartService
    {
        private static readonly SaleStartDal SSDal = new SaleStartDal();
        public List<ContractProductsModel> GetPageList(SContractProductsModel SModel)
        {
            try { return SSDal.GetPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ContractProductsModel> GetBindOrderPageList(SContractProductsModel SModel)
        {
            try { return SSDal.GetBindOrderPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
