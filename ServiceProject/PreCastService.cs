using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class PreCastService
    {
        private static readonly PreCastDal CDal = new PreCastDal();
        public List<PreCastModel> GetList(SPreCastModel SModel)
        {
            try { return CDal.GetList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public PreCastModel GetDetailById(int Id)
        {
            try { return CDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(PreCastModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
