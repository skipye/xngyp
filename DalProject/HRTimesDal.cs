using System;
using System.Collections.Generic;
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
            if (!string.IsNullOrEmpty(SModel.StartTime))
            {
                StartTime = Convert.ToDateTime(SModel.StartTime);
            }
            if (!string.IsNullOrEmpty(SModel.EndTime))
            {
                EndTime = Convert.ToDateTime(SModel.EndTime).AddDays(1);
            }
            using (var db = new XNHREntities())
            {
                var list = (from p in db.HR_Times.Where(k=>k.Department!=355)
                            where p.d_start >= StartTime
                            where p.d_end <= EndTime
                            where SModel.TypeId == 2 ? p.Department == 350 : SModel.TypeId == 1 ? p.Department != 350 : true
                            select new HRTimesModel
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Department = p.Department,
                                DepartmentName = p.DepartmentName,
                                d_start=p.d_start,
                                d_end=p.d_end,
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
                            }).ToList();
                if (list.Count == 0)
                {
                    var listNew = (from p in db.ehr_sum.Where(k => k.department != 355 && k.employee!= 3285 && k.employee!= 3204)
                                   where p.d_start >= StartTime
                                where p.d_end <= EndTime
                                where SModel.TypeId == 2 ? p.department == 350 : SModel.TypeId == 1 ? p.department != 350 : true
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
                        tables.WordDay = Convert.ToInt32(item.d1.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d2.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d3.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d4.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d5.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d6.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d7.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d8.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d9.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d10.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d11.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d12.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d13.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d14.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d15.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d16.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d17.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d18.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d19.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d20.Length > 0 ? 1 : 0) +Convert.ToInt32(item.d21.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d22.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d23.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d24.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d25.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d26.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d27.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d28.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d29.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d30.Length > 0 ? 1 : 0) + Convert.ToInt32(item.d1.Length > 0 ? 1 :0);
                        db.HR_Times.Add(tables);
                    }
                    db.SaveChanges();
                }

                return list;
            }
        }
    }
}
