using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStore.DATA.EF//.Metadata
{
    [MetadataType(typeof(CategoriesMetadata))]
    public partial class Category { }

    class CategoriesMetadata
    {
        [Display(Name ="Category")]
        [Required(ErrorMessage ="* Category required")]
        [StringLength(100,ErrorMessage ="* Cannot exceed 100 chars")]
        public string CategoryName { get; set; }

    }
}
