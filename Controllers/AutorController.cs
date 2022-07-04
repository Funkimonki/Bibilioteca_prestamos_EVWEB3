using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class AutorController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();
        // GET: TipoUsuario
        public ActionResult Index()
        {
            var autor = db.Autor.ToList();

            return View(autor);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Autor autor)
        {
            string error = "";
            var existeAutor = db.Autor.FirstOrDefault(x => x.Nombre == autor.Nombre);
            if (existeAutor == null)
            {
                db.Autor.Add(autor);
                db.SaveChanges();
            }
            else
            error = "El autor ya está registrado";
            ViewBag.Error = error;
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
               
                var autor = db.Autor.Find(id);
                if (autor != null)
                {
                    
             
                    return View(autor);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Autor autor)
        {
      
            db.Entry(autor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var autor = db.Autor.Find(id);
                if (autor != null)
                {
                    return View(autor);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var autor = db.Autor.Find(id);
            foreach (var m in db.Libro.Where(f => f.id_Autor == id))
            {
                db.Libro.Remove(m);
            }

            db.Autor.Remove(autor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}