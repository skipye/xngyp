using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class HRService
    {
        private static readonly HRTimesDal CDal = new HRTimesDal();
        public List<HRTimesModel> HRTimeList(SHRTimesModel SModel)
        {
            try { return CDal.HRTimeList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SHRTimesModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SHRTimesModel GetDetailById(SHRTimesModel Models)
        {
            try { return CDal.GetDetailById(Models); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public HRTimesModel GetCWTime(int Id)
        {
            try { return CDal.GetCWTime(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EditCWTime(HRTimesModel Models)
        {
            try { CDal.EditCWTime(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
