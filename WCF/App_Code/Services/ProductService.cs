using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCF;

// ProductService - Product and (sub)category service
// 
//
public class ProductService : IProductService
{
    private ProductModel db = new ProductModel();

    //GetProducts - Returns a list of ProductDTOs for every item
    //              If no products are present will return an empty list
    //              Throws FaultException on failure from EF.
    public List<ProductDTO> GetProducts()
    {
        List<ProductDTO> list;
        try
        {
            list = ProductListConverter(db.Products.ToList());
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        return list;
    }


    //GetProductsByCat - Returns a list of ProductDTOs for every item in a Category
    //                   If no products are present will return an empty list
    //                   Throws FaultException on failure from EF.
    public List<ProductDTO> GetProductsByCat(int CategoryId)
    {
        List<ProductDTO> list;
        try
        {
            list = ProductListConverter((from product in db.Products
                                         where product.ProductSubCategory.CategoryID == CategoryId
                                         select product).ToList());
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        return list;
    }


    //GetProductsBySubCat - Returns a list of ProductDTOs for every item in a Sub-Category
    //                      If no products are present will return an empty list
    //                      Throws FaultException on failure from EF.
    public List<ProductDTO> GetProductsBySubCat(int SubCategoryId)
    {
        List<ProductDTO> list;
        try
        {
            list = ProductListConverter((from product in db.Products
                                         where product.SubCategoryID == SubCategoryId
                                         select product).ToList());
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        return list;
    }


    //GetProductByID - Returns a ProductDTO for a ProductID
    //                 If no product is found returns a ProductDTO with ProductID of 0
    //                 Throws FaultException on failure from EF.
    public ProductDTO GetProductByID(int ProductId)
    {
        ProductDTO result;
        try
        {
            result = new ProductDTO((from product in db.Products
                                     where product.ProductID == ProductId
                                     select product).FirstOrDefault());
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        if (result == null)
        {
            result.ProductID = 0;
        }
        return result;
    }


    //GetSubCategoryByID - Returns a SubCategoryDTO for a SubCategoryID
    //                     If no Sub-Category is found returns a SubCategoryID with SubCategoryID of 0
    //                     Throws FaultException on failure from EF.
    public SubCategoryDTO GetSubCategoryByID(int SubCategoryID)
    {
        SubCategoryDTO results;
        try
        {
            results = new SubCategoryDTO((from subCategory in db.ProductSubCategories
                                          where subCategory.SubCategoryID == SubCategoryID
                                          select subCategory).FirstOrDefault());
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        return results;
    }


    //GetSubCategory - Returns a list of SubCategoryDTOs for all SubCategorys
    //                 If no Sub-Categorys exist returns an empty list
    //                 Throws FaultException on failure from EF.
    public List<SubCategoryDTO> GetSubCategory()
    {
        List<ProductSubCategory> results;
        List<SubCategoryDTO> SubCatDTOList = new List<SubCategoryDTO>();
        try
        {
            results = db.ProductSubCategories.ToList();
        }
        catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        foreach (ProductSubCategory subCategory in results)
        {
            SubCatDTOList.Add(new SubCategoryDTO(subCategory));
        }
        return SubCatDTOList;
    }


    //GetProductsByTopSales - Returns a list of ProductDTOs for Top selling products, length of list passed as arguement
    //                        If no sales exist, returns an empty list
    //                        Throws FaultException on failure from EF
    public List<ProductDTO> GetProductsByTopSales(int countReturned)
    {
        List<ProductDTO> prodDTOList = new List<ProductDTO>();
        try
        {
            OrderService orderService = new OrderService();
            foreach (int productID in orderService.getTopSalesByCount(countReturned))
            {
                prodDTOList.Add(new ProductDTO(db.Products.Find(productID)));
            }
        } catch (Exception e)
        {
            throw new FaultException(e.Message);
        }
        return prodDTOList;
    }


    // AddProduct - Create a new product
    //              Check product is not null
    //              Throws FaultException on failure from EF
    public void AddProduct(ProductDTO product)
    {
        if (product != null)
        {
            try
            {
                db.Products.Add(ProductDTOConverter(product, new Product()));
                db.SaveChanges();
            } catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        } else
        {
            throw new FaultException("Cannot create an empty Product");
        }
    }


    // EditProduct - Edit existing product
    //               Check product is not null
    //               Throws FaultException on failure from EF
    public void EditProduct(ProductDTO product)
    {
        if (product != null)
        {
            try
            {
                db.Entry(ProductDTOConverter(product, db.Products.Find(product.ProductID))).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
        else
        {
            throw new FaultException("Cannot edit null Product");
        }
    }


    // DeleteProduct - Delete existing product
    //                 Check product is not null
    //                 Throws FaultException on failure from EF
    public void DeleteProduct(ProductDTO product)
    {
        if (product != null)
        {
            try
            {
                db.Products.Remove(db.Products.Find(product.ProductID));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
        else
        {
            throw new FaultException("Cannot delete null Product");
        }
    }


    // ProductListConverter - Converts a list of Products to a list of ProductDTOs.
    //                        Used to prepare Entity object for WCF service.
    //                        Returns an empty list if passed list is empy.
    private List<ProductDTO> ProductListConverter(List<Product> productList)
    {
        List<ProductDTO> prodEntList = new List<ProductDTO>();
        foreach (Product p in productList)
        {
            prodEntList.Add(new ProductDTO(p));
        }
        return prodEntList;
    }


    // ProductDTOConverter - Covert a ProductDTO to a Product
    //                       Used when receiving a new or updated ProductDTO from client
    //                       Throws FaultException on Null product as arguement
    private Product ProductDTOConverter(ProductDTO product, Product targetProduct)
    {
        try
        {
            targetProduct.RetailerID = product.RetailerID;
            targetProduct.SubCategoryID = product.SubCategoryID;
            targetProduct.ProductName = product.Name;
            targetProduct.ProductDescription = product.Description;
            targetProduct.Price = product.Price;
            targetProduct.UnitsInStock = product.Stock;
            targetProduct.Picture = product.Picture;
            targetProduct.IsDiscontinued = product.Discontinued;
            targetProduct.ProductSubCategory = db.ProductSubCategories.Find(product.SubCategoryID);
        } catch (NullReferenceException)
        {
            throw new FaultException("Product cannot be Null");
        }
        return targetProduct;
    }

    public void Dispose()
    {
        db.Dispose();
        db = null;
    }

}
