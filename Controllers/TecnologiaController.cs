﻿using MvcApplication6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication6.Controllers
{
    public class TecnologiaController : Controller
    {
        RecluITEntities1 db = new RecluITEntities1();

        //Consultar Tecnologia
        public ActionResult Details()
        {
            ViewBag.tecnologias = db.tecnologia.OrderBy(b=>b.nombre).ToList();
            return View();
        }

        //Agregar  nueva tecnología
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tecnologia nueva, FormCollection collection)
        {
            try
            {
                nueva.idTecnologia = crearId();
                db.Entry(nueva).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            catch
            {
                ModelState.AddModelError("","Ha ocurrido un error al registrar esta tecnología, por favor intente nuevamente");
                return View();
            }
        }

        //Editar
        public ActionResult Edit(int? id)
        {
            tecnologia tec = db.tecnologia.Where(b => b.idTecnologia == id).Single();
            return View(tec);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var actualizar = db.tecnologia.Where(b => b.idTecnologia == id).Single();
            try
            {
                if (TryUpdateModel(actualizar, "", new string[] { "nombre" }))
                {
                    db.Entry(actualizar).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details");
                }
            }
            catch
            {
                ModelState.AddModelError("","La tecnología no se ha podido guardar correctamente, por favor intente nuevamente.");
                return View(actualizar);
            }
            return View(actualizar);
        }

        //Eliminar tecnología
        public ActionResult Delete(int? id)
        {
            var eliminar = db.tecnologia.Where(b => b.idTecnologia == id).Single();
            return View(eliminar);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id == null)
            { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            var delete = db.tecnologia.Where(b => b.idTecnologia == id).Single();
            try
            {
                db.Entry(delete).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Esta tecnología no ha podido eliminarse debido a que hay candidatos registrados con ella.");
                return View(delete);
            }
            catch
            {
                ModelState.AddModelError("", "HA ocurrido un error al eliminar esta tecnología, por favor intente nuevamente.");
                return View(delete);
            }
        }

        //Crear un Id para una nueva tecnología
        private int crearId()
        {
            try
            {
                int max = db.tecnologia.Max(b => b.idTecnologia);
                return max+1;
            }
            catch { return 1; }
        }
    }
}
