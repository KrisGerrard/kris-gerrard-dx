using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.CustomerServiceReference;
using WebStore.Models;
using WebStore.OrderServiceReference;

namespace WebStore.Controllers
{
    public class OrdersController : Controller
    {
        // POST: Orders/AddToCart
        // Add item to cart, update cart items in viewbag, and return true if on backorder
        [HttpPost]
        public string AddToCart(int ProductID, int Quantity, double Price, string ProductName, byte[] Picture, bool BackOrder)
        {
            List<ShoppingCartItem> shoppingCartList;
            ShoppingCartItem newItem = new ShoppingCartItem(ProductID, Quantity, Price, ProductName, Picture, BackOrder);
            ShoppingCartItem existingEntry = null;

            //if existing items are saved in session storage retreive, else create new list
            if (Session["shoppingCartListVar"] != null)
            {
                shoppingCartList = (List<ShoppingCartItem>)Session["shoppingCartListVar"];
                existingEntry = shoppingCartList.FirstOrDefault(item => item.ProductID == ProductID);
            }
            else
            {
                shoppingCartList = new List<ShoppingCartItem>();
            }

            //add new items to cart
            if (existingEntry == null)
            {
                shoppingCartList.Add(newItem);
            }
            else
            {
                shoppingCartList.First(item => item.ProductID == ProductID).Quantity += Quantity;
            }

            //save shopping cart back to session state
            Session["shoppingCartListVar"] = shoppingCartList;

            //update shopping cart message on menu
            double TotalCost = shoppingCartList.Sum(od => (od.Price * od.Quantity));
            int TotalItems = shoppingCartList.Sum(od => od.Quantity);
            string Message = "  $" + TotalCost + " (" + TotalItems + " items)";
            Session["shoppingCartMessage"] = Message;

            //return message to display on live page
            return Message;
        }

        // GET: Orders/ShoppingCart
        public ActionResult ShoppingCart()
        {
            ViewBag.content = true;
            if ((Session["shoppingCartListVar"] as List<ShoppingCartItem>) == null)
            {
                ViewBag.content = false;
            }
            return View();
        }

        // GET: Orders/_ShoppingCartList
        public ActionResult _ShoppingCartList()
        {
            List<ShoppingCartItem> cart = Session["shoppingCartListVar"] as List<ShoppingCartItem>;
            if (cart != null)
            {
                ViewBag.Sum = cart.Sum(c => (c.Price * c.Quantity));
            } else
            {
                ViewBag.Sum = '0';
            }
            return PartialView(cart);
        }

        // POST: Orders/UpdateCart
        [HttpPost]
        public string UpdateCart(int ProductID, int Quantity)
        {
            string message = Session["shoppingCartMessage"] as string;
            List<ShoppingCartItem> cart = Session["shoppingCartListVar"] as List<ShoppingCartItem>;
            if (Quantity > 0)
            {
                cart.Where(c => c.ProductID == ProductID).FirstOrDefault().Quantity = Quantity;
            }
            else
            {
                if (cart != null && cart.Count > 1)
                {
                    cart.Remove(cart.Where(c => c.ProductID == ProductID).FirstOrDefault());
                } else
                {
                    cart = null;
                    message = "  Your Shopping Cart is empty";
                }
            }
            if (cart != null)
            {
                //update shopping cart message on menu
                double TotalCost = cart.Sum(od => (od.Price * od.Quantity));
                int TotalItems = cart.Sum(od => od.Quantity);
                message = "  $" + TotalCost + " (" + TotalItems + " items)";
            }
            Session["shoppingCartListVar"] = cart;
            Session["shoppingCartMessage"] = message;
            return message;
        }


        // GET: /Orders/GetOrders
        [Authorize]
        public ActionResult GetOrders()
        {
            //Get Customer ID
            if (Session["customerIDVar"] == null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    using (CustomerServiceClient CustomerClient = new CustomerServiceClient())
                    {
                        Session["customerIDVar"] = CustomerClient.getCustIDByUsername(User.Identity.Name);
                        CustomerClient.Close();
                    }
                }
                else
                {
                    throw new NotSupportedException("You must be logged in to access this page");
                }
            }
            int CustomerID = (int)Session["customerIDVar"];

            //Get customer Order list
            List<OrderDTO> orders;
            using (OrderServiceClient client = new OrderServiceClient())
            {
                orders = client.getOrders(CustomerID).ToList();
                client.Close();
            }
            return View(orders);
        }

    }
}