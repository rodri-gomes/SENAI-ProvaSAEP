using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProvaSAEP.csharp;

namespace ProvaSAEP.csharp
{
    public class cliente
    {
        public string id { get; set; }
        public string nome { get; set; }
    }

    public class concessionaria
    {
        public string id { get; set; }
        public string nome { get; set; }
    }

    public class automoveis
    {
        public string id { get; set; }
        public string modelo { get; set; }
        public string preco { get; set; }
    }

    public class alocacao
    {
        public string id { get; set; }
        public string area { get; set; }
        public string automoveis { get; set; }
        public string concessionaria { get; set; }
        public string quantidade { get; set; }
    }
}