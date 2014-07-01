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
            if (rec.usuario != null && rec.password != null && rec.validar(rec.usuario, rec.password))
            {
                FormsAuthentication.SetAuthCookie(rec.usuario, rec.rememberMe);
                return RedirectToAction("ConsultarCandidatos", "Reclutador", rec);
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
