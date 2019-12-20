using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataBase;
using ModelProject;

namespace DalProject
{
    public class HRTimesDal
    {
        //先同步数据
        public List<HRTimesModel> HRTimeList(SHRTimesModel SModel)
        {
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            int CountColumn = 30;
            int CurrMonth = DateTime.Now.Month;
            int CurrDay = DateTime.Now.Day;
            string CrrD = "d" + CurrDay;
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            int NewMonth = EndTime.AddDays(-1).Month;
            int NewDay = EndTime.AddDays(-1).Day;
            CountColumn = EndTime.AddDays(-1).Day;
            if (CurrDay < NewDay && CurrMonth == NewMonth)
            {
                CountColumn = CurrDay;
            }
            AddHRTimeInNewTable(StartTime, EndTime, SModel.TypeId);
            using (var db = new XNHREntities())
            {
                var list = (from p in db.HR_Times.Where(k => k.Department != 355 && k.Name!="叶心峰")
                            where p.d_start >= StartTime
                            where p.d_end <= EndTime
                            where SModel.TypeId == 2 ? p.Department == 350 : SModel.TypeId == 1 ? p.Department != 350 : true
                            select new HRTimesModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Department = p.Department,
                                DepartmentName = p.DepartmentName,
                                d_start = p.d_start,
                                d_end = p.d_end,
                                workingtime = p.workingtime,
                                shouldworkingtime = p.shouldworkingtime,
                                shouldworkingday = p.shouldworkingday,
                                workingday = p.workingday,
                                d1 = p.d1,
                                d2 = p.d2,
                                d3 = p.d3,
                                d4 = p.d4,
                                d5 = p.d5,
                                d6 = p.d6,
                                d7 = p.d7,
                                d8 = p.d8,
                                d9 = p.d9,
                                d10 = p.d10,
                                d11 = p.d11,
                                d12 = p.d12,
                                d13 = p.d13,
                                d14 = p.d14,
                                d15 = p.d15,
                                d16 = p.d16,
                                d17 = p.d17,
                                d18 = p.d18,
                                d19 = p.d19,
                                d20 = p.d20,
                                d21 = p.d21,
                                d22 = p.d22,
                                d23 = p.d23,
                                d24 = p.d24,
                                d25 = p.d25,
                                d26 = p.d26,
                                d27 = p.d27,
                                d28 = p.d28,
                                d29 = p.d29,
                                d30 = p.d30,
                                d31 = p.d31,
                                WorkDay = p.WordDay,
                                TopMonthTX = p.TopMonthTX,
                                MonthTX = p.MonthTX,
                                TotalTX = p.TotalTX,
                            }).ToList();

                foreach (var item in list)
                {
                    var table = db.HR_WorkTime.Where(k => k.HRId == item.Id).ToList();
                    if (table != null)
                    {
                        item.d1 = item.d1 == "" && table.Where(k => k.WorkDate == "d1").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d1").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d1").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d1").Select(k => k.Remark).FirstOrDefault() + " " + item.d1 : item.d1 + " " + table.Where(k => k.WorkDate == "d1").Select(k => k.Remark).FirstOrDefault() : item.d1;
                        if (CountColumn >= 2)
                        {
                            item.d2 = item.d2 == "" && table.Where(k => k.WorkDate == "d2").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d2").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d2").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d2").Select(k => k.Remark).FirstOrDefault() + " " + item.d2 : item.d2 + " " + table.Where(k => k.WorkDate == "d2").Select(k => k.Remark).FirstOrDefault() : item.d2;
                        }
                        else { item.d2 = "无"; }
                        if (CountColumn >= 3)
                        {
                            item.d3 = item.d3 == "" && table.Where(k => k.WorkDate == "d3").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d3").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d3").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d3").Select(k => k.Remark).FirstOrDefault() + " " + item.d3 : item.d3 + " " + table.Where(k => k.WorkDate == "d3").Select(k => k.Remark).FirstOrDefault() : item.d3;
                        }
                        else { item.d3 = "无"; }
                        if (CountColumn >= 4)
                        {
                            item.d4 = item.d4 == "" && table.Where(k => k.WorkDate == "d4").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d4").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d4").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d4").Select(k => k.Remark).FirstOrDefault() + " " + item.d4 : item.d4 + " " + table.Where(k => k.WorkDate == "d4").Select(k => k.Remark).FirstOrDefault() : item.d4;
                        }
                        else { item.d4 = "无"; }
                        if (CountColumn >= 5)
                        {
                            item.d5 = item.d5 == "" && table.Where(k => k.WorkDate == "d5").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d5").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d5").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d5").Select(k => k.Remark).FirstOrDefault() + " " + item.d5 : item.d5 + " " + table.Where(k => k.WorkDate == "d5").Select(k => k.Remark).FirstOrDefault() : item.d5;
                        }
                        else { item.d5 = "无"; }
                        if (CountColumn >= 6)
                        {
                            item.d6 = item.d6 == "" && table.Where(k => k.WorkDate == "d6").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d6").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d6").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d6").Select(k => k.Remark).FirstOrDefault() + " " + item.d6 : item.d6 + " " + table.Where(k => k.WorkDate == "d6").Select(k => k.Remark).FirstOrDefault() : item.d6;
                        }
                        else { item.d6 = "无"; }
                        if (CountColumn >= 7)
                        {
                            item.d7 = item.d7 == "" && table.Where(k => k.WorkDate == "d7").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d7").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d7").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d7").Select(k => k.Remark).FirstOrDefault() + " " + item.d7 : item.d7 + " " + table.Where(k => k.WorkDate == "d7").Select(k => k.Remark).FirstOrDefault() : item.d7;
                        }
                        else { item.d7 = "无"; }
                        if (CountColumn >= 8)
                        {
                            item.d8 = item.d8 == "" && table.Where(k => k.WorkDate == "d8").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d8").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d8").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d8").Select(k => k.Remark).FirstOrDefault() + " " + item.d8 : item.d8 + " " + table.Where(k => k.WorkDate == "d8").Select(k => k.Remark).FirstOrDefault() : item.d8;
                        }
                        else { item.d8 = "无"; }
                        if (CountColumn >= 9)
                        {
                            item.d9 = item.d9 == "" && table.Where(k => k.WorkDate == "d9").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d9").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d9").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d9").Select(k => k.Remark).FirstOrDefault() + " " + item.d9 : item.d9 + " " + table.Where(k => k.WorkDate == "d9").Select(k => k.Remark).FirstOrDefault() : item.d9;
                        }
                        else { item.d9 = "无"; }
                        if (CountColumn >= 10)
                        {
                            item.d10 = item.d10 == "" && table.Where(k => k.WorkDate == "d10").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d10").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d10").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d10").Select(k => k.Remark).FirstOrDefault() + " " + item.d10 : item.d10 + " " + table.Where(k => k.WorkDate == "d10").Select(k => k.Remark).FirstOrDefault() : item.d10;
                        }
                        else { item.d10 = "无"; }
                        if (CountColumn >= 11)
                        {
                            item.d11 = item.d11 == "" && table.Where(k => k.WorkDate == "d11").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d11").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d11").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d11").Select(k => k.Remark).FirstOrDefault() + " " + item.d11 : item.d11 + " " + table.Where(k => k.WorkDate == "d11").Select(k => k.Remark).FirstOrDefault() : item.d11;
                        }
                        else { item.d11 = "无"; }
                        if (CountColumn >= 12)
                        {
                            item.d12 = item.d12 == "" && table.Where(k => k.WorkDate == "d12").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d12").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d12").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d12").Select(k => k.Remark).FirstOrDefault() + " " + item.d12 : item.d12 + " " + table.Where(k => k.WorkDate == "d12").Select(k => k.Remark).FirstOrDefault() : item.d12;
                        }
                        else { item.d12 = "无"; }
                        if (CountColumn >= 13)
                        {
                            item.d13 = item.d13 == "" && table.Where(k => k.WorkDate == "d13").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d13").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d13").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d13").Select(k => k.Remark).FirstOrDefault() + " " + item.d13 : item.d13 + " " + table.Where(k => k.WorkDate == "d13").Select(k => k.Remark).FirstOrDefault() : item.d13;
                        }
                        else { item.d13 = "无"; }
                        if (CountColumn >= 14)
                        {
                            item.d14 = item.d14 == "" && table.Where(k => k.WorkDate == "d14").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d14").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d14").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d14").Select(k => k.Remark).FirstOrDefault() + " " + item.d14 : item.d14 + " " + table.Where(k => k.WorkDate == "d14").Select(k => k.Remark).FirstOrDefault() : item.d14;
                        }
                        else { item.d14 = "无"; }
                        if (CountColumn >= 15)
                        {
                            item.d15 = item.d15 == "" && table.Where(k => k.WorkDate == "d15").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d15").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d15").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d15").Select(k => k.Remark).FirstOrDefault() + " " + item.d15 : item.d15 + " " + table.Where(k => k.WorkDate == "d15").Select(k => k.Remark).FirstOrDefault() : item.d15;
                        }
                        else { item.d15 = "无"; }
                        if (CountColumn >= 16)
                        {
                            item.d16 = item.d16 == "" && table.Where(k => k.WorkDate == "d16").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d16").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d16").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d16").Select(k => k.Remark).FirstOrDefault() + " " + item.d16 : item.d16 + " " + table.Where(k => k.WorkDate == "d16").Select(k => k.Remark).FirstOrDefault() : item.d16;
                        }
                        else { item.d16 = "无"; }
                        if (CountColumn >= 17)
                        {
                            item.d17 = item.d17 == "" && table.Where(k => k.WorkDate == "d17").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d17").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d17").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d17").Select(k => k.Remark).FirstOrDefault() + " " + item.d17 : item.d17 + " " + table.Where(k => k.WorkDate == "d17").Select(k => k.Remark).FirstOrDefault() : item.d17;
                        }
                        else { item.d17 = "无"; }
                        if (CountColumn >= 18)
                        {
                            item.d18 = item.d18 == "" && table.Where(k => k.WorkDate == "d18").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d18").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d18").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d18").Select(k => k.Remark).FirstOrDefault() + " " + item.d18 : item.d18 + " " + table.Where(k => k.WorkDate == "d18").Select(k => k.Remark).FirstOrDefault() : item.d18;
                        }
                        else { item.d18 = "无"; }
                        if (CountColumn >= 19)
                        {
                            item.d19 = item.d19 == "" && table.Where(k => k.WorkDate == "d19").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d19").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d19").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d19").Select(k => k.Remark).FirstOrDefault() + " " + item.d19 : item.d19 + " " + table.Where(k => k.WorkDate == "d19").Select(k => k.Remark).FirstOrDefault() : item.d19;
                        }
                        else { item.d19 = "无"; }
                        if (CountColumn >= 20)
                        {
                            item.d20 = item.d20 == "" && table.Where(k => k.WorkDate == "d20").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d20").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d20").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d20").Select(k => k.Remark).FirstOrDefault() + " " + item.d20 : item.d20 + " " + table.Where(k => k.WorkDate == "d20").Select(k => k.Remark).FirstOrDefault() : item.d20;
                        }
                        else { item.d20 = "无"; }
                        if (CountColumn >= 21)
                        {
                            item.d21 = item.d21 == "" && table.Where(k => k.WorkDate == "d21").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d21").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d21").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d21").Select(k => k.Remark).FirstOrDefault() + " " + item.d21 : item.d21 + " " + table.Where(k => k.WorkDate == "d21").Select(k => k.Remark).FirstOrDefault() : item.d21;
                        }
                        else { item.d21 = "无"; }
                        if (CountColumn >= 22)
                        {
                            item.d22 = item.d22 == "" && table.Where(k => k.WorkDate == "d22").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d22").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d22").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d22").Select(k => k.Remark).FirstOrDefault() + " " + item.d22 : item.d22 + " " + table.Where(k => k.WorkDate == "d22").Select(k => k.Remark).FirstOrDefault() : item.d22;
                        }
                        else { item.d22 = "无"; }
                        if (CountColumn >= 23)
                        {
                            item.d23 = item.d23 == "" && table.Where(k => k.WorkDate == "d23").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d23").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d23").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d23").Select(k => k.Remark).FirstOrDefault() + " " + item.d23 : item.d23 + " " + table.Where(k => k.WorkDate == "d23").Select(k => k.Remark).FirstOrDefault() : item.d23;
                        }
                        else { item.d23 = "无"; }
                        if (CountColumn >= 24)
                        {
                            item.d24 = item.d24 == "" && table.Where(k => k.WorkDate == "d24").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d24").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d24").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d24").Select(k => k.Remark).FirstOrDefault() + " " + item.d24 : item.d24 + " " + table.Where(k => k.WorkDate == "d24").Select(k => k.Remark).FirstOrDefault() : item.d24;
                        }
                        else { item.d24 = "无"; }
                        if (CountColumn >= 25)
                        {
                            item.d25 = item.d25 == "" && table.Where(k => k.WorkDate == "d25").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d25").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d25").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d25").Select(k => k.Remark).FirstOrDefault() + " " + item.d25 : item.d25 + " " + table.Where(k => k.WorkDate == "d25").Select(k => k.Remark).FirstOrDefault() : item.d25;
                        }
                        else { item.d25 = "无"; }
                        if (CountColumn >= 26)
                        {
                            item.d26 = item.d26 == "" && table.Where(k => k.WorkDate == "d26").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d26").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d26").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d26").Select(k => k.Remark).FirstOrDefault() + " " + item.d26 : item.d26 + " " + table.Where(k => k.WorkDate == "d26").Select(k => k.Remark).FirstOrDefault() : item.d26;
                        }
                        else { item.d26 = "无"; }
                        if (CountColumn >= 27)
                        {
                            item.d27 = item.d27 == "" && table.Where(k => k.WorkDate == "d27").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d27").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d27").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d27").Select(k => k.Remark).FirstOrDefault() + " " + item.d27 : item.d27 + " " + table.Where(k => k.WorkDate == "d27").Select(k => k.Remark).FirstOrDefault() : item.d27;
                        }
                        else { item.d27 = "无"; }
                        if (CountColumn >= 28)
                        {
                            item.d28 = item.d28 == "" && table.Where(k => k.WorkDate == "d28").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d28").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d28").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d28").Select(k => k.Remark).FirstOrDefault() + " " + item.d28 : item.d28 + " " + table.Where(k => k.WorkDate == "d28").Select(k => k.Remark).FirstOrDefault() : item.d28;
                        }
                        else { item.d28 = "无"; }
                        if (CountColumn >= 29)
                        {
                            item.d29 = item.d29 == "" && table.Where(k => k.WorkDate == "d29").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d29").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d29").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d29").Select(k => k.Remark).FirstOrDefault() + " " + item.d29 : item.d29 + " " + table.Where(k => k.WorkDate == "d29").Select(k => k.Remark).FirstOrDefault() : item.d29;
                        }
                        else { item.d29 = "无"; }
                        if (CountColumn >= 30)
                        {
                            item.d30 = item.d30 == "" && table.Where(k => k.WorkDate == "d30").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d30").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d30").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d30").Select(k => k.Remark).FirstOrDefault() + " " + item.d30 : item.d30 + " " + table.Where(k => k.WorkDate == "d30").Select(k => k.Remark).FirstOrDefault() : item.d30;
                        }
                        else { item.d30 = "无"; }
                        if (CountColumn >= 31)
                        {
                            item.d31 = item.d31 == "" && table.Where(k => k.WorkDate == "d31").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d31").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d31").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d31").Select(k => k.Remark).FirstOrDefault() + " " + item.d31 : item.d31 + " " + table.Where(k => k.WorkDate == "d31").Select(k => k.Remark).FirstOrDefault() : item.d31;
                        }
                        else { item.d31 = "无"; }
                    }


                }
                return list;
            }
        }
        //下载考勤数据到新表中
        public void AddHRTimeInNewTable(DateTime StartTime, DateTime EndTime,int TypeId)
        {
            using (var db = new XNHREntities())
            {
                string StrSqlImg = string.Format(@"delete HR_Times where d_start>='{0}' and d_end<='{1}'", StartTime, EndTime);
                db.Database.ExecuteSqlCommand(StrSqlImg);

                var listNew = (from p in db.ehr_sum.Where(k => k.department != 355)
                               where p.d_start >= StartTime
                               where p.d_end <= EndTime
                               where TypeId == 2 ? p.department == 350 : TypeId == 1 ? p.department != 350 : true
                               select new HRTimesModel
                               {
                                   Id = p.id,
                                   Name = p.name,
                                   Department = p.department,
                                   DepartmentName = p.departmentname,
                                   d_start = p.d_start,
                                   d_end = p.d_end,
                                   workingtime = p.workingtime,
                                   shouldworkingtime = p.shouldworkingtime,
                                   shouldworkingday = p.shouldworkingday,
                                   workingday = p.workingday,
                                   d1 = p.d1,
                                   d2 = p.d2,
                                   d3 = p.d3,
                                   d4 = p.d4,
                                   d5 = p.d5,
                                   d6 = p.d6,
                                   d7 = p.d7,
                                   d8 = p.d8,
                                   d9 = p.d9,
                                   d10 = p.d10,
                                   d11 = p.d11,
                                   d12 = p.d12,
                                   d13 = p.d13,
                                   d14 = p.d14,
                                   d15 = p.d15,
                                   d16 = p.d16,
                                   d17 = p.d17,
                                   d18 = p.d18,
                                   d19 = p.d19,
                                   d20 = p.d20,
                                   d21 = p.d21,
                                   d22 = p.d22,
                                   d23 = p.d23,
                                   d24 = p.d24,
                                   d25 = p.d25,
                                   d26 = p.d26,
                                   d27 = p.d27,
                                   d28 = p.d28,
                                   d29 = p.d29,
                                   d30 = p.d30,
                                   d31 = p.d31,

                               }).ToList();
                foreach (var item in listNew)
                {
                    var tables = new HR_Times();
                    tables.Name = item.Name;
                    tables.Department = item.Department;
                    tables.DepartmentName = item.DepartmentName;
                    tables.workingtime = item.workingtime;
                    tables.d_start = item.d_start;
                    tables.d_end = item.d_end;
                    tables.shouldworkingtime = item.shouldworkingtime;
                    tables.shouldworkingday = item.shouldworkingday;
                    tables.workingday = item.workingday;
                    tables.d1 = item.d1;
                    tables.d2 = item.d2;
                    tables.d3 = item.d3;
                    tables.d4 = item.d4;
                    tables.d5 = item.d5;
                    tables.d6 = item.d6;
                    tables.d7 = item.d7;
                    tables.d8 = item.d8;
                    tables.d9 = item.d9;
                    tables.d10 = item.d10;
                    tables.d11 = item.d11;
                    tables.d12 = item.d12;
                    tables.d13 = item.d13;
                    tables.d14 = item.d14;
                    tables.d15 = item.d15;
                    tables.d16 = item.d16;
                    tables.d17 = item.d17;
                    tables.d18 = item.d18;
                    tables.d19 = item.d19;
                    tables.d20 = item.d20;
                    tables.d21 = item.d21;
                    tables.d22 = item.d22;
                    tables.d23 = item.d23;
                    tables.d24 = item.d24;
                    tables.d25 = item.d25;
                    tables.d26 = item.d26;
                    tables.d27 = item.d27;
                    tables.d28 = item.d28;
                    tables.d29 = item.d29;
                    tables.d30 = item.d30;
                    tables.d31 = item.d31;
                    tables.WordDay = Convert.ToInt32(item.d1.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d2.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d3.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d4.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d5.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d6.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d7.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d8.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d9.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d10.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d11.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d12.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d13.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d14.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d15.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d16.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d17.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d18.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d19.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d20.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d21.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d22.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d23.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d24.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d25.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d26.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d27.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d28.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d29.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d30.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d1.Length > 0 ? 1 : 0);
                    db.HR_Times.Add(tables);
                }
                db.SaveChanges();
            }
        }
        //导出
        public DataTable ToExcelOut(SHRTimesModel SModel)
        {
            DataTable Exceltable = new DataTable();
            DateTime StartTime = Convert.ToDateTime("1999-12-31");
            DateTime EndTime = Convert.ToDateTime("2999-12-31");
            int CountColumn = 30;
            int CurrMonth = DateTime.Now.Month;
            int CurrDay = DateTime.Now.Day;
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            int NewMonth = EndTime.AddDays(-1).Month;
            int NewDay = EndTime.AddDays(-1).Day;
            CountColumn = EndTime.AddDays(-1).Day;
            if (CurrDay < NewDay && CurrMonth == NewMonth)
            {
                CountColumn = CurrDay;
            }
            using (var db = new XNHREntities())
            {
                var list = (from p in db.HR_Times.Where(k => k.Department != 355)
                            where p.d_start >= StartTime
                            where p.d_end <= EndTime
                            where SModel.TypeId == 2 ? p.Department == 350 : SModel.TypeId == 1 ? p.Department != 350 : true
                            select new HRTimesModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Department = p.Department,
                                DepartmentName = p.DepartmentName,
                                d_start = p.d_start,
                                d_end = p.d_end,
                                workingtime = p.workingtime,
                                shouldworkingtime = p.shouldworkingtime,
                                shouldworkingday = p.shouldworkingday,
                                workingday = p.workingday,
                                d1 = p.d1,
                                d2 = p.d2,
                                d3 = p.d3,
                                d4 = p.d4,
                                d5 = p.d5,
                                d6 = p.d6,
                                d7 = p.d7,
                                d8 = p.d8,
                                d9 = p.d9,
                                d10 = p.d10,
                                d11 = p.d11,
                                d12 = p.d12,
                                d13 = p.d13,
                                d14 = p.d14,
                                d15 = p.d15,
                                d16 = p.d16,
                                d17 = p.d17,
                                d18 = p.d18,
                                d19 = p.d19,
                                d20 = p.d20,
                                d21 = p.d21,
                                d22 = p.d22,
                                d23 = p.d23,
                                d24 = p.d24,
                                d25 = p.d25,
                                d26 = p.d26,
                                d27 = p.d27,
                                d28 = p.d28,
                                d29 = p.d29,
                                d30 = p.d30,
                                d31 = p.d31,
                                WorkDay = p.WordDay,
                                TopMonthTX = p.TopMonthTX,
                                MonthTX = p.MonthTX,
                                TotalTX = p.TotalTX,
                            }).ToList();
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                    {
                        var table = db.HR_WorkTime.Where(k => k.HRId == item.Id).ToList();
                        if (table != null)
                        {
                            item.d1 = item.d1 == "" && table.Where(k => k.WorkDate == "d1").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d1").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d1").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d1").Select(k => k.Remark).FirstOrDefault() + " " + item.d1 : item.d1 + " " + table.Where(k => k.WorkDate == "d1").Select(k => k.Remark).FirstOrDefault() : item.d1;
                            if (CountColumn >= 2)
                            {
                                item.d2 = item.d2 == "" && table.Where(k => k.WorkDate == "d2").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d2").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d2").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d2").Select(k => k.Remark).FirstOrDefault() + " " + item.d2 : item.d2 + " " + table.Where(k => k.WorkDate == "d2").Select(k => k.Remark).FirstOrDefault() : item.d2;
                            }
                            else { item.d2 = "无"; }
                            if (CountColumn >= 3)
                            {
                                item.d3 = item.d3 == "" && table.Where(k => k.WorkDate == "d3").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d3").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d3").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d3").Select(k => k.Remark).FirstOrDefault() + " " + item.d3 : item.d3 + " " + table.Where(k => k.WorkDate == "d3").Select(k => k.Remark).FirstOrDefault() : item.d3;
                            }
                            else { item.d3 = "无"; }
                            if (CountColumn >= 4)
                            {
                                item.d4 = item.d4 == "" && table.Where(k => k.WorkDate == "d4").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d4").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d4").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d4").Select(k => k.Remark).FirstOrDefault() + " " + item.d4 : item.d4 + " " + table.Where(k => k.WorkDate == "d4").Select(k => k.Remark).FirstOrDefault() : item.d4;
                            }
                            else { item.d4 = "无"; }
                            if (CountColumn >= 5)
                            {
                                item.d5 = item.d5 == "" && table.Where(k => k.WorkDate == "d5").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d5").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d5").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d5").Select(k => k.Remark).FirstOrDefault() + " " + item.d5 : item.d5 + " " + table.Where(k => k.WorkDate == "d5").Select(k => k.Remark).FirstOrDefault() : item.d5;
                            }
                            else { item.d5 = "无"; }
                            if (CountColumn >= 6)
                            {
                                item.d6 = item.d6 == "" && table.Where(k => k.WorkDate == "d6").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d6").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d6").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d6").Select(k => k.Remark).FirstOrDefault() + " " + item.d6 : item.d6 + " " + table.Where(k => k.WorkDate == "d6").Select(k => k.Remark).FirstOrDefault() : item.d6;
                            }
                            else { item.d6 = "无"; }
                            if (CountColumn >= 7)
                            {
                                item.d7 = item.d7 == "" && table.Where(k => k.WorkDate == "d7").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d7").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d7").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d7").Select(k => k.Remark).FirstOrDefault() + " " + item.d7 : item.d7 + " " + table.Where(k => k.WorkDate == "d7").Select(k => k.Remark).FirstOrDefault() : item.d7;
                            }
                            else { item.d7 = "无"; }
                            if (CountColumn >= 8)
                            {
                                item.d8 = item.d8 == "" && table.Where(k => k.WorkDate == "d8").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d8").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d8").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d8").Select(k => k.Remark).FirstOrDefault() + " " + item.d8 : item.d8 + " " + table.Where(k => k.WorkDate == "d8").Select(k => k.Remark).FirstOrDefault() : item.d8;
                            }
                            else { item.d8 = "无"; }
                            if (CountColumn >= 9)
                            {
                                item.d9 = item.d9 == "" && table.Where(k => k.WorkDate == "d9").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d9").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d9").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d9").Select(k => k.Remark).FirstOrDefault() + " " + item.d9 : item.d9 + " " + table.Where(k => k.WorkDate == "d9").Select(k => k.Remark).FirstOrDefault() : item.d9;
                            }
                            else { item.d9 = "无"; }
                            if (CountColumn >= 10)
                            {
                                item.d10 = item.d10 == "" && table.Where(k => k.WorkDate == "d10").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d10").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d10").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d10").Select(k => k.Remark).FirstOrDefault() + " " + item.d10 : item.d10 + " " + table.Where(k => k.WorkDate == "d10").Select(k => k.Remark).FirstOrDefault() : item.d10;
                            }
                            else { item.d10 = "无"; }
                            if (CountColumn >= 11)
                            {
                                item.d11 = item.d11 == "" && table.Where(k => k.WorkDate == "d11").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d11").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d11").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d11").Select(k => k.Remark).FirstOrDefault() + " " + item.d11 : item.d11 + " " + table.Where(k => k.WorkDate == "d11").Select(k => k.Remark).FirstOrDefault() : item.d11;
                            }
                            else { item.d11 = "无"; }
                            if (CountColumn >= 12)
                            {
                                item.d12 = item.d12 == "" && table.Where(k => k.WorkDate == "d12").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d12").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d12").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d12").Select(k => k.Remark).FirstOrDefault() + " " + item.d12 : item.d12 + " " + table.Where(k => k.WorkDate == "d12").Select(k => k.Remark).FirstOrDefault() : item.d12;
                            }
                            else { item.d12 = "无"; }
                            if (CountColumn >= 13)
                            {
                                item.d13 = item.d13 == "" && table.Where(k => k.WorkDate == "d13").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d13").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d13").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d13").Select(k => k.Remark).FirstOrDefault() + " " + item.d13 : item.d13 + " " + table.Where(k => k.WorkDate == "d13").Select(k => k.Remark).FirstOrDefault() : item.d13;
                            }
                            else { item.d13 = "无"; }
                            if (CountColumn >= 14)
                            {
                                item.d14 = item.d14 == "" && table.Where(k => k.WorkDate == "d14").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d14").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d14").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d14").Select(k => k.Remark).FirstOrDefault() + " " + item.d14 : item.d14 + " " + table.Where(k => k.WorkDate == "d14").Select(k => k.Remark).FirstOrDefault() : item.d14;
                            }
                            else { item.d14 = "无"; }
                            if (CountColumn >= 15)
                            {
                                item.d15 = item.d15 == "" && table.Where(k => k.WorkDate == "d15").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d15").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d15").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d15").Select(k => k.Remark).FirstOrDefault() + " " + item.d15 : item.d15 + " " + table.Where(k => k.WorkDate == "d15").Select(k => k.Remark).FirstOrDefault() : item.d15;
                            }
                            else { item.d15 = "无"; }
                            if (CountColumn >= 16)
                            {
                                item.d16 = item.d16 == "" && table.Where(k => k.WorkDate == "d16").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d16").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d16").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d16").Select(k => k.Remark).FirstOrDefault() + " " + item.d16 : item.d16 + " " + table.Where(k => k.WorkDate == "d16").Select(k => k.Remark).FirstOrDefault() : item.d16;
                            }
                            else { item.d16 = "无"; }
                            if (CountColumn >= 17)
                            {
                                item.d17 = item.d17 == "" && table.Where(k => k.WorkDate == "d17").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d17").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d17").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d17").Select(k => k.Remark).FirstOrDefault() + " " + item.d17 : item.d17 + " " + table.Where(k => k.WorkDate == "d17").Select(k => k.Remark).FirstOrDefault() : item.d17;
                            }
                            else { item.d17 = "无"; }
                            if (CountColumn >= 18)
                            {
                                item.d18 = item.d18 == "" && table.Where(k => k.WorkDate == "d18").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d18").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d18").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d18").Select(k => k.Remark).FirstOrDefault() + " " + item.d18 : item.d18 + " " + table.Where(k => k.WorkDate == "d18").Select(k => k.Remark).FirstOrDefault() : item.d18;
                            }
                            else { item.d18 = "无"; }
                            if (CountColumn >= 19)
                            {
                                item.d19 = item.d19 == "" && table.Where(k => k.WorkDate == "d19").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d19").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d19").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d19").Select(k => k.Remark).FirstOrDefault() + " " + item.d19 : item.d19 + " " + table.Where(k => k.WorkDate == "d19").Select(k => k.Remark).FirstOrDefault() : item.d19;
                            }
                            else { item.d19 = "无"; }
                            if (CountColumn >= 20)
                            {
                                item.d20 = item.d20 == "" && table.Where(k => k.WorkDate == "d20").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d20").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d20").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d20").Select(k => k.Remark).FirstOrDefault() + " " + item.d20 : item.d20 + " " + table.Where(k => k.WorkDate == "d20").Select(k => k.Remark).FirstOrDefault() : item.d20;
                            }
                            else { item.d20 = "无"; }
                            if (CountColumn >= 21)
                            {
                                item.d21 = item.d21 == "" && table.Where(k => k.WorkDate == "d21").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d21").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d21").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d21").Select(k => k.Remark).FirstOrDefault() + " " + item.d21 : item.d21 + " " + table.Where(k => k.WorkDate == "d21").Select(k => k.Remark).FirstOrDefault() : item.d21;
                            }
                            else { item.d21 = "无"; }
                            if (CountColumn >= 22)
                            {
                                item.d22 = item.d22 == "" && table.Where(k => k.WorkDate == "d22").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d22").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d22").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d22").Select(k => k.Remark).FirstOrDefault() + " " + item.d22 : item.d22 + " " + table.Where(k => k.WorkDate == "d22").Select(k => k.Remark).FirstOrDefault() : item.d22;
                            }
                            else { item.d22 = "无"; }
                            if (CountColumn >= 23)
                            {
                                item.d23 = item.d23 == "" && table.Where(k => k.WorkDate == "d23").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d23").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d23").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d23").Select(k => k.Remark).FirstOrDefault() + " " + item.d23 : item.d23 + " " + table.Where(k => k.WorkDate == "d23").Select(k => k.Remark).FirstOrDefault() : item.d23;
                            }
                            else { item.d23 = "无"; }
                            if (CountColumn >= 24)
                            {
                                item.d24 = item.d24 == "" && table.Where(k => k.WorkDate == "d24").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d24").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d24").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d24").Select(k => k.Remark).FirstOrDefault() + " " + item.d24 : item.d24 + " " + table.Where(k => k.WorkDate == "d24").Select(k => k.Remark).FirstOrDefault() : item.d24;
                            }
                            else { item.d24 = "无"; }
                            if (CountColumn >= 25)
                            {
                                item.d25 = item.d25 == "" && table.Where(k => k.WorkDate == "d25").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d25").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d25").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d25").Select(k => k.Remark).FirstOrDefault() + " " + item.d25 : item.d25 + " " + table.Where(k => k.WorkDate == "d25").Select(k => k.Remark).FirstOrDefault() : item.d25;
                            }
                            else { item.d25 = "无"; }
                            if (CountColumn >= 26)
                            {
                                item.d26 = item.d26 == "" && table.Where(k => k.WorkDate == "d26").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d26").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d26").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d26").Select(k => k.Remark).FirstOrDefault() + " " + item.d26 : item.d26 + " " + table.Where(k => k.WorkDate == "d26").Select(k => k.Remark).FirstOrDefault() : item.d26;
                            }
                            else { item.d26 = "无"; }
                            if (CountColumn >= 27)
                            {
                                item.d27 = item.d27 == "" && table.Where(k => k.WorkDate == "d27").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d27").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d27").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d27").Select(k => k.Remark).FirstOrDefault() + " " + item.d27 : item.d27 + " " + table.Where(k => k.WorkDate == "d27").Select(k => k.Remark).FirstOrDefault() : item.d27;
                            }
                            else { item.d27 = "无"; }
                            if (CountColumn >= 28)
                            {
                                item.d28 = item.d28 == "" && table.Where(k => k.WorkDate == "d28").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d28").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d28").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d28").Select(k => k.Remark).FirstOrDefault() + " " + item.d28 : item.d28 + " " + table.Where(k => k.WorkDate == "d28").Select(k => k.Remark).FirstOrDefault() : item.d28;
                            }
                            else { item.d28 = "无"; }
                            if (CountColumn >= 29)
                            {
                                item.d29 = item.d29 == "" && table.Where(k => k.WorkDate == "d29").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d29").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d29").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d29").Select(k => k.Remark).FirstOrDefault() + " " + item.d29 : item.d29 + " " + table.Where(k => k.WorkDate == "d29").Select(k => k.Remark).FirstOrDefault() : item.d29;
                            }
                            else { item.d29 = "无"; }
                            if (CountColumn >= 30)
                            {
                                item.d30 = item.d30 == "" && table.Where(k => k.WorkDate == "d30").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d30").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d30").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d30").Select(k => k.Remark).FirstOrDefault() + " " + item.d30 : item.d30 + " " + table.Where(k => k.WorkDate == "d30").Select(k => k.Remark).FirstOrDefault() : item.d30;
                            }
                            else { item.d30 = "无"; }
                            if (CountColumn >= 31)
                            {
                                item.d31 = item.d31 == "" && table.Where(k => k.WorkDate == "d31").FirstOrDefault() == null ? "休" : table.Where(k => k.WorkDate == "d31").FirstOrDefault() != null ? table.Where(k => k.WorkDate == "d31").Select(k => k.WorkTime).FirstOrDefault() == 1 ? table.Where(k => k.WorkDate == "d31").Select(k => k.Remark).FirstOrDefault() + " " + item.d31 : item.d31 + " " + table.Where(k => k.WorkDate == "d31").Select(k => k.Remark).FirstOrDefault() : item.d31;
                            }
                            else { item.d31 = "无"; }
                        }
                    }
                }

                if (list != null && list.Any())
                {
                    Exceltable.Columns.Add("姓名", typeof(string));
                    Exceltable.Columns.Add("部门", typeof(string));
                    Exceltable.Columns.Add("月份", typeof(string));
                    Exceltable.Columns.Add("1", typeof(string));
                    Exceltable.Columns.Add("2", typeof(string));
                    Exceltable.Columns.Add("3", typeof(string));
                    Exceltable.Columns.Add("4", typeof(string));
                    Exceltable.Columns.Add("5", typeof(string));
                    Exceltable.Columns.Add("6", typeof(string));
                    Exceltable.Columns.Add("7", typeof(string));
                    Exceltable.Columns.Add("8", typeof(string));
                    Exceltable.Columns.Add("9", typeof(string));
                    Exceltable.Columns.Add("10", typeof(string));
                    Exceltable.Columns.Add("11", typeof(string));
                    Exceltable.Columns.Add("12", typeof(string));
                    Exceltable.Columns.Add("13", typeof(string));
                    Exceltable.Columns.Add("14", typeof(string));
                    Exceltable.Columns.Add("15", typeof(string));
                    Exceltable.Columns.Add("16", typeof(string));
                    Exceltable.Columns.Add("17", typeof(string));
                    Exceltable.Columns.Add("18", typeof(string));
                    Exceltable.Columns.Add("19", typeof(string));
                    Exceltable.Columns.Add("20", typeof(string));
                    Exceltable.Columns.Add("21", typeof(string));
                    Exceltable.Columns.Add("22", typeof(string));
                    Exceltable.Columns.Add("23", typeof(string));
                    Exceltable.Columns.Add("24", typeof(string));
                    Exceltable.Columns.Add("25", typeof(string));
                    Exceltable.Columns.Add("26", typeof(string));
                    Exceltable.Columns.Add("27", typeof(string));
                    Exceltable.Columns.Add("28", typeof(string));
                    Exceltable.Columns.Add("29", typeof(string));
                    Exceltable.Columns.Add("30", typeof(string));
                    Exceltable.Columns.Add("31", typeof(string));
                    if (SModel.DCTypeId == 1)
                    {
                        Exceltable.Columns.Add("应出勤", typeof(string));
                        Exceltable.Columns.Add("实出勤", typeof(string));
                        Exceltable.Columns.Add("上月累计调休", typeof(string));
                        Exceltable.Columns.Add("本月调休", typeof(string));
                        Exceltable.Columns.Add("累计调休", typeof(string));
                    }
                    if (SModel.DCTypeId == 2 || SModel.DCTypeId == 4)
                    {
                        Exceltable.Columns.Add("出勤", typeof(string));
                        Exceltable.Columns.Add("上班", typeof(string));
                    }
                    foreach (var item in list)
                    {
                        DataRow row = Exceltable.NewRow();
                        row["姓名"] = item.Name;
                        row["部门"] = item.DepartmentName;
                        row["月份"] = NewMonth;
                        row["1"] = item.d1;
                        row["2"] = item.d2;
                        row["3"] = item.d3;
                        row["4"] = item.d4;
                        row["5"] = item.d5;
                        row["6"] = item.d6;
                        row["7"] = item.d7;
                        row["8"] = item.d8;
                        row["9"] = item.d9;
                        row["10"] = item.d10;
                        row["11"] = item.d11;
                        row["12"] = item.d12;
                        row["13"] = item.d13;
                        row["14"] = item.d14;
                        row["15"] = item.d15;
                        row["16"] = item.d16;
                        row["17"] = item.d17;
                        row["18"] = item.d18;
                        row["19"] = item.d19;
                        row["20"] = item.d20;
                        row["21"] = item.d21;
                        row["22"] = item.d22;
                        row["23"] = item.d23;
                        row["24"] = item.d24;
                        row["25"] = item.d25;
                        row["26"] = item.d26;
                        row["27"] = item.d27;
                        row["28"] = item.d28;
                        row["29"] = item.d29;
                        row["30"] = item.d30;
                        row["31"] = item.d10;
                        if (SModel.DCTypeId == 1)
                        {
                            row["应出勤"] = item.shouldworkingday;
                            row["实出勤"] = item.WorkDay;
                            row["上月累计调休"] = item.TopMonthTX;
                            row["本月调休"] = item.MonthTX;
                            row["累计调休"] = item.TotalTX;
                        }
                        if (SModel.DCTypeId == 2 || SModel.DCTypeId == 4)
                        {
                            row["出勤"] = item.WorkDay;
                            row["加班"] = item.MonthTX;
                        }
                        Exceltable.Rows.Add(row);
                    }
                }
            }
            return Exceltable;
        }

        public void AddOrUpdate(SHRTimesModel Models)
        {
            Random r = new Random();
            using (var db = new XNHREntities())
            {
                var table = db.HR_WorkTime.Where(k => k.HRId == Models.HRId && k.WorkDate == Models.WorkDate).SingleOrDefault();
                if (table!=null)
                {
                    table.WorkTime = Models.WorkTime;
                    table.Remark = Models.Remark;
                }
                else
                {
                    HR_WorkTime tables = new HR_WorkTime();
                    tables.Id = Guid.NewGuid();
                    tables.HRId = Models.HRId;
                    tables.WorkDate = Models.WorkDate;
                    tables.WorkTime = Models.WorkTime;
                    tables.Remark = Models.Remark;
                    tables.CreateTime = DateTime.Now;
                    
                    db.HR_WorkTime.Add(tables);
                }
                db.SaveChanges();
            }
        }
        public SHRTimesModel GetDetailById(SHRTimesModel Models)
        {
            using (var db = new XNHREntities())
            {
                var tables = (from p in db.HR_WorkTime.Where(k => k.HRId == Models.HRId && k.WorkDate == Models.WorkDate)
                              select new SHRTimesModel
                              {
                                  HRId = Models.HRId,
                                  WorkDate = Models.WorkDate,
                                  WorkTime = p.WorkTime,
                                  Remark = p.Remark,
                              }).SingleOrDefault();
                return tables;
            }
        }
        public HRTimesModel GetCWTime(int Id)
        {
            using (var db = new XNHREntities())
            {
                var table = (from p in db.HR_Times.Where(k => k.Id == Id)
                             select new HRTimesModel
                             {
                                 Id = p.Id,
                                 shouldworkingday = p.shouldworkingday,
                                 WorkDay = p.WordDay,
                                 TopMonthTX = p.TopMonthTX,
                                 MonthTX = p.MonthTX,
                                 TotalTX = p.TotalTX,
                             }).FirstOrDefault();
                return table;
            }
        }
        public void EditCWTime(HRTimesModel Models)
        {
            using (var db = new XNHREntities())
            {
                var table = db.HR_Times.Where(k => k.Id == Models.Id).SingleOrDefault();
                table.shouldworkingday = Models.shouldworkingday;
                table.WordDay = Models.WorkDay;
                table.TopMonthTX = Models.TopMonthTX;
                table.MonthTX = Models.MonthTX;
                table.TotalTX = Models.TotalTX;
                db.SaveChanges();
            }
        }

    }
}
