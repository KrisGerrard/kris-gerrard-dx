using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Service.DataTransferObjects;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductDTO> GetProducts();

        [OperationContract]
        List<ProductDTO> GetProductsByCat(int CategoryId);

        [OperationContract]
        List<ProductDTO> GetProductsBySubCat(int SubCategoryId);

        [OperationContract]
        ProductDTO GetProductByID(int ProductId);

        [OperationContract]
        void AddProduct(ProductDTO product);

        [OperationContract]
        void EditProduct(ProductDTO product);

        [OperationContract]
        void DeleteProduct(ProductDTO product);

        [OperationContract]
        SubCategoryDTO GetSubCategoryByID(int SubCategoryId);

        [OperationContract]
        List<SubCategoryDTO> GetSubCategory();

        [OperationContract]
        List<ProductDTO> GetProductsByTopSales(int countReturned);
    }
}
