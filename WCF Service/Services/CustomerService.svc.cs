using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCF_Service.Models;
using WCF_Service.DataTransferObjects;

namespace WCF_Service
{
    public class CustomerService : ICustomerService
    {
        private CustomerModel db = new CustomerModel();

        // createCustomer - Used to create a new customer from Username and return Customer ID. 
        //                  Returns 0 on business validation error.
        //                  Throws FaultException on failure from EF.
        public int createCustomer(string Username)
        {
            // Validate - business logic requires this to be a unique name
            if (String.IsNullOrEmpty(Username) || db.Customers.Where(c => c.UserName == Username).Count() != 0)
            {
                return 0;
            }

            // Save to DB
            Customer customer = new Customer();
            customer.UserName = Username;
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }

            // Customer has been succesfully added, so return ID
            return db.Customers.Where(cust => cust.UserName == Username).FirstOrDefault().CustomerID;
        }

        // createAddress - Used to create a new address from AddressDTO and Customer ID, returns Address ID.
        //                 Returns 0 on business validation error.
        //                 Throws FaultException on failure from EF.
        public int createAddress(AddressDTO newAddress, int customerID)
        {
            // Validate - Address cannot be null, and customer must exist, for an address to be created.
            if (newAddress == null || db.Customers.Where(c => c.CustomerID == customerID).Count() < 1)
            {
                return 0;
            }

            //create new address, and populate from DTO
            Address address = new Address();
            address.Street = newAddress.Street;
            address.Suburb = newAddress.Suburb;
            address.City = newAddress.City;
            address.PostalCode = newAddress.PostalCode;
            address.Country = newAddress.Country;
            address.AddressType = newAddress.AddressType;
            address.AddressType1 = db.AddressTypes.Find(address.AddressType);
            address.Customers.Add(db.Customers.Find(customerID));

            //save
            try
            {
                db.Addresses.Add(address);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }

            //address succesfully added, return ID
            return db.Addresses.Where(a => a.City == address.City &&
                                           a.Country == address.Country &&
                                           a.Street == address.Street &&
                                           a.Suburb == address.Suburb &&
                                           a.AddressType == address.AddressType).FirstOrDefault().AddressID;
        }


        // getAddressesByCustID - Get customer addresse(s) from ID. 
        //                        Returns empty list if no results.
        //                        Throws FaultException on failure from EF.
        public List<AddressDTO> getAddressesByCustID(int customerID)
        {
            List<AddressDTO> addressList = new List<AddressDTO>();
            List<Address> list = new List<Address>();
            Customer customer;
            try
            {
                customer = db.Customers.FirstOrDefault(cust => cust.CustomerID == customerID);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            if (customer != null)
            {
                list = customer.Addresses.ToList();
            }
            foreach (Address address in list)
            {
                addressList.Add(new AddressDTO(address));
            }
            return addressList;
        }


        // getCustIDByUsername - Return User ID when queried by Username
        //                       Returns zero on no match
        //                       Throws FaultException on failure from EF.
        public int getCustIDByUsername(string username)
        {
            int id;
            Customer customer;
            if (string.IsNullOrEmpty(username))
            {
                return 0;
            }
            try
            {
                customer = db.Customers.FirstOrDefault(cust => cust.UserName == username);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            if (customer == null)
            {
                id = 0;
            }
            else
            {
                id = customer.CustomerID;
            }
            return id;
        }

        public void Dispose()
        {
            db.Dispose();
            db = null;
        }
    }
}
