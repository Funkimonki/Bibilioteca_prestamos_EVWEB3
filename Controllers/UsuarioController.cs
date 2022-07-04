using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{
    public class UsuarioController : Controller
    {
        prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();
        // GET: Usuario
        public ActionResult Index()
        {
            var Usuarios = db.Usuario.ToList();

            return View(Usuarios);
        }

        public ActionResult Create()
        {
            ViewBag.id_tipo = new SelectList(db.TipoUsuario, "id_tipo", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Usuario Usuarios)
        {
            string error = "";
            var existeUsuarios = db.Usuario.FirstOrDefault(x => x.Rut == Usuarios.Rut);
            ViewBag.id_tipo = new SelectList(db.TipoUsuario, "id_tipo", "Nombre");


            if (existeUsuarios == null)
            {
                db.Usuario.Add(Usuarios);

                db.SaveChanges();
            }
            else
                error = "El usuario ya está registrado";
            ViewBag.Error = error;
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {

                var usuario = db.Usuario.Find(id);
                if (usuario != null)
                {

                    ViewBag.id_tipo = new SelectList(db.TipoUsuario, "id_tipo", "nombre", usuario.id_tipo);
                    return View(usuario);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Usuario user)
        {

            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var usuario = db.Usuario.Find(id);
                if (usuario != null)
                {
                    return View(usuario);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAccept(int id)
        {
            var usuario = db.Usuario.Find(id);
            foreach (var m in db.Prestamo.Where(f => f.id_Usuario == id))
            {
                db.Prestamo.Remove(m);
            }
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}