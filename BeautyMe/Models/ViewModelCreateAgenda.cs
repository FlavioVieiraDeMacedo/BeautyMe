using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyMe.Models
{
    public class ViewModelCreateAgenda
    {
        public List<Servico> servicosDoProfissional { get; set; }
        public Agenda agenda { get; set; }
        public int IdServ { get; set; }
    }
}