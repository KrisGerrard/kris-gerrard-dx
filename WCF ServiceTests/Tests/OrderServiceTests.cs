using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF_ServiceTests.Models;
using WCF_ServiceTests.OrderServiceReference;
using WCF_Service.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace WCF_ServiceTests.Tests
{
    // Test Class for WCF_Services - Testing using OrderService Service Reference.
    //                               Tested with MSTESTv2 unit test framework v14.00
    //
    [TestClass]
    public class OrderServiceTests
    {
        private TestDBModel db; //EF Azure DB
        OrderServiceClient client; //WCF


        // getOrdersTest - Tests correct number of orders are received per customer ID
        //                 Datarow format: ( Customer ID )
        //                 
        [DataTestMethod()]
        [DataRow(0)] //Invalid customer ID, return empty list
        [DataRow(1)] //Known customer ID, return 1 address
        [DataRow(7000000)] //Unknown customer ID (does not exist), return empty 
        public void getOrdersTest(int customerID)
        {
            //Arrange
            int testOrdersQty, actualOrdersQty;
            client = new OrderServiceClient();
            db = new TestDBModel();
            //Act
            actualOrdersQty = db.Orders.Where(o => o.CustomerID == customerID).Count();
            testOrdersQty = client.getOrders(customerID).Count();
            //Assert
            Assert.AreEqual(actualOrdersQty, testOrdersQty);
        }


        // addOrderTest - Tests orders are either created, or a fault exception is thrown.
        //                After verifying order was created, order is removed.
        //                Datarow format: ( Address ID , Customer ID )
        [DataTestMethod]
        [DataRow(3,4)] // OK
        [DataRow(0, 1)] // Invalid Address ID
        [DataRow(1, 0)] // Invalid Customer ID
        [DataRow(7000, 2)] // Unknown Address ID
        [DataRow(2, 7000)] // Unknown Customer ID
        public void addOrderTest(int AddressID, int CustomerID)
        {
            //Arrange
            client = new OrderServiceClient();
            db = new TestDBModel();
            Order testOrder = new Order();
            Exception expectedException = new Exception();
            //  Create new order
            List<WCF_Service.Models.OrderDetailItem> detailList = new List<WCF_Service.Models.OrderDetailItem>();
            //detailList.Add(new WCF_Service.Models.OrderDetailItem(1,1));
            OrderDTO orderDto = new OrderDTO();
            orderDto.AddressID = AddressID;
            orderDto.Complete = true;
            orderDto.CustomerID = CustomerID;
            orderDto.OrderDate = DateTime.Now;
            orderDto.products = detailList;

            //Act
            //  Create new order
            try
            {
                client.addOrder(orderDto);
            } catch (Exception e)
            {
                e = expectedException;
            }
            //  Verify order is created
            var result = db.Orders.Where(o => o.OrderDate == orderDto.OrderDate &&
                                              o.AddressID == orderDto.AddressID &&
                                              o.CustomerID == orderDto.CustomerID).ToList();
            if (result.Count() > 0) //Order was created
            {
                testOrder = result.FirstOrDefault();
                //Delete order
                //db.OrderDetails.Remove(testOrder.OrderDetails.FirstOrDefault());
                db.Orders.Remove(testOrder);
                db.SaveChanges();
                //Assert
                Assert.AreEqual(AddressID, testOrder.AddressID);
                Assert.AreEqual(CustomerID, testOrder.CustomerID);
            }
            else //Order was not created, test there is an exception explaining why
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
