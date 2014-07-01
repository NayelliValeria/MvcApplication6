using MvcApplication6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication4.Controllers
{
    public class ReclutadorController : Controller
    {
        public ActionResult ConsultarCandidatos( reclutador rec)
        {
            rec.ConsultarCandidatos();
            return View();
        }
    }
}
