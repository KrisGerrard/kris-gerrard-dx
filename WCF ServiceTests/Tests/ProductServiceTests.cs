using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF_ServiceTests.Models;
using WCF_ServiceTests.ProductServiceReference;
using WCF_Service.DataTransferObjects;
using System.Linq;

namespace WCF_ServiceTests.Tests
{
    // Test Class for WCF_Services - Testing using ProductService Service Reference.
    //                               Tested with MSTESTv2 unit test framework v14.00
    //
    [TestClass]
    public class ProductServiceTests
    {
        private TestDBModel db; //EF Azure DB
        ProductServiceClient client; //WCF


        //GetProductByIDTest - Tests product returned is correct, and that invalid/unknown products return a productID of 0.
        //                     
        //
        [DataTestMethod()]
        [DataRow(0)] //Invalid product ID
        [DataRow(1)] //Known product ID
        [DataRow(7000000)] //Unknown product ID (does not exist)
        public void GetProductByIDTest(int ProductId)
        {
            //Arrange
            Product actualProd;
            ProductDTO testProd;
            client = new ProductServiceClient();
            db = new TestDBModel();
            if (db.Products.Where(p => p.ProductID == ProductId).Count() > 0)
            {
                actualProd = db.Products.FirstOrDefault(p => p.ProductID == ProductId);
            } else
            {
                actualProd = new Product();
                actualProd.ProductID = 0;
            }
            //Act
            testProd = client.GetProductByID(ProductId);
            //Assert
            if (actualProd.ProductID != 0)
            {
                Assert.AreEqual(actualProd.ProductName, testProd.Name);
                Assert.AreEqual(actualProd.Price, testProd.Price);
                Assert.AreEqual(actualProd.SubCategoryID, testProd.SubCategoryID);
                Assert.AreEqual(actualProd.UnitsInStock, testProd.Stock);
                Assert.AreEqual(actualProd.ProductDescription, testProd.Description);
                Assert.AreEqual(actualProd.IsDiscontinued, testProd.Discontinued);
            }
            Assert.AreEqual(actualProd.ProductID, testProd.ProductID);
        }


        //GetSubCategoryByIDTest - Tests SubCategory returned is correct, and that invalid/unknown SubCategory return a productID of 0.
        //                     
        //
        [DataTestMethod()]
        [DataRow(0)] //Invalid Sub Category ID
        [DataRow(1)] //Known Sub Category ID
        [DataRow(7000000)] //Unknown Sub Category ID (does not exist)
        public void GetSubCategoryByIDTest(int SubCategoryID)
        {
            //Arrange
            ProductSubCategory actualCat;
            SubCategoryDTO testCat;
            client = new ProductServiceClient();
            db = new TestDBModel();
            if (db.ProductSubCategories.Where(p => p.SubCategoryID == SubCategoryID).Count() > 0)
            {
                actualCat = db.ProductSubCategories.FirstOrDefault(p => p.SubCategoryID == SubCategoryID);
            } else
            {
                actualCat = new ProductSubCategory();
                actualCat.SubCategoryID = 0;
            }

            //Act
            testCat = client.GetSubCategoryByID(SubCategoryID);

            //Assert
            if (actualCat.SubCategoryID != 0)
            {
                Assert.AreEqual(actualCat.SubCategory, testCat.SubCategoryName);
                Assert.AreEqual(actualCat.CategoryID, testCat.ParentCategoryID);
                Assert.AreEqual(actualCat.ProductCategory.Category, testCat.ParentCategoryName);
            }
            Assert.AreEqual(actualCat.SubCategoryID, testCat.SubCategoryID);
        }

        //GetProductsTest - Tests count of products returned when all are requested
        //                     
        //
        [TestMethod]
        public void GetProductsTest()
        {
            //Arrange
            int testQty, actualQty;
            client = new ProductServiceClient();
            db = new TestDBModel();
            //Act
            actualQty = db.Products.Count();
            testQty = client.GetProducts().Count();
            //Assert
            Assert.AreEqual(actualQty, testQty);
        }


