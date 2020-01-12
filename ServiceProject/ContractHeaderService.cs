using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DalProject;
using System.Web.Mvc;
using System.Data;

namespace ServiceProject
{
    public class ContractHeaderService
    {
        private static readonly ContractHeaderDal CHDal = new ContractHeaderDal();
        public ContractModel GetPageList(SContractHeaderModel SModel)
        {
            try { return CHDal.GetPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AdminModel GetHTCount()
        {
            try { return CHDal.GetHTCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AdminModel GetFHTCount()
        {
            try { return CHDal.GetFHTCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PriceModel GetXSCount()
        {
            try { return CHDal.GetXSCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PriceModel GetFXSCount()
        {
            try { return CHDal.GetFXSCount(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ContractModel GetFPageList(SContractHeaderModel SModel)
        {
            try { return CHDal.GetFPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(ContractHeaderModel Models)
        {
            try { CHDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ContractHeaderModel GetDetailById(int Id)
        {
            try { return CHDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ContractHeaderModel GetFDetailById(int Id)
        {
            try { return CHDal.GetFDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Delete(string ListId)
        {
            try { CHDal.Delete(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Checked(string ListId, int CheckedId)
        {
            try { CHDal.Checked(ListId, CheckedId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CWChecked(string ListId, int CheckedId)
        {
            try { CHDal.CWChecked(ListId, CheckedId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int GetCRMHTCount(DateTime CreateTime)
        {
            try { return CHDal.GetCRMHTCount(CreateTime); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetColorDrolist(int? pId)
        {
            try { return CHDal.GetColorDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetWoodDrolist(int? pId)
        {
            try { return CHDal.GetWoodDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetProSNDrolist(int? pId)
        {
            try { return CHDal.GetProSNDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetFatherProSNDrolist(int? pId)
        {
            try { return CHDal.GetFatherProSNDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetProFSNDrolist(int? pId)
        {
            try { return CHDal.GetProFSNDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetProAreaDrolist(int? pId)
        {
            try { return CHDal.GetProAreaDrolist(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetCKDrolist(int? pId, int? CKType)
        {
            try { return CHDal.GetCKDrolist(pId, CKType); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetFCKDrolist(int? pId, int? CKType)
        {
            try { return CHDal.GetFCKDrolist(pId, CKType); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetFProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            try { return CHDal.GetFProNameDrolistBySNAndArea(ProSN, ProProArea); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetProNameDrolistBySNAndArea(int? ProSN, int? ProProArea)
        {
            try { return CHDal.GetProNameDrolistBySNAndArea(ProSN, ProProArea); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddProducts(ContractProductsModel Models)
        {
            try { CHDal.AddProducts(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddFProducts(FContractProductsModel Models)
        {
            try { CHDal.AddFProducts(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteOne(int Id)
        {
            try { CHDal.DeleteOne(Id); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFProduct(int Id)
        {
            try { CHDal.DeleteFProduct(Id); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ContractProductsModel> GetProductListByOrder(int HTId)
        {
            try { return CHDal.GetProductListByOrder(HTId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ContractProductsModel> GetFProductListByOrder(int HTId)
        {
            try { return CHDal.GetFProductListByOrder(HTId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToFExcelOut(SContractHeaderModel SModel)
        {
            try
            {
                return CHDal.ToFExcelOut(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcelOut(SContractHeaderModel SModel)
        {
            try
            {
                return CHDal.ToExcelOut(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
