using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyMe.Models
{
    public class ViewModelAgendaProfissional
    {
        public List<Agenda> agenda { get; set; }
        public int IdProf { get; set; }
        public string NomeProf { get; set; }
        public List<ViewModeldatas> calendario { get; set; }
    }
    public class ViewModeldatas
    {
        public string data { get; set; }
        public int quantidade { get; set; }
    }
}