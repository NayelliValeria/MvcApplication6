﻿using MvcApplication6.Models;
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
        RecluITEntities db = new RecluITEntities();
    
        //Acciones realizadas por Reclutador
        public ActionResult ConsultarCandidatos()
        {
            int id = (int)Session["idReclutador"];
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
                marcarTecnologias(null);
                if (ModelState.IsValid)
                {
                    nuevo.idReclutador = (int)Session["idReclutador"];
                    nuevo.setIds();
                    nuevo = asignarTecnologias(tecnologiasList, nuevo);
                    db.Entry(nuevo).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarCandidatos");
                }
                else
                {
                    ModelState.AddModelError("", "Por favor verifique la información proporcionada.");
                    return View();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                ModelState.AddModelError("", "Ha ocurrido un error al guardar los datos, por favor intente nuevamente.");
                return View();
            }
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
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var actualizar = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
            if( TryUpdateModel( actualizar,"", new string[]{ "CURP", "RFC", "email", "telefono", "palabrasClave"}))
            {
                try{

                }
            }
        }

        /*
         * 
         * 

                marcarTecnologias(null);
                if (ModelState.IsValid)
                {
                    editar =  asignarTecnologias(tecnologiasList,editar);
                    db.Entry(editar).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarCandidatos");
                }
                else
                {
                    ModelState.AddModelError("", "Por favor verifique la información proporcionada.");
                    return View();
                }
                int id = Convert.ToInt16(ide);
                var candidato = db.candidato.Where(b => b.idCandidato == id)
                .Include(b => b.tecnologia)
                .Include(b => b.persona)
                .Single();
                return View(candidato);
            }
            catch { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
        }
        
*/

        //Eliminar candidato
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
