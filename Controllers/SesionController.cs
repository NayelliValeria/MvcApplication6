using MvcApplication6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcApplication6.Controllers
{
    public class SesionController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(reclutador rec)
        {
            rec = rec.validar(rec.usuario, rec.password);
            if (rec!=null)
            {
                FormsAuthentication.SetAuthCookie(rec.usuario, rec.rememberMe);
                Session["idReclutador"] = rec.idReclutador;
                Session["nombre"] = rec.persona.nombre;
                Session["apellidos"] = "" + rec.persona.apePaterno + " " + rec.persona.apeMaterno;
                Session["permisos"] = rec.permisos;
                return RedirectToAction("ConsultarCandidatos", "Candidato", rec);
            }
            ModelState.AddModelError("", "El usuario y/o contraseña son incorrectos.");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Sesion");
        }

    }
}
