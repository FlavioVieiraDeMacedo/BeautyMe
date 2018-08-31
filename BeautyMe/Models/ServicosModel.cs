namespace BeautyMe.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class ServicosModel : DbContext
    {
        public ServicosModel()
            : base("name=ServicosModel")
        {
        }
        public virtual DbSet<Servico> ServicosEntities { get; set; }
    }

    public class Servico
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Tempo { get; set; }

        public virtual Profissional Profissional{ get; set; }
}
}