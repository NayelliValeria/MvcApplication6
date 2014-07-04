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
        public List<string> tecnologiasList { get; set; }
        RecluITEntities db = new RecluITEntities();

        //Por administrador
        public void Create() { }

        public void Details() { }

        public void Delete() { }

        public void Edit() { }

        //Por reclutador
        public void changePassword() { }
        public void changeData() { }
    }
}
