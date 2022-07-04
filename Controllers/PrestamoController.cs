using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class PrestamoController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();

        public ActionResult PrestamoC()
        {
            ViewBag.id_Libro = new SelectList(db.Libro, "id_Libro", "Titulo");
            ViewBag.id_Usuario = new SelectList(db.Usuario, "id_Usuario", "Nombre");

            return View();
        }
        [HttpPost]
        public ActionResult PrestamoC(Prestamo prestamos)
        {
            string error1 = "";
            string error = "";

            ViewBag.id_Usuario = new SelectList(db.Usuario, "id_Usuario", "nombre");
            ViewBag.id_Libro = new SelectList(db.Libro, "id_Libro", "Titulo");
            if (prestamos.FechaPres < DateTime.Now)
            {
                error1 = "La fecha de Prestamo es menor a la fecha actual";
                ViewBag.Error1 = error1;
            }
            else
            if (prestamos.FechaDev > prestamos.FechaPres)
            {

                db.Prestamo.Add(prestamos);

                db.SaveChanges();

            }
            else
                error = "La fecha de devolución es menor a la de préstamo";
            ViewBag.Error = error;

            return View();
        }
        [HttpPost]
       
        public ActionResult Index()
        {

                var prestamos = db.Prestamo.ToList();

                return View(prestamos);
         
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var prestamos = db.Prestamo.Find(id);
                if (prestamos != null)
                {
                    string error1 = "";
                    string error = "";

                    ViewBag.id_Usuario = new SelectList(db.Usuario, "id_Usuario", "nombre");
                    ViewBag.id_Libro = new SelectList(db.Libro, "id_Libro", "Titulo");
                    if (prestamos.FechaPres < DateTime.Now)
                    {
                        error1 = "La fecha de Prestamo es menor a la fecha actual";
                        ViewBag.Error1 = error1;
                    }
                    else
                    if (prestamos.FechaDev > prestamos.FechaPres)
                    {

                        db.Prestamo.Add(prestamos);

                        db.SaveChanges();

                    }
                    else
                        error = "La fecha de devolución es menor a la de préstamo";
                    ViewBag.Error = error;

                  
                    return View(prestamos);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Prestamo prestamos)
        {

            db.Entry(prestamos).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var prestamo = db.Prestamo.Find(id);
                if (prestamo != null)
                {
                    return View(prestamo);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var prestamo = db.Prestamo.Find(id);          
            db.Prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
