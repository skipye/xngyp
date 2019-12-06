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
        
    }
}
