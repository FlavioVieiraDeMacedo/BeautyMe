namespace BeautyMe.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sexo { get; set; }
        public string CPF { get; set; }

        public string Email { get; set; }
        //Local 
        public string Pais { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Complemento { get; set; }

        //Descri��o do domicilio
        public bool Tomada110 { get; set; }
        public bool Tomada220 { get; set; }
        public bool Espelho { get; set; }
        public bool Cadeira { get; set; }
        public bool Agua { get; set; }
        public string Luz { get; set; }

    }
}