        //GetProductsByCatTest - Tests count of Products returned in Category is correct, and that invalid/unknown products return an empty list.
        //                     
        //
        [DataTestMethod()]
        [DataRow(0)] //Invalid Category ID
        [DataRow(3)] //Known Category ID
        [DataRow(7000000)] //Unknown Category ID (does not exist)
        public void GetProductsByCatTest(int CategoryId)
        {
            //Arrange
            int testQty, actualQty;
            client = new ProductServiceClient();
            db = new TestDBModel();
            //Act
            actualQty = db.Products.Where(p => p.ProductSubCategory.CategoryID == CategoryId).Count();
            testQty = client.GetProductsByCat(CategoryId).Count();
            //Assert
            Assert.AreEqual(actualQty, testQty);
        }


        //GetProductsBySubCatTest - Tests count of Products returned in SubCategory is correct, and that invalid/unknown products return an empty list.
        //                     
        //
        [DataTestMethod()]
        [DataRow(0)] //Invalid SubCategory ID
        [DataRow(3)] //Known SubCategory ID
        [DataRow(7000000)] //Unknown SubCategory ID (does not exist)
        public void GetProductsBySubCatTest(int SubCategoryId)
        {
            //Arrange
            int testQty, actualQty;
            client = new ProductServiceClient();
            db = new TestDBModel();
            //Act
            actualQty = db.Products.Where(p => p.SubCategoryID == SubCategoryId).Count();
            testQty = client.GetProductsBySubCat(SubCategoryId).Count();
            //Assert
            Assert.AreEqual(actualQty, testQty);
        }

        //GetSubCategoryTest - Tests count of SubCategory returned when all are requested
        //                     
        //
        [TestMethod]
        public void GetSubCategoryTest()
        {
            //Arrange
            int testQty, actualQty;
            client = new ProductServiceClient();
            db = new TestDBModel();
            //Act
            actualQty = db.ProductSubCategories.Count();
            testQty = client.GetSubCategory().Count();
            //Assert
            Assert.AreEqual(actualQty, testQty);
        }


        // AddProductTest - Tests a new product can be created, and verifies product details.
        //                  When complete deletes product created for testing.
        //
        [DataTestMethod()]
        [DataRow(0, "Unit Test")] //Invalid SubCategory ID
        [DataRow(3, "Unit Test")] //OK
        [DataRow(7000000, "Unit Test")] //Unknown SubCategory ID (does not exist)
        [DataRow(3, "")] //Invalid Name
        public void AddProductTest(int SubCategory, string Name)
        {
            //Arrange 
            client = new ProductServiceClient();
            db = new TestDBModel();
            Product testProduct = new Product();
            Exception expectedException = new Exception();
            //  Create Product DTO
            ProductDTO product = new ProductDTO();
            product.Name = Name;
            product.Discontinued = true;
            product.Description = "Unit Test on " + DateTime.Now;
            product.Price = 100;
            product.RetailerID = 1;
            product.Stock = 1;
            product.SubCategoryID = SubCategory;

            //Act
            //  create product
            try
            {
                client.AddProduct(product);
            } catch (Exception e)
            {
                expectedException = e;
            }
            //  verify exists
            var result = db.Products.Where(p => p.ProductName == product.Name &&
                                            p.IsDiscontinued == product.Discontinued &&
                                            p.ProductDescription == product.Description &&
                                            p.Price == product.Price &&
                                            p.RetailerID == product.RetailerID &&
                                            p.UnitsInStock == product.Stock &&
                                            p.SubCategoryID == product.SubCategoryID);
            //Assert
            if (result.Count() > 0) //Product has been created
            {
                testProduct = result.FirstOrDefault();
                //tidy up and delete product
                db.Products.Remove(testProduct);
                db.SaveChanges();
                //Assert
                Assert.AreEqual(Name, testProduct.ProductName);
                Assert.AreEqual(SubCategory, testProduct.SubCategoryID);
            } else //Product has not been created, test there is an exception explaining why
            {
                Assert.IsNotNull(expectedException);
            }         
        }


        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
            if (client != null)
            {
                client.Close();
                client = null;
            }
        }
    }
}
