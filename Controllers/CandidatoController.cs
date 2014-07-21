using MvcApplication6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication6.Controllers
{
    public class CandidatoController : Controller
    {
        RecluITEntities1 db = new RecluITEntities1();
    
        //Acciones realizadas por Reclutador
        public ActionResult ConsultarCandidatos()
        {
            int id = (int)Session["idReclutador"];
            if ((int)Session["permisos"] == 1)// administrador
                ViewBag.candidatos = db.candidato.Include(b=>b.reclutador).ToList();
            else //reclutador
                ViewBag.candidatos = db.candidato.Where(b => b.idReclutador == id).ToList();
            return View();
        }

        //Nuevo candidato
        public ActionResult Create()
        {
            marcarTecnologias(null);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(candidato nuevo, string[] tecnologiasList, FormCollection collection)
        {
            try
            {
                marcarTecnologias(nuevo);
                if (ModelState.IsValid)
                {
                    nuevo.idReclutador = (int)Session["idReclutador"];
                    nuevo.setIds();
                    nuevo.fecha_registro = DateTime.Now;
                    nuevo = asignarTecnologias(tecnologiasList, nuevo);
                    db.Entry(nuevo).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarCandidatos");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Ha ocurrido un error al guardar los datos, por favor intente nuevamente." + "\n\nError: " + e.Message);
                return View(nuevo);
            }
            finally
            {
                ModelState.AddModelError("", "Por favor verifique la información proporcionada.");
            }
            return View(nuevo);
        }
        //Verificar CURP para nuevo registro
        [HttpPost]
        public ActionResult verificarCURP( string curp)
        {
            if (curp == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try{
                db.candidato.Where(b => b.CURP == curp).Single();
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }catch{
                return new HttpStatusCodeResult(HttpStatusCode.OK); 
            }
        }
        //detalles del curp registrado
        //[HttpPost]
        public ActionResult detallesCURP(string curp)
        {
            if (curp == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try
            {
                var candidato = db.candidato.Where(b => b.CURP == curp)
                    .Include(b=>b.persona)
                    .Include(b=>b.reclutador).Single();
                Response.StatusCode = (int)HttpStatusCode.OK;
                string texto = "<br>Nombre: " + candidato.persona.nombre
                    + " " + candidato.persona.apePaterno
                    + " " + candidato.persona.apeMaterno
                    + "<br>Fecha: " + candidato.fecha_registro
                    + "<br>e-mail: " + candidato.email
                    + "<br>Reclutador: " + candidato.reclutador.persona.nombre
                    + " " + candidato.reclutador.persona.apePaterno
                    + " " + candidato.reclutador.persona.apeMaterno + " ";
                return Content( texto );
            }
            catch { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
        }

        //Editar candidato
        public ActionResult Edit(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var candidato = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
            marcarTecnologias(candidato);
            return View(candidato);
        }
        [HttpPost]
        public ActionResult Edit(int? id, string[] tecnologiasList)
        {
            if (id == null)
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            var actualizar = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
            marcarTecnologias(actualizar);
            actualizar = asignarTecnologias(tecnologiasList, actualizar);
            if (TryUpdateModel(actualizar, "", new string[] { "persona", "CURP", "RFC", "email", "telefono", "palabrasClave" }))
            {
                try{
                    db.Entry(actualizar).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarCandidatos");
                }
                catch{
                    ModelState.AddModelError("", "No se pudo actualizar la información, intente nuevamente.");
                }
            }
            return View(actualizar);
        }

        //Eliminar candidato
        public ActionResult Delete(int? id)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var candidato = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
            marcarTecnologias(candidato);
            return View(candidato);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var delete = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
            try
            {
                db.Entry(delete).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("ConsultarCandidatos");
            }catch
            {
                ModelState.AddModelError("", "No has sido posible eliminar a este candidato, intente nuevamente.");
            }
            return View(delete);
        }

        //Agregar tecnologias
        private candidato asignarTecnologias(string[] seleccionadas, candidato candidato)
        {
            if (seleccionadas == null)
            {
                candidato.tecnologia = new List<tecnologia>();
                return candidato;
            }
            var tSeleccionadas = new HashSet<string>(seleccionadas);
            var candTecs = new HashSet<int>(candidato.tecnologia.Select(t => t.idTecnologia));
            if (candTecs == null)
                candTecs = new HashSet<int>();
            foreach (var tec in db.tecnologia)
            {
                if (tSeleccionadas.Contains(tec.idTecnologia.ToString()))//Si está seleccionada
                {
                    if (!candTecs.Contains(tec.idTecnologia))//si aún no la tiene registrada, se agrega
                        candidato.tecnologia.Add(tec);
                }
                else //Si no está seleccionada
                {
                    if (candTecs.Contains(tec.idTecnologia))//Si la tiene registrada, se elimina
                        candidato.tecnologia.Remove(tec);
                }
            }
            return candidato;
        }
        //actualizar tecnologias
        private void marcarTecnologias(candidato candidato)
        {
            var todasTecnologias = db.tecnologia;
            var tecnologiaCandidato =new HashSet<int>();
            if (candidato != null)
                tecnologiaCandidato = new HashSet<int>(candidato.tecnologia.Select(t => t.idTecnologia));
            var chbox = new List<tecnologiaCandidato>();
            foreach( var tecnologia in todasTecnologias)
            {
                chbox.Add(new tecnologiaCandidato
                {
                    idTecnologia = tecnologia.idTecnologia,
                    nombreTecnologia = tecnologia.nombre,
                    domina = tecnologiaCandidato.Contains( tecnologia.idTecnologia)
                });
            }
            ViewBag.tecnologias = chbox;
        }
    }
}
