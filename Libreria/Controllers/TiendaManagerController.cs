using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria.Models;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using PagedList;



namespace Libreria.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class TiendaManagerController : Controller
    {
        private Contexto db = new Contexto();

        //
        // GET: /TiendaManager/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ejemplares = from s in db.Ejemplares
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ejemplares = ejemplares.Where(s => s.Titulo.Contains(searchString));
                                       //|| s.Autor.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ejemplares = ejemplares.OrderByDescending(s => s.Titulo);
                    break;
                
                default:
                    ejemplares = ejemplares.OrderBy(s => s.Titulo);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(ejemplares.ToPagedList(pageNumber, pageSize));
        }


        public ViewResult NotAdmin()
        {
            return View();
        }

        //
        // GET: /TiendaManager/Details/5

        public ViewResult Details(int id)
        {
            Ejemplar ejemplar = db.Ejemplares.Find(id);
            return View(ejemplar);
        }

        //
        // GET: /TiendaManager/Create

        public ActionResult Create()
        {
            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nombre");
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nombre");
            return View();
        }

        //
        // POST: /TiendaManager/Create

        [HttpPost]
        public ActionResult Create(Ejemplar ejemplar)
        {
            if (ModelState.IsValid)
            {
                db.Ejemplares.Add(ejemplar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nombre", ejemplar.GeneroId);
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nombre", ejemplar.AutorId);
            return View(ejemplar);
        }

        // GET: /TiendaManager/CreateGenero

        public ActionResult CreateGenero()
        {
            
            return View();
        }

        //
        // POST: /TiendaManager/CreateGenero

        [HttpPost]
        public ActionResult CreateGenero(Genero genero)
        {
            if (ModelState.IsValid)
            {
                db.Generos.Add(genero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(genero);
        }

        // GET: /TiendaManager/CreateAutor

        public ActionResult CreateAutor()
        {

            return View();
        }

        //
        // POST: /TiendaManager/CreateAutor

        [HttpPost]
        public ActionResult CreateAutor(Autor autor)
        {
            if (ModelState.IsValid)
            {
                db.Autores.Add(autor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(autor);
        }

        //
        // GET: /TiendaManager/Edit/5

        public ActionResult Edit(int id)
        {
            Ejemplar ejemplar = db.Ejemplares.Find(id);
            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nombre", ejemplar.GeneroId);
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nombre", ejemplar.AutorId);
            return View(ejemplar);
        }

        //
        // POST: /TiendaManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Ejemplar ejemplar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ejemplar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GeneroId = new SelectList(db.Generos, "GeneroId", "Nombre", ejemplar.GeneroId);
            ViewBag.AutorId = new SelectList(db.Autores, "AutorId", "Nombre", ejemplar.AutorId);
            return View(ejemplar);
        }

        //
        // GET: /TiendaManager/Delete/5

        public ActionResult Delete(int id)
        {
            Ejemplar ejemplar = db.Ejemplares.Find(id);
            return View(ejemplar);
        }

        //
        // POST: /TiendaManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ejemplar ejemplar = db.Ejemplares.Find(id);
            db.Ejemplares.Remove(ejemplar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}