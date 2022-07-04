using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class EditorialController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();
        // GET: Editorial
        public ActionResult Index()
        {
            var editorial = db.Editorial.ToList();

            return View(editorial);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Editorial editorial)
        {
            string error = "";
            var existeEditorial = db.Editorial.FirstOrDefault(x => x.Nombre == editorial.Nombre);
            if (existeEditorial == null)
            {
                db.Editorial.Add(editorial);
                db.SaveChanges();
            }
            else
                error = "La editorial ya está registrada";
            ViewBag.Error = error;
            return View();


        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var editorial = db.Editorial.Find(id);
                if (editorial != null)
                {


                    return View(editorial);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Editorial editorial)
        {

            db.Entry(editorial).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var editorial = db.Editorial.Find(id);
                if (editorial != null)
                {
                    return View(editorial);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var editorial = db.Editorial.Find(id);
            foreach (var m in db.Libro.Where(f => f.id_Editorial == id))
            {
                db.Libro.Remove(m);
            }

            db.Editorial.Remove(editorial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}