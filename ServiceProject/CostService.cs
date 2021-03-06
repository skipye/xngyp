﻿using DalProject;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ServiceProject
{
    public class CostService
    {
        private static readonly CostDal CDal = new CostDal();
        public List<CostModel> GetPageList(SCostModel SModel)
        {
            try { return CDal.GetPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public bool AddOrUpdate(CostModel Models)
        {
            try { CDal.AddOrUpdate(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CostModel GetDetailById(int Id)
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
        public List<CostModel> GetFPageList(SCostModel SModel)
        {
            try { return CDal.GetFPageList(SModel); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Decimal? GetChuChangPrice(int ProductId, int WoodId)
        {
            try { return CDal.GetChuChangPrice(ProductId, WoodId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Decimal? GetGYPCCPrice(int ProductId, int WoodId)
        {
            try { return CDal.GetGYPCCPrice(ProductId, WoodId); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddOrUpdateF(CostModel Models)
        {
            try { CDal.AddOrUpdateF(Models); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public CostModel GetFDetailById(int Id)
        {
            try { return CDal.GetFDetailById(Id); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteFMore(string ListId)
        {
            try { CDal.DeleteFMore(ListId); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddFCost()
        {
            try { CDal.AddFCost(); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddGYPCost()
        {
            try { CDal.AddGYPCost(); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateGYPSN()
        {
            try { CDal.UpdateGYPSN(); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateFCost()
        {
            try { CDal.UpdateFCost(); return true; }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToFExcelOut(SCostModel SModel)
        {
            try
            {
                return CDal.ToFExcelOut(SModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ToExcelOut(SCostModel SModel)
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
    }
}
