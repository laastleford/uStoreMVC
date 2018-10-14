using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStore.DATA.EF//.Metadata
{
    [MetadataType(typeof(EmployeesMetadata))]
    public partial class Employee { }
    class EmployeesMetadata
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="* First Name required")]
        [StringLength(20, ErrorMessage ="* Cannot exceed 20 chars")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "* Last Name required")]
        [StringLength(50,ErrorMessage ="* Cannot exceed 50 chars")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "* Email required")]
        [StringLength(50, ErrorMessage ="* Cannot exceed 50 chars")]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Display(Name="Start Date")]
        [Required(ErrorMessage = "* Start Date required")]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }
        [Display(Name ="Separation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(NullDisplayText ="N/A",ApplyFormatInEditMode =true)]
        public Nullable<System.DateTime> SeparationDate { get; set; }
        [Display(Name ="Is a Contractor")]
        public bool isContractor { get; set; }

    }
}
