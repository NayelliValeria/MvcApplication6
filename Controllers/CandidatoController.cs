using MvcApplication6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            marcarTecnologias(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(candidato nuevo, string[] tecs, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    nuevo.idReclutador = (int)Session["idReclutador"];
                    nuevo.setIds();
                    asignarTecnologias(tecs, nuevo);
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
            /*catch (ArgumentException)
            { }*/
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                ModelState.AddModelError("", "Ha ocurrido un error al guardar los datos, por favor intente nuevamente.");
                marcarTecnologias(null);
                return View();
            }
        }

        private void asignarTecnologias( string[] seleccionadas, candidato candidato) 
        {
            if(seleccionadas == null)
            {
                candidato.tecnologia = new List<tecnologia>();
                return;
            }
            var tSeleccionadas = new HashSet<string>(seleccionadas);
            var candTecs = new HashSet<int>(candidato.tecnologia.Select( t=>t.idTecnologia));
            if(candTecs == null)
                candTecs = new HashSet<int>();
            foreach( var tec in db.tecnologia)
            {
                if( tSeleccionadas.Contains(tec.idTecnologia.ToString()) )//Si está seleccionada
                {
                    if( !candTecs.Contains(tec.idTecnologia) )//si aún no la tiene registrada, se agrega
                        candidato.tecnologia.Add(tec);
                }else //Si no está seleccionada
                {
                    if(candTecs.Contains(tec.idTecnologia))//Si la tiene registrada, se elimina
                        candidato.tecnologia.Remove(tec);
                }
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

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

        private void agregarTecnologiasCandidato(string[] tecs, int idCandidato)
        {
            if (tecs == null)
            {
                //Mandar mensaje de error
            }
            foreach (string s in tecs)
            {
                Console.WriteLine("elemento:" + s);
            }
        }
    }
}
