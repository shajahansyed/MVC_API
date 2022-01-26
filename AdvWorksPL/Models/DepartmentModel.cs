using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdvWorksPL.Models
{
    public class DepartmentModel
    {
        [DisplayName("Department Name")]
        public string DeptName { get; set; }
        [DisplayName("Group Name")]
        public string DeptGroupName { get; set; }
    }
}