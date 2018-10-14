using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStore.DATA.EF//.Metadata
{
    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }

    class DepartmentMetadata
    {
        [StringLength(50,ErrorMessage ="* Cannot exceed 50 chars")]
        [Display(Name ="Department")]
        [Required(ErrorMessage ="* Department required")]
        public string DeptName { get; set; }

    }
}
