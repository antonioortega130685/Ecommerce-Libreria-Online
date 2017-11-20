using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Libreria.Models;

namespace Libreria.Controllers
{
    public class HomeController : Controller
    {
        Contexto storeDB = new Contexto();


        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            return View(albums);
        }

        private List<Ejemplar> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Ejemplares
                .OrderByDescending(a => a.DetallesOrden.Count())
                .Take(count)
                .ToList();
        }
    

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}