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
    }
}
