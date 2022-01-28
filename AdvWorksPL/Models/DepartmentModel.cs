using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdvWorksPL.Models
{
    public class DepartmentModel
    {
        [DisplayName("Department Name")]
        [Required(ErrorMessage = "Department name should not be empty.")]
        [RegularExpression(@"^[A-Z\sa-z]+$", ErrorMessage = "Department name must have only english letters")]
        [StringLength(20,MinimumLength = 10,ErrorMessage = "Department name should be between 10 and 20 characters")]
        public string DeptName { get; set; }
        [DisplayName("Group Name")]
        [Required(ErrorMessage = "Group name should not be empty.")]
        [RegularExpression(@"^[A-Z\sa-z]+$", ErrorMessage = "Group name must have only english letters")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Group name should be between 10 and 20 characters")]
        public string DeptGroupName { get; set; }
    }
}