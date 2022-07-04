using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
  
    public class EjemplarController : Controller
    {  prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();
        // GET: Ejemplar
        public ActionResult Index()
        {
            var ejemplares = db.Ejemplar.ToList();

            return View(ejemplares);
        }

        public ActionResult Create()
        {
            ViewBag.id_libro = new SelectList(db.Libro, "id_libro", "Titulo");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Ejemplar ejemplares)
        {
            db.Ejemplar.Add(ejemplares);
            db.SaveChanges();
            ViewBag.id_libro = new SelectList(db.Libro, "id_libro", "Titulo");
            return View();
        }
           
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var ejemplares = db.Ejemplar.Find(id);
                if (ejemplares != null)
                {

                    ViewBag.id_Libro = new SelectList(db.Libro, "id_libro", "Titulo", ejemplares.id_Libro);
                    return View(ejemplares);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Ejemplar ejemplares)
        {

            db.Entry(ejemplares).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var ejemplares = db.Ejemplar.Find(id);
                if (ejemplares != null)
                {
                    return View(ejemplares);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var ejemplares = db.Ejemplar.Find(id);
            db.Ejemplar.Remove(ejemplares);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}