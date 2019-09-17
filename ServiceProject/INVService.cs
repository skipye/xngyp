using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class INVService
    {
        private static readonly INVDal CDal = new INVDal();
        public List<INV_NameModel> GetPageList()
        {
            try { return CDal.GetPageList(); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<SelectListItem> GetType(int? pId)
        {
            try { return CDal.GetType(pId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(INV_NameModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public INV_NameModel GetDetailById(int Id)
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
        
    }
}
