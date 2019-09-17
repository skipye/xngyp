using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class LabelsService
    {
        private static readonly LabelsDal CDal = new LabelsDal();
        public List<LabelsModel> GetLabelsList(SLabelsModel SModel)
        {
            try { return CDal.GetLabelsList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public LabelsModel GetDetailById(int Id)
        {
            try { return CDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(LabelsModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
