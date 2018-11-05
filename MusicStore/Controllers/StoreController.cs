using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Reference Models
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreModel db = new MusicStoreModel();
        // GET: Store
        public ActionResult Index()
        {
            var genres = db.Genres.OrderBy(g => g.Name).ToList();
            return View(genres);
        }

        public ActionResult Product(string ProductName)
        {
            ViewBag.ProductName = ProductName;
            return View();
        }

        //Get: store/Albums
        public ActionResult Albums(String genre)
        {
            //mock up some album data
            //var albums = new List<Album>();

            //            for (int i=1; i<10; i++)
            //          {
            //            albums.Add(new Album { Title = "Album" + i.ToString() });
            //      }
            var albums = db.Albums.Where(a => a.Genre.Name == genre).OrderBy(a => a.Title).ToList();
            ViewBag.Genre = genre;
            return View(albums);
        }

        //Get: Store/AddToCart/5
        public ActionResult AddToCart(int AlbumId)
        {
            GetCartId();
            string CurrentCartId = Session["CartId"].ToString(); 
            Cart cartItem = new Cart
            {
                AlbumId = AlbumId,
                Count = 1,
                DateCreated = DateTime.Now,
                CartId = CurrentCartId
            };

            db.Carts.Add(cartItem);
            db.SaveChanges();

            //show cart page
            return RedirectToAction("ShoppingCart");
        }

        private void GetCartId()
        {
            //is there already a cart id
            if(Session["CartId"] == null)
            {
                //is user loggedin?
                if (User.Identity.Name == "") {
                    //generate unique id that is session specific
                    Session["CartId"] = Guid.NewGuid().ToString();
                }
                //if user is not logged in
                else
                {
                    Session["CartId"] = User.Identity.Name;
                }
            }
        }

        //GET: Store/ShoppingCart

        public ActionResult ShoppingCart()
        {
            //get current Cart for display
            GetCartId();
            string CurrentCartId = Session["CartId"].ToString();
            var cartItems = db.Carts.Where(c => c.CartId == CurrentCartId);
            //load view and pass the list of items in the users cart
            return View(cartItems);
        }

        //GET: Store/RemoveFromCart/4
        public ActionResult RemoveFromCart(int RecordId)
        {
            //remove item from users cart
            Cart CartItem = db.Carts.SingleOrDefault(c => c.RecordId == RecordId);
            db.Carts.Remove(CartItem);
            db.SaveChanges();

            //reload cart page
            return RedirectToAction("ShoppingCart");
        }
    }
}