using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCF_Service.Models;
using WCF_Service.DataTransferObjects;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrderService.svc or OrderService.svc.cs at the Solution Explorer and start debugging.
    public class OrderService : IOrderService
    {
        private OrderModel db = new OrderModel();


        //getTopSalesByCount - Returns ProductID for top sales by item (not by order), takes an integer to define result set size
        //                     If no sales are present will return an empty list
        //                     Throws FaultException on failure from EF.
        public List<int> getTopSalesByCount(int countReturned)
        {
            List<int> results;
            try
            {
                //create salesGroup from OrderDetails and then sum and sort by quantity
                results = (from salesOrders in db.OrderDetails
                           group new { salesOrders.ProductID, salesOrders.Quantity } by salesOrders.ProductID into salesGroup
                           orderby salesGroup.Sum(x => x.Quantity) descending
                           select salesGroup.Select(r => r.ProductID).FirstOrDefault()).Take(countReturned).ToList();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            return results;
        }


        //addOrder - Create a new order from OrderDTO.
        //           Throws FaultException on failure from EF, and on validation error.
        //       
        public void addOrder(OrderDTO newOrder)
        {
            bool customerExists;
            bool addressExists;
            //validate - null, address ID, and Customer ID
            if (newOrder == null)
            {
                throw new FaultException("Order cannot be null");
            }
            using (CustomerModel dbc = new CustomerModel())
            {
                customerExists = dbc.Customers.Where(c => c.CustomerID == newOrder.CustomerID).Count() > 0;
                addressExists = dbc.Addresses.Where(a => a.AddressID == newOrder.AddressID).Count() > 0;
            }
            if (!customerExists)
            {
                throw new FaultException("Cannot find customer");
            } else if (!addressExists)
            {
                throw new FaultException("Cannot find address");
            }
            //create Order
            Order order = new Order();
            order.AddressID = newOrder.AddressID;
            order.Complete = newOrder.Complete;
            order.CustomerID = newOrder.CustomerID;
            order.OrderDate = DateTime.Now;
            db.Orders.Add(order);
            //create Order Details
            foreach (OrderDetailItem newOrderDetail in newOrder.products)
            {
                OrderDetail orderdetail = new OrderDetail();
                orderdetail.OrderID = order.OrderID;
                orderdetail.Packaged = true;
                orderdetail.PackagedBy = 1;
                orderdetail.ProductID = newOrderDetail.ProductID;
                orderdetail.Quantity = newOrderDetail.Quantity;
                db.OrderDetails.Add(orderdetail);
            }
            //save
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }


        //getOrders - Return a list of OrderDTO's for a customerID
        //            Returns an empty list on no result.
        //            Throws FaultException on failure from EF. 
        public List<OrderDTO> getOrders(int customerID)
        {
            List<OrderDTO> orderList = new List<OrderDTO>();
            //find requested orders
            List<Order> list;
            try
            {
                list = db.Orders.Where(order => order.CustomerID == customerID).ToList();
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            //if no orders exist, return an empty list
            if (list.Count > 0)
            {
                foreach (Order order in list)
                {
                    //create order body
                    OrderDTO odto = new OrderDTO();
                    odto.OrderID = order.OrderID;
                    odto.CustomerID = order.CustomerID;
                    odto.AddressID = order.AddressID;
                    odto.Complete = order.Complete;
                    odto.OrderDate = order.OrderDate;
                    //loop through order details and create list of sub-items
                    List<OrderDetailItem> orderdetailList = new List<OrderDetailItem>();
                    foreach (OrderDetail orderdetail in db.OrderDetails.Where(od => od.OrderID == order.OrderID))
                    {
                        orderdetailList.Add(new OrderDetailItem(orderdetail.ProductID, orderdetail.Quantity));
                    }
                    odto.products = orderdetailList;
                    orderList.Add(odto);
                }
            }
            return orderList;
        }
        public void Dispose()
        {
            db.Dispose();
            db = null;
        }
    }
}
