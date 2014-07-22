using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication6.Models;
using System.Net;
using System.Data.Entity.Infrastructure;

namespace MvcApplication6.Controllers
{
    public class ReclutadorController : Controller
    {
        RecluITEntities1 db = new RecluITEntities1();

        //Por administrador
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "persona, usuario, password, permisos")]reclutador rec)
        {
            try
            {
                rec = setIds(rec);
                if (ModelState.IsValid)
                {
                    db.reclutador.Add(rec);
                    //db.Entry(rec).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Este nombre de usuario ya existe, por favor elija otro.");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ha ocurrido un error al guardar los datos, por favor intente nuevamente.");
                return View();
            }
            return View();
        }

        //Consultar reclutadores
        public ActionResult Details() 
        {
            ViewBag.reclutadores = db.reclutador
                .Include(b => b.persona)
                .OrderBy(b => b.persona.apePaterno).ToList();
            return View();
        }

        //Eliminar reclurador
        public ActionResult Delete( int? id) 
        {
            var delete = db.reclutador.Where(b => b.idReclutador == id).Single();
            return View(delete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var delete = db.reclutador.Where(b => b.idReclutador == id).Single();
            try
            {
                db.Entry(delete).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            catch {
                ModelState.AddModelError("","El reclutador no se ha podido eliminar correctamente, por favor intente nuevamente.");
                return View(delete);
            }
        }

        //Editar datos de reclutador
        public ActionResult Edit(int? id) 
        {
            var editar = db.reclutador.Where(b => b.idReclutador == id)
                .Include(b => b.persona)
                .Single();
            return View(editar);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var editar = db.reclutador.Where(b => b.idReclutador == id)
                .Include(b => b.persona)
                .Single();
            try
            {
                if (TryUpdateModel(editar, "", new string[] { "persona", "usuario", "permisos" }))
                {
                    db.Entry(editar).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
            }
            catch(RetryLimitExceededException) {
                ModelState.AddModelError("","El reclutador no se ha guardado correctamente, por favor intente nuevamente. ");
                return View(editar);
            }
            return View(editar);
        }

        //Por reclutador
        public ActionResult changePassword() {
            return View();
        }
        //[HttpPost]
        public ActionResult changePassword2(string pass, string pass2)
        {
            //string pass2="";
            if(pass == null || pass2==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            string user = (string)Session["usuario"];
            var rec = db.reclutador
                .Where(b => b.usuario ==user)
                .Where(b => b.password == pass)
                .Include(b=>b.persona)
                .Single();
            //rec.password = pass2;
            try
            {
                db.Entry(rec).Property(b=>b.password).CurrentValue = pass2;
                db.Entry(rec).State = EntityState.Modified;
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (RetryLimitExceededException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            catch { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
        }


        public void changeData() { }

        //Generar nuevo id
        private reclutador setIds(reclutador rec)
        {
            try { rec.idReclutador = db.reclutador.Max(b => b.idReclutador) + 1; }
            catch { rec.idReclutador = 1; }
            try { rec.persona.idPersona = rec.idPersona = db.persona.Max(b => b.idPersona) + 1; }
            catch { rec.persona.idPersona = rec.idPersona = 1; }
            return rec;
        }

    }
}
