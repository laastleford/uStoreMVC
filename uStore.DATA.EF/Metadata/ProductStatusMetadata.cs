using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStore.DATA.EF//.Metadata
{
    [MetadataType(typeof(ProductStatusMetadata))]
    public partial class ProductStatus { }

    class ProductStatusMetadata
    {
        [Display(Name ="Status")]
        [StringLength(20,ErrorMessage ="* Cannot exceed 20 chars")]
        [Required(ErrorMessage ="* Status required")]
        public string StatusName { get; set; }

    }
}
