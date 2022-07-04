using Bibilioteca_prestamos_EVWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibilioteca_prestamos_EVWEB.Controllers
{

    public class HomeController : Controller
    {    prestamos_bibliotecaEntities db = new prestamos_bibliotecaEntities();

        // GET: Home
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pass)
        {
            string error = "Debe ingresar correo o contraseña";
            if (email.Trim() != "" && pass.Trim() != "")
            {
                var user = db.Usuario
                    .FirstOrDefault(x => x.email == email && x.pass == pass);
                if (user != null)
                {
                    Session["nombre"] = user.Nombre;
                    Session["id"] = user.id_Usuario;
                    Session["tipo"] = user.id_tipo;
                    return RedirectToAction("Index", "Home");
                }
                error = "Correo o contraseña incorrectos";
            }
            ViewBag.Error = error;
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Add(new HttpCookie("nombre", ""));

            return RedirectToAction("Login");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}