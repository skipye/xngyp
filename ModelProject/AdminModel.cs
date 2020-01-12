using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ModelProject
{
    public class AdminModel
    {
        public int? TotalCount { get; set; }
        public int? TodayCount { get; set; }
        public int? WeekCount { get; set; }
        public int? MonthCount { get; set; }
        public int? YearCount { get; set; }
    }
    public class PriceModel
    {
        public decimal? TotalCount { get; set; }
        public decimal? TodayCount { get; set; }
        public decimal? WeekCount { get; set; }
        public decimal? MonthCount { get; set; }
        public decimal? YearCount { get; set; }
    }
    public class CKModel
    {
        public AdminModel FCK { get; set; }
        public AdminModel CK { get; set; }
    }
    public class KCModel
    {
        public AdminModel FSKC { get; set; }
        public AdminModel SKC { get; set; }
        public AdminModel FKC { get; set; }
        public AdminModel KC { get; set; }
    }
    public class XSModel
    {
        public AdminModel FSXHT { get; set; }
        public AdminModel SXHT { get; set; }
        public PriceModel FSX { get; set; }
        public PriceModel SX { get; set; }
    }
    public class SKModel
    {
        public PriceModel FSK { get; set; }
        public PriceModel SK { get; set; }
    }
    public class GXModel
    {
        public int? TotalCount { get; set; }
        public int? KLCount { get; set; }
        public int? MGQCount { get; set; }
        public int? DHCount { get; set; }
        public int? MGHCount { get; set; }
        public int? YQCount { get; set; }
        public int? GMCount { get; set; }
        public int? PJACount { get; set; }
    }
    public class GXCountModel
    {
        public GXModel FGX { get; set; }
        public GXModel GX { get; set; }
    }
}
