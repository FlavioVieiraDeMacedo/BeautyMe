namespace BeautyMe.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Servico
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Tempo { get; set; }

        [Required]
        public int Profissional_Id { get; set; }

        [ForeignKey("Profissional_Id")]
        public virtual Profissional Profissional{ get; set; }
}
}