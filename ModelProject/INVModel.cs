using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelProject
{
    //仓库设置
    public class INV_NameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string Address { get; set; }//仓库位置
        public string Remark { get; set; }//仓库功能说明
        public DateTime? CreateTime { get; set; }
        public List<SelectListItem> TypeDroList { get; set; }
    }
    public class INV_Name_TypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }//仓库功能说明
    }
    
}
