using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria.Models;

namespace Libreria.Controllers
{
    public class TiendaController : Controller
    {
        Contexto storeDB = new Contexto();

        //
        // GET: /Tienda/

        public ActionResult Index(string searchString)
        {
            var generos = storeDB.Generos.ToList();
            return View(generos);



        }



        //
        // GET: /Tienda/Browse?genero=Ciencia

        public ActionResult Browse(string genero)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = storeDB.Generos.Include("Ejemplares")
                .Single(g => g.Nombre == genero);

            return View(genreModel);
        }

        //
        // GET: /Tienda/Details/5

        public ActionResult Details(int id)
        {
            var ejemplar = storeDB.Ejemplares.Find(id);

            return View(ejemplar);
        }

        //
        // GET: /Tienda/GenreMenu

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var generos = storeDB.Generos.ToList();

            return PartialView(generos);
        }

    }
}