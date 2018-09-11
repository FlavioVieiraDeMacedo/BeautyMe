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
    public class ServicosController : Controller
    {
        private Contexto _contexto = new Contexto();

        // GET: Servicos
        [Authorize]
        public ActionResult Index()
        {
            var servicos = _contexto.ServicosEntities.ToList().Where(a => a.Profissional.Email == User.Identity.Name);
            return View(servicos);
        }

        // GET: Servicos/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _contexto.ServicosEntities.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // GET: Servicos/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Descricao,Preco,Tempo")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.Profissional = _contexto.ProfissionalEntities.ToList().Find(a => a.Email == User.Identity.Name);
                _contexto.ServicosEntities.Add(servico);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servico);
        }

        // GET: Servicos/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _contexto.ServicosEntities.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Servico servico)
        {
            
            if (ModelState.IsValid)
            {
                var serv = _contexto.ServicosEntities.ToList().Find(a => a.Id == servico.Id);
                serv.Name = servico.Name;
                serv.Preco = servico.Preco;
                serv.Descricao = servico.Descricao;
                serv.Tempo = servico.Tempo;
                
                _contexto.Entry(serv).State = EntityState.Modified;
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servico);
        }

        // GET: Servicos/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = _contexto.ServicosEntities.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        // POST: Servicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = _contexto.ServicosEntities.Find(id);
            _contexto.ServicosEntities.Remove(servico);
            _contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Pesquisar(string pesquisa = "")
        {
            return View(_contexto.ServicosEntities.ToList().Where(p => p.Name.Contains(pesquisa)).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _contexto.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
