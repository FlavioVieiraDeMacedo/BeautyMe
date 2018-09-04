using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyMe.Models;
using Microsoft.AspNet.Identity;

namespace BeautyMe.Controllers
{
    public class AgendaController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Agenda
        public ActionResult Index()
        {
            if (User.IsInRole("Cliente"))
            {
                return RedirectToAction("VerAgendaCliente");
            }
            else
            {
                var _prof = db.ProfissionalEntities.Where(a => a.Email == User.Identity.Name).ToArray();
                if (_prof.Length>0)
                {
                    int profissionalId = _prof[0].Id;
                    return RedirectToAction("VerAgendaProfissional", new { profId = profissionalId });
                }
                
            }
            
            //return View(db.AgendaEntities.ToList());
            return RedirectToAction("Create");
        }

        // GET: Agenda/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Agenda agenda = db.AgendaEntities.Find(id);
        //    if (agenda == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(agenda);
        //}

        // GET: Agenda/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Data,Descricao")] Agenda agenda, int IdServ)
        {
            int IdCli = db.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0].Id;
            if (ModelState.IsValid)
            {
                agenda.Cliente = db.ClienteEntities.Find(IdCli);
                agenda.Servico = db.ServicosEntities.Find(IdServ);

                db.AgendaEntities.Add(agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agenda);
        }

        // GET: Agenda/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Agenda agenda = db.AgendaEntities.Find(id);
        //    if (agenda == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(agenda);
        //}

        // POST: Agenda/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Agenda agenda = db.AgendaEntities.Find(id);
        //    db.AgendaEntities.Remove(agenda);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Cliente")]
        public ActionResult VerAgendaCliente()
        {
            Cliente cliente = db.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0];
            

            return View(db.AgendaEntities.ToList().Where(a => a.Cliente.Id == cliente.Id).ToList());
        }


        public ActionResult VerAgendaProfissional(int? profId)
        {
            if (profId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var prof = db.ProfissionalEntities.ToList().Find(a => a.Id == profId);
            var cli = db.ClienteEntities.ToList().First();
            var servicos = new List<int>();

            foreach (var item in prof.Servicos)
            {
                servicos.Add(item.Id);
            }

            var agenda = new List<Agenda>();

            foreach (var item in servicos)
            {
                agenda.AddRange(db.AgendaEntities.Where(a=>a.Servico.Id==item));
            }

            return View(agenda);
        }
    }
}
