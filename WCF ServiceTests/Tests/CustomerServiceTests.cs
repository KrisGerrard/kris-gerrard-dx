using Microsoft.VisualStudio.TestTools.UnitTesting;
using WCF_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF_ServiceTests.CustomerServiceReference;
using WCF_Service.DataTransferObjects;
using WCF_ServiceTests.Models;

namespace WCF_Service.Tests
{
    // Test Class for WCF_Services - Testing using CustomerService Service Reference.
    //                               Tested with MSTESTv2 unit test framework v14.00
    //
    [TestClass()]
    public class CustomerServiceTests
    {
        private TestDBModel db; //EF Azure DB
        CustomerServiceClient client; //WCF

        // getAddressesByCustIDTest - tests the three possible types of input conditions, and quantity of addresses returned.
        //                            DataRows follow format ( Customer ID , Expected amount of Addresses returned )
        //
        [DataTestMethod()]
        [DataRow(0,0)] //Invalid customer ID, return empty list
        [DataRow(1,1)] //Known customer ID, return 1 address
        [DataRow(7000000,0)] //Unknown customer ID (does not exist), return empty 
        public void getAddressesByCustIDTest(int customerID, int actualCount)
        {
            //Arrange
            int resultCount;
            CustomerServiceClient client = new CustomerServiceClient();

            //Act
            resultCount = client.getAddressesByCustID(customerID).Count;

            //Assert
            Assert.AreEqual(actualCount, resultCount);
        }


        // getCustIDByUsername - tests the three possible types of input conditions vs Customer ID returned.
        //                       DataRows follow format ( Username , Customer ID )
        //
        [DataTestMethod()]
        [DataRow("")] //Invalid customer ID
        [DataRow("TerryMck")] //Known customer ID
        [DataRow("MaxwellSmart")] //Unknown customer ID (does not exist)
        public void getCustIDByUsernameTest(string username)
        {
            //Arrange
            int result,
                customerID;
            Customer customer;
            CustomerServiceClient client = new CustomerServiceClient();

            //Act
            //  Get customer ID directly from database
            if (String.IsNullOrEmpty(username)) //for null we expect 0 to be returned, and cannot directly query DB so must manually set this
            {
                customerID = 0;
            } else
            {
                db = new TestDBModel();
                customer = db.Customers.FirstOrDefault(c => c.UserName == username);
                db.Dispose();
                if (customer == null)
                {
                    customerID = 0;
                } else
                {
                    customerID = customer.CustomerID;
                }

            }
            //  Get customer ID from WCF Service
            result = client.getCustIDByUsername(username);

            //Assert
            Assert.AreEqual(result, customerID);
        }


        // createAddressTest - tests the three possible types of input variations of Customer ID's, 
        //                     and verifys data is written to DB. Deletes from DB when finished to tidy up.
        //                     DataRows follow format ( Username , Customer ID )
        [DataTestMethod()]
        [DataRow(0)] //Invalid customer ID
        [DataRow(2)] //Known customer ID
        [DataRow(7000000)] //Unknown customer ID (does not exist)
        public void createAddressTest(int customerID)
        {
            //Arrange
            int resultAddressID;
            int actualAddressID;
            client = new CustomerServiceClient();
            Customer customer;
            //  create test address
            string stamp = "WCF Service unit test - " + DateTime.Now;
            AddressDTO address = new AddressDTO();
            address.AddressType = 1;
            address.Country = stamp;
            address.City = stamp;
            address.Street = stamp;
            address.Suburb = stamp;

            //Act
            //  Test service and create new address
            resultAddressID = client.createAddress(address, customerID);
            //  query actual address ID
            db = new TestDBModel();
            customer = db.Customers.FirstOrDefault(c => c.CustomerID == customerID);
            if (customer == null) //determine if address should of been added
            {
                actualAddressID = 0;
            } else
            {
                //find new test address
                actualAddressID = customer.Addresses.FirstOrDefault(a => a.City == address.City &&
                                                                         a.Country == address.Country &&
                                                                         a.Street == address.Street &&
                                                                         a.Suburb == address.Suburb &&
                                                                         a.AddressType == address.AddressType).AddressID;
                //remove test address
                db.Addresses.Remove(db.Addresses.Find(actualAddressID));
                db.SaveChanges();
            }

            //Assert
            Assert.AreEqual(resultAddressID, actualAddressID);
        }


        // createCustomerTest - tests types of input variations of unique Username and verifys data. 
        //                      Deletes from DB when finished to tidy up.
        //                      DataRows format: ( Username )
        [DataTestMethod()]
        [DataRow(null)] //Invalid username
        [DataRow("")] //Invalid username
        [DataRow("BrianSnl")] //Existing username
        [DataRow("Unit_Test_User")] //New username
        public void createCustomerTest(string Username)
        {
            //Arrange
            client = new CustomerServiceClient();
            int resultID,
                actualID;
            //  Create new Customer
            Customer testCustomer = new Customer();
            testCustomer.UserName = Username;

            //Act
            //  Create Customer
            resultID = client.createCustomer(Username);
            //  Verify Customer is created
            db = new TestDBModel();
            if (String.IsNullOrEmpty(Username) || db.Customers.Where(c => c.UserName == Username).Count() != 0)
            {
                actualID = 0;
            }
            else
            {
                actualID = db.Customers.FirstOrDefault(c => c.UserName == Username).CustomerID;
                //Remove test user
                db.Customers.Remove(db.Customers.Find(actualID));
                db.SaveChanges();
            }
            //Assert
            //  Compare ID matches
            Assert.AreEqual(resultID, actualID);
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