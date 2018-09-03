namespace BeautyMe.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Profissional
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Sexo { get; set; }
        public string CPF { get; set; }

        public string Cidade { get; set; }

        
        public virtual ICollection<Servico> Servicos { get; set; }
    }
}