using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyMe.Models;

namespace BeautyMe.Controllers
{
    public class ProfissionalsController : Controller
    {
        private Contexto _contexto = new Contexto();

        // GET: Profissionals
        public ActionResult Index()
        {
            //return View(_contexto.ProfissionalEntities.ToList());
            return RedirectToAction("Agenda");
        }

        // GET: Profissionals/Details/5
        [Authorize(Roles = "Profissional")]
        public ActionResult Details()
        {
            Profissional profissional = _contexto.ProfissionalEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0];
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // GET: Profissionals/Create
        [Authorize]
        public ActionResult Create()
        {
            if(User.IsInRole("Profissional"))
            {
                return View();
            }
            return RedirectToAction("Create", "Clientes");
        }

        // POST: Profissionals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profissional")]
        public ActionResult Create([Bind(Include = "Id,Name,Sexo,CPF,Cidade")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                profissional.Email = User.Identity.Name;
                _contexto.ProfissionalEntities.Add(profissional);
                _contexto.SaveChanges();
                return RedirectToAction("Index","Agenda");
            }

            return View(profissional);
        }

        // GET: Profissionals/Edit/5
        [Authorize(Roles = "Profissional")]
        public ActionResult Edit(int? id)
        {
            Profissional profissional = _contexto.ProfissionalEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0];
            if (profissional == null)
            {
                return HttpNotFound();
            }
            return View(profissional);
        }

        // POST: Profissionals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Profissional")]
        public ActionResult Edit([Bind(Include = "Id,Name,Sexo,CPF,Cidade")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(profissional).State = EntityState.Modified;
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profissional);
        }

        // GET: Profissionals/Delete/5
        //[Authorize(Roles = "Profissional")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Profissional profissional = _contexto.ProfissionalEntities.Find(id);
        //    if (profissional == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(profissional);
        //}

        // POST: Profissionals/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Profissional")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Profissional profissional = _contexto.ProfissionalEntities.Find(id);
        //    _contexto.ProfissionalEntities.Remove(profissional);
        //    _contexto.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contexto.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListarServicos(int? profId)
        {
            if (profId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profissional = _contexto.ProfissionalEntities.ToList().Find(a => a.Id == profId);
            return View(profissional.Servicos.Where(a=>a.Desativado==false).ToList());
        }
    }
}
