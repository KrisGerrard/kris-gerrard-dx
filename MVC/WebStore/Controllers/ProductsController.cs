using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebStore.Models;
using WebStore.ProductServiceReference;

namespace WebStore.Controllers
{
    public class ProductsController : Controller
    {
        ProductServiceClient client = new ProductServiceClient();

        // GET: Top Sellers
        public ActionResult Home()
        {
            ViewBag.Title = "Top Selling Products";
            return View("Index", client.GetProductsByTopSales(5));
        }

        // GET: Products
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View(client.GetProducts());
        }

        public ActionResult ProductsBySubCat(int id)
        {
            return View("Index", client.GetProductsBySubCat(id));
        }

        public ActionResult ProductsByCat(int id)
        {
            return View("Index", client.GetProductsByCat(id));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO product = client.GetProductByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.ProductSubcategoryID = new SelectList(client.GetSubCategory(), "ProductSubcategoryID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "ProductID")] ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                client.AddProduct(product);
                return RedirectToAction("Home");
            }

            //ViewBag.ProductSubcategoryID = new SelectList(client.GetSubCategory(), "ProductSubcategoryID", "Name", product.SubCategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO product = client.GetProductByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProductSubcategoryID = new SelectList(client.GetSubCategory(), "ProductSubcategoryID", "Name", product.SubCategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                client.EditProduct(product);
                return RedirectToAction("Details", "Products", new { id = product.ProductID });
            }
            //ViewBag.ProductSubcategoryID = new SelectList(client.GetSubCategory(), "ProductSubcategoryID", "Name", product.SubCategoryID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductDTO product = client.GetProductByID((int)id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductDTO product = client.GetProductByID(id);
            client.DeleteProduct(product);
            return RedirectToAction("Home");
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

        // GET: Partial View for Layout Sidebar, with menu categories generated from DB
        [ChildActionOnly]
        public ActionResult _MenuPartial()
        {
            //get list of all Subcategorys
            var SubCategorys = client.GetSubCategory();
            var Categorys = (from SC in SubCategorys
                             select new { SC.ParentCategoryID, SC.ParentCategoryName }).Distinct();
            
            //create menu item
            Menu _menu = new Menu();

            //for each categories
            foreach (var Category in Categorys)
            {
                //create MenuItem
                var _menuItem = new MenuItem();
                _menuItem.MenuItemName = Category.ParentCategoryName;
                _menuItem.MenuItemId = Category.ParentCategoryID;

                //for each sub category that matches category
                foreach (var ChildCategory in SubCategorys)
                {
                    if (ChildCategory.ParentCategoryID == _menuItem.MenuItemId)
                    {
                        //add childeren to menu item
                        var menuItem = new MenuItem();
                        menuItem.MenuItemName = ChildCategory.SubCategoryName;
                        menuItem.MenuItemId = ChildCategory.SubCategoryID;
                        _menuItem.ChildMenuItems.Add(menuItem);
                    }
                }
                //add MenuItem to menu
                _menu.Items.Add(_menuItem);
            }

            //populate viewbag for shopping cart message on menu nav
            ViewBag.ShoppingCartMessage = Session["shoppingCartMessage"];

            return PartialView("_MenuPartial", _menu);
        }

    }
}
