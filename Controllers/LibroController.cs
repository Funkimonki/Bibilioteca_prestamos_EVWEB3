using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class LibroController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();

        // GET: Libro
        public ActionResult Index()
        {
            var Libros = db.Libro.ToList();

            return View(Libros);
        }
        public ActionResult ListaLibros(int? Autor, int? Editorial)
        {
            var lista = db.Libro.ToList();
            if (Autor != null)
                lista = lista.Where(x => x.id_Autor == Autor).ToList();
            if (Editorial != null)
                lista = lista.Where(x => x.id_Editorial == Editorial).ToList();

            return PartialView("_ListaLibros", lista);
        }
        public ActionResult Create()
        {          
            ViewBag.id_Editorial = new SelectList(db.Editorial , "id_Editorial", "Nombre");
            ViewBag.id_Autor = new SelectList(db.Autor, "id_Autor", "Nombre");

            return View();
        }
        [HttpPost]
        public ActionResult Create(Libro Libros, HttpPostedFileBase archivo)
        {
            if (archivo != null)
            {
                string nombreArchivo = Libros.Titulo;
                string extension = Path.GetExtension(archivo.FileName);
                nombreArchivo = nombreArchivo + extension;
                string ruta = Path.Combine(Server.MapPath("/Imagenes/"), nombreArchivo);
                archivo.SaveAs(ruta);
                Libros.archivo = nombreArchivo;
            }
            db.Libro.Add(Libros);
            db.SaveChanges();
    
            ViewBag.id_Editorial = new SelectList(db.Editorial, "id_Editorial", "Nombre");     
            ViewBag.id_Autor = new SelectList(db.Autor, "id_Autor", "Nombre");
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var Libros = db.Libro.Find(id);
                if (Libros != null)
                {
                    ViewBag.id_Editorial = new SelectList(db.Editorial, "id_Editorial", "Nombre",Libros.id_Editorial);
                    ViewBag.id_Autor = new SelectList(db.Autor, "id_Autor", "Nombre",Libros.id_Autor);
                    return View(Libros);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Libro Libros, HttpPostedFileBase archivo)
        {
            var Book = db.Libro.Find(Libros.id_Libro);

            if (archivo != null)
            {
                string nombreArchivo = Libros.Titulo;
                string extension = Path.GetExtension(archivo.FileName);
                nombreArchivo = nombreArchivo + extension;
                string ruta = Path.Combine(Server.MapPath("/Imagenes/"), nombreArchivo);
                archivo.SaveAs(ruta);
                Book.archivo = nombreArchivo;
            }
            db.Entry(Book).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var Libros = db.Libro.Find(id);
                if (Libros != null)
                {
                    return View(Libros);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var Libros = db.Libro.Find(id);
            foreach (var m in db.Prestamo.Where(f => f.id_Libro == id))
            {
                db.Prestamo.Remove(m);
            }
            foreach (var n in db.Ejemplar.Where(f => f.id_Libro == id))
            {
                db.Ejemplar.Remove(n);
            }
            db.Libro.Remove(Libros);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}