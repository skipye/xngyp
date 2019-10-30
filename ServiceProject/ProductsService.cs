using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class ProductsService
    {
        private static readonly ProductsDal CDal = new ProductsDal();

        public List<ProductsNameModel> GetPageList()
        {
            try { return CDal.GetPageList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(ProductsNameModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ProductsNameModel GetDetailById(int Id)
        {
            try { return CDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteMore(string ListId)
        {
            try { CDal.DeleteMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<ProductsSNModel> GetSNPageList()
        {
            try { return CDal.GetSNPageList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public bool AddOrUpdateSN(ProductsSNModel Models)
        {
            try { CDal.AddOrUpdateSN(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ProductsSNModel GetSNDetailById(int Id)
        {
            try { return CDal.GetSNDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteSNMore(string ListId)
        {
            try { CDal.DeleteMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetProNameDrolistBySN(int? ProSN)
        {
            try
            {
                return CDal.GetProNameDrolistBySN(ProSN);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetSecSNDrolistByFatherId(int? FatherId, string SelectedId)
        {
            try
            {
                return CDal.GetSecSNDrolistByFatherId(FatherId, SelectedId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
