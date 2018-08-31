namespace BeautyMe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class ProfissionalModel : DbContext
    {
        public ProfissionalModel()
            : base("name=ProfissionalModel")
        {
        }
        
         public virtual DbSet<Profissional> ProfissionalEntities { get; set; }
    }

    public class Profissional
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sexo { get; set; }
        public string CPF { get; set; }

        public string Cidade { get; set; }

        public virtual ICollection<Servico> Servicos { get; set; }
    }
}