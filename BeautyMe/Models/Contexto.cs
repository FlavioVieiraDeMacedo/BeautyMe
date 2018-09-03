using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeautyMe.Models
{
    public class Contexto : DbContext
    {

        public Contexto()
            : base("DefaultConnection") { }

        public DbSet<Cliente> ClienteEntities { get; set; }
        public DbSet<Agenda> AgendaEntities { get; set; }
        public DbSet<Servico> ServicosEntities { get; set; }
        public DbSet<Profissional> ProfissionalEntities { get; set; }
    }
}