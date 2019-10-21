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
    public class SemiService
    {
        private static readonly SemiDal CDal = new SemiDal();
        public List<SemiModel> GetSemiList(SSemiModel SModel)
        {
            try { return CDal.GetSemiList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SemiModel GetDetailById(int Id)
        {
            try { return CDal.GetDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdate(SemiModel Models)
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
        public DataTable ToExcelOut(SSemiModel SModel)
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
        public bool CheckMore(string ListId, int InvId)
        {
            try { CDal.CheckMore(ListId, InvId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //安排生产
        public bool AddWork(string ListId)
        {
            try { CDal.AddWork(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
