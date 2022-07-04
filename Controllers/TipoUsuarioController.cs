using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class TipoUsuarioController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();
        // GET: TipoUsuario
        public ActionResult Index()
        {
            var tipos = db.TipoUsuario.ToList();

            return View(tipos);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(TipoUsuario tipo)
        {

            string error = "";
            var existeTipo = db.TipoUsuario.FirstOrDefault(x => x.Nombre == tipo.Nombre);
            if (existeTipo == null)
            {

                db.TipoUsuario.Add(tipo);

                db.SaveChanges();
            }
            else
                error = "El tipo de usuario ya está registrado";
            ViewBag.Error = error;
            return View();


        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var tipo = db.TipoUsuario.Find(id);
                if (tipo != null)
                {


                    return View(tipo);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(TipoUsuario tipo)
        {

            db.Entry(tipo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var tipo = db.TipoUsuario.Find(id);
                if (tipo != null)
                {
                  
                    return View(tipo);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var tipo = db.TipoUsuario.Find(id);
            foreach (var m in db.Usuario.Where(f => f.id_tipo == id))
            {
                db.Usuario.Remove(m);
            }
        
            db.TipoUsuario.Remove(tipo);
         
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}