using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Product Data Transfer Object
/// This class is used to wrap Product objects for user with the Product Service.
/// Constructor can use existing Product object to create equivilant ProductDTO.
/// </summary>
namespace WCF
{
    [DataContract]
    public class ProductDTO
    {
        public ProductDTO() { }
        public ProductDTO(Product product)
        {
            this.ProductID = product.ProductID;
            this.RetailerID = product.RetailerID;
            this.SubCategoryID = product.SubCategoryID;
            this.Name = product.ProductName;
            this.Description = product.ProductDescription;
            this.Price = product.Price;
            this.Stock = product.UnitsInStock;
            this.Picture = product.Picture;
            this.Discontinued = product.IsDiscontinued;
        }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int RetailerID { get; set; }

        [DataMember]
        public int SubCategoryID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int Stock { get; set; }

        [DataMember]
        public byte[] Picture { get; set; }

        [DataMember]
        public bool? Discontinued { get; set; }

    }    
}