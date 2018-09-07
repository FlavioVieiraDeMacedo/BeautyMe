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
        
        // GET: Agenda/Create
        [Authorize]
        public ActionResult Create(int IdProf)
        {
            ViewModelCreateAgenda viewModel = new ViewModelCreateAgenda() {
                servicosDoProfissional = db.ProfissionalEntities.Find(IdProf).Servicos.ToList()
            };
            return View(viewModel);
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModelCreateAgenda viewModel)
        {
            var agenda = viewModel.agenda;
            int IdCli = db.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0].Id;
            if (ModelState.IsValid)
            {
                agenda.Cliente = db.ClienteEntities.Find(IdCli);
                agenda.Servico = db.ServicosEntities.Find(viewModel.IdServ);

                db.AgendaEntities.Add(agenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agenda);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Authorize(Roles = "Cliente")]
        public ActionResult VerAgendaCliente()
        {
            var cli = db.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray();
            if (cli.Length>0)
            {
                db.ClienteEntities.ToList();
                db.ServicosEntities.ToList();
                return View(db.AgendaEntities.ToList().Where(a => a.Cliente.Id == cli[0].Id).ToList());
            }
            return RedirectToAction("Create","Clientes");
           
        }

        [HttpGet]
        public ActionResult VerAgendaProfissional(int? profId1)
        {
            var viewModel = new ViewModelAgendaProfissional();
            if (profId1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var prof = db.ProfissionalEntities.ToList().Find(a => a.Id == profId1);
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
            viewModel.NomeProf = prof.Name;
            viewModel.agenda = agenda;
            viewModel.IdProf = (int)profId1;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult VerAgendaProfissional(ViewModelAgendaProfissional viewModel)
        {

            return RedirectToAction("Create", new { IdProf = viewModel.IdProf });
        }

    }
}
