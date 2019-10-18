using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class WorkOrderService
    {
        private static readonly WorkOrderDal WODal = new WorkOrderDal();
        public List<WorkOrderModel> GetPageList(int? Status, bool IsSale)
        {
            try { return WODal.GetPageList(Status, IsSale); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddWorkOrder(string ListId)
        {
            try { WODal.AddWorkOrder(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddWorlFrom(WorkFromModel Models, string ListId)
        {
            try { WODal.AddWorlFrom(Models,ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string GetUserDrolistByJob(string Job)
        {
            try { return WODal.GetUserDrolistByJob(Job); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<WorkOrderEven> GetWorkOrderEvenList(int Id)
        {
            try { return WODal.GetWorkOrderEvenList(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<WorkFromModel> GetFlowList(SWorkFromModel SModel)
        {
            try { return WODal.GetFlowList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Checked(string ListId, int status)
        {
            try { WODal.Checked(ListId, status); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
