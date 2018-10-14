using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uStore.DATA.EF//.Metadata
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    class ProductMetadata
    {
        [Display(Name ="Product Name")]
        [Required(ErrorMessage ="* Product Name required")]
        [StringLength(150, ErrorMessage ="* Cannot exceed 150 chars")]
        public string ProductName { get; set; }

        [Display(Name ="Product Description")]
        [DisplayFormat(NullDisplayText ="N/A")]
        public string ProductDescription { get; set; }

        [Display(Name ="Price")]
        [DisplayFormat(NullDisplayText ="N/A",ApplyFormatInEditMode = true)]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public Nullable<decimal> Price { get; set; }


        [Display(Name = "Qty in Stock")]
        [DisplayFormat(NullDisplayText = "N/A", ApplyFormatInEditMode = true)]
        public Nullable<short> UnitsInStock { get; set; }
        public string ProductImage { get; set; }

    }
}
