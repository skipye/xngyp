﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class XNHREntities : DbContext
    {
        public XNHREntities()
            : base("name=XNHREntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ehr_employee> ehr_employee { get; set; }
        public DbSet<ehr_department> ehr_department { get; set; }
        public DbSet<ehr_postday> ehr_postday { get; set; }
        public DbSet<ehr_sum> ehr_sum { get; set; }
        public DbSet<HR_WorkTime> HR_WorkTime { get; set; }
        public DbSet<HR_Times> HR_Times { get; set; }
    }
}
