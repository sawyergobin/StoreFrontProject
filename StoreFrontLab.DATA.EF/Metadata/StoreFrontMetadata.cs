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
        [Required(ErrorMessage ="*Category ID is required")]
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "*Category Name is required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
    #endregion

    #region Product
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product { }

    public class ProductMetaData
    {
        //TODO Set the further specs of what these need to be
        [Required(ErrorMessage = "*Product ID is required")]
        [Display(Name = "Unique Product ID")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "*Product Name is required")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*A Description is required")]
        [Display(Name = "Product Description")]
        [UIHint("MultilineText")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*Category ID is required")]
        [Display(Name = "Product Status ID")]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "*Product Status ID is required")]
        [Display(Name = "Product Status ID")]
        public int ProductStatusID { get; set; }

        [Required(ErrorMessage = "*Calorie Count is required")]
        [Display(Name = "Product Calorie Count")]
        public short CalorieCount { get; set; }

        [Required(ErrorMessage = "*A Price is required")]
        public decimal Price { get; set; }

        //No validation on boolean values
        [Display(Name = "Is Product On Sale?")]
        public bool IsOnSale { get; set; }

        [DisplayFormat(NullDisplayText = "00")]
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
        [Required(ErrorMessage = "*Product Status ID is required")]
        [Display(Name = "Product Status ID")]
        public int ProductStatusID { get; set; }

        [Required(ErrorMessage = "*Product Status Name is required")]
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }

    }
    #endregion

}
