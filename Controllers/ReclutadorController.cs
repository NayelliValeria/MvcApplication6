using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication6.Models;

namespace MvcApplication6.Controllers
{
    public class ReclutadorController : Controller
    {
        RecluITEntities db = new RecluITEntities();

        public ActionResult ConsultarCandidatos( reclutador rec)
        {
            return View(db.candidato_persona.Where(b => b.idReclutador == rec.idReclutador).ToList());
        }

        public ActionResult AgregarCandidato()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCandidato( candidato_persona can)
        {
            candidato_persona nuevo = new candidato_persona();
            return View(can);
        }
    }
}
