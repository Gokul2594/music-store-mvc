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
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Product(string ProductName)
        {
            ViewBag.ProductName = ProductName;
            return View();
        }

        //Get: store/Albums
        public ActionResult Albums()
        {
            //mock up some album data
            var albums = new List<Album>();

            for (int i=1; i<10; i++)
            {
                albums.Add(new Album { Title = "Album" + i.ToString() });
            }
            
            return View(albums);
        }
    }
}