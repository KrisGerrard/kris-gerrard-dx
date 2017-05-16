using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebStore.CustomerServiceReference;
using WebStore.Models;
using WebStore.OrderServiceReference;

namespace WebStore.Controllers
{
    public class CustomersController : Controller
    {
        CustomerServiceClient client = new CustomerServiceClient();

        // GET: Customers/ShowAddress
        // Partial view of addresses for customer and display on model
        [Authorize]
        public ActionResult _ShowAddress()
        {
            return PartialView(client.getAddressesByCustID((int)Session["customerIDVar"]));
        }

        // POST: Customers/AddAddress
        public void AddAddress(AddressDTO address)
        {
            client.createAddress(address, (int)Session["customerIDVar"]);
        }

        // GET: Customers/Checkout
        [Authorize]
        public ActionResult CheckOut()
        {
            string userName;

            // retreive customer ID
            int CustomerID;
            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
                CustomerID = client.getCustIDByUsername(userName);
            }
            else
            {
                throw new NotSupportedException("You must be logged in to access this page");
            }

            //Create customer if they do not exist in DB
            if (CustomerID == 0)
            {
                CustomerID = client.createCustomer(userName);
            }

            //Store Customer ID for use when creating order
            Session["customerIDVar"] = CustomerID;

            //Pass shopping cart to view
            return View(Session["shoppingCartListVar"] as List<ShoppingCartItem>);
        }


        // POST Customers/Checkout
        [HttpPost]
        [Authorize]
        public void Checkout(int addressID)
        {
            bool backorder = false;
            OrderDTO order = new OrderDTO();
            order.AddressID = addressID;
            order.CustomerID = (int)Session["customerIDVar"];
            order.OrderDate = DateTime.Now;
            List<OrderDetailItem> list = new List<OrderDetailItem>();
            foreach (ShoppingCartItem item in (Session["shoppingCartListVar"] as List<ShoppingCartItem>))
            {
                if (item.onBackOrder == true)
                {
                    backorder = true;
                }
                OrderDetailItem orderdetail = new OrderDetailItem();
                orderdetail.ProductID = item.ProductID;
                orderdetail.Quantity = item.Quantity;
                list.Add(orderdetail);
            }
            order.products = list.ToArray();
            if (backorder)
            {
                order.Complete = false;
            }
            else
            {
                order.Complete = true;
            }
            // place order
            using (OrderServiceClient client = new OrderServiceClient())
            {
                client.addOrder(order);
            }
            // reset shopping cart
            Session["shoppingCartListVar"] = new List<ShoppingCartItem>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                client.Close();
                client = null;
            }
            base.Dispose(disposing);
        }
    }
}