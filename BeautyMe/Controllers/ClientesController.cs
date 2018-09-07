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
    public class ClientesController : Controller
    {

        private Contexto _contexto = new Contexto();

        // GET: Clientes
        public ActionResult Index()
        {
            //return View(_contexto.ClienteEntities.ToList());
            return RedirectToAction("Index","Agenda");
        }

        // GET: Clientes/Details/5
        public ActionResult Details()
        {
            var _cli = _contexto.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray();
            if (_cli.Length <= 0)
            {
                return View("Create");
            }
            Cliente cliente = _cli[0];
            return View(cliente);
        }

        // GET: Clientes/Create
        [Authorize(Roles = "Cliente")]
        public ActionResult Create()
        {
            var _cli = _contexto.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray();
            if (_cli.Length <=0)
            {
                return View();
            }
            Cliente cliente = _cli[0];
            
            return View("Details");
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Cliente")]
        public ActionResult Create([Bind(Include = "Id,Name,Sexo,CPF,Pais,Cidade,Bairro,CEP,Endereco,Complemento,Tomada110,Tomada220,Espelho,Cadeira,Agua,Luz")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Email = User.Identity.Name;
                _contexto.ClienteEntities.Add(cliente);
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit()
        {
            Cliente cliente = _contexto.ClienteEntities.Where(a => a.Email == User.Identity.Name).ToArray()[0];
            if (cliente == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Sexo,CPF,Pais,Cidade,Bairro,CEP,Endereco,Complemento,Tomada110,Tomada220,Espelho,Cadeira,Agua,Luz")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _contexto.Entry(cliente).State = EntityState.Modified;
                _contexto.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Cliente cliente = _contexto.ClienteEntities.Find(id);
        //    if (cliente == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cliente);
        //}

        // POST: Clientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Cliente cliente = _contexto.ClienteEntities.Find(id);
        //    _contexto.ClienteEntities.Remove(cliente);
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
    }
}
