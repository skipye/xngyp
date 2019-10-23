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
        public bool DeleteMore(string ListId)
        {
            try { CDal.DeleteMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //移库操作
        public bool MoveINV(string ListId, int INVId)
        {
            try { CDal.MoveINV(ListId, INVId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcelOut(SLabelsModel SModel)
        {
            try
            {
                return CDal.ToExcelOut(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelsModel> GetWorkLabelsList(SLabelsModel SModel)
        {
            try
            {
                return CDal.GetWorkLabelsList(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //绑定合同操作
        public bool CheckLabels(string ListId, int CRM_Id)
        {
            try
            {
                CDal.CheckLabels(ListId, CRM_Id); return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
