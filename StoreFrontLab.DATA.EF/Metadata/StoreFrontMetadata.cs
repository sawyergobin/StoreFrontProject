using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF//.Metadata = namespace matching the original classes to allow partial views to function
{

    #region Category
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category { }

    public class CategoryMetaData
    {
        //public int CategoryID { get; set; }
        [Required(ErrorMessage = "*Category Name is required")]
        [Display(Name = "Category Name")]
        [StringLength(30, ErrorMessage ="*Must not be more than 30 characters")]
        public string CategoryName { get; set; }
    }
    #endregion

    #region Product
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product { }

    public class ProductMetaData
    {
        //public int ProductID { get; set; }

        [Required(ErrorMessage = "*Product Name is required")]
        [Display(Name = "Product Name")]
        [StringLength(30, ErrorMessage = "*Must not be more than 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*A Description is required")]
        [Display(Name = "Product Description")]
        [UIHint("MultilineText")]
        [StringLength(300, ErrorMessage = "*Must not be more than 300 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*Category ID is required")]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*Product Status ID is required")]
        [Display(Name = "Product Status")]
        public int ProductStatusID { get; set; }

        [Required(ErrorMessage = "*Calorie Count is required")]
        [Display(Name = "Calorie Count")]
        public short CalorieCount { get; set; }

        [Required(ErrorMessage = "*A Price is required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

        //No validation on boolean values
        [Display(Name = "Is Product On Sale?")]
        public bool IsOnSale { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public Nullable<byte> DiscountPercentage { get; set; }

        [Required(ErrorMessage = "*A Product Image is required")]
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
        
    }
    #endregion

    #region ProductStatus
    [MetadataType(typeof(ProductStatusMetaData))]
    public partial class ProductStatus { }

    public class ProductStatusMetaData
    {
        //public int ProductStatusID { get; set; }

        [Required(ErrorMessage = "*Product Status Name is required")]
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

    }
    #endregion

}
