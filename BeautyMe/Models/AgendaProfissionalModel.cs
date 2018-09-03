namespace BeautyMe.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class AgendaProfissionalModel : DbContext
    {
        public AgendaProfissionalModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Agenda> AgendaEntities { get; set; }
    }

    public class Agenda
    {
        [Key]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Cliente Cliente { get; set; }
        public Servico Servico { get; set; }
    }
}