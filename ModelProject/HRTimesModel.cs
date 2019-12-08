using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelProject
{
    public class HRTimesModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int? Department { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? d_start { get; set; }
        public DateTime? d_end { get; set; }
        public int? workingtime { get; set; }
        public int? shouldworkingtime { get; set; }
        public float? shouldworkingday { get; set; }
        public float? workingday { get; set; }
        public string d1 { get; set; }
        public string d2 { get; set; }
        public string d3 { get; set; }
        public string d4 { get; set; }
        public string d5 { get; set; }
        public string d6 { get; set; }
        public string d7 { get; set; }
        public string d8 { get; set; }
        public string d9 { get; set; }
        public string d10 { get; set; }
        public string d11 { get; set; }
        public string d12 { get; set; }
        public string d13 { get; set; }
        public string d14 { get; set; }
        public string d15 { get; set; }
        public string d16 { get; set; }
        public string d17 { get; set; }
        public string d18 { get; set; }
        public string d19 { get; set; }
        public string d20 { get; set; }
        public string d21 { get; set; }
        public string d22 { get; set; }
        public string d23 { get; set; }
        public string d24 { get; set; }
        public string d25 { get; set; }
        public string d26 { get; set; }
        public string d27 { get; set; }
        public string d28 { get; set; }
        public string d29 { get; set; }
        public string d30 { get; set; }
        public string d31 { get; set; }
        public int? WorkDay { get; set; }
        public string TopMonthTX { get; set; }
        public string MonthTX { get; set; }
        public string TotalTX { get; set; }
    }
    public class SHRTimesModel
    {
        public int? HRId { get; set; }
        public int TypeId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int columnCount { get; set; }
        public string StrColumn { get; set; }
        public string WorkDate { get; set; }
        public int? WorkTime { get; set; }
        public string Remark { get; set; }
    }
}
