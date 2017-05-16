using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WCF_Service.Models;

/// SubCategory Data Transfer Object
/// This class is used to wrap Address objects for user with the Product Service.
/// Constructor uses existing SubCategory object to create equivilant SubCategoryDTO.
/// </summary>
namespace WCF_Service.DataTransferObjects
{
    [DataContract]
    public class SubCategoryDTO
    {
        [DataMember]
        public int SubCategoryID { get; set; }

        [DataMember]
        public int ParentCategoryID { get; set; }

        [DataMember]
        public string SubCategoryName { get; set; }

        [DataMember]
        public string ParentCategoryName { get; set; }

        public SubCategoryDTO(ProductSubCategory productSubCategory)
        {
            this.SubCategoryID = productSubCategory.SubCategoryID;
            this.ParentCategoryID = productSubCategory.CategoryID;
            this.SubCategoryName = productSubCategory.SubCategory;
            this.ParentCategoryName = productSubCategory.ProductCategory.Category;
        }

        public SubCategoryDTO() { }
    }
}