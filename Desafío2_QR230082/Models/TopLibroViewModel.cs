using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LecturaUDB.Models
{
    public class TopLibroViewModel
    {
        public int Posicion { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public double Promedio { get; set; }
        public string PortadaUrl { get; set; }
    }
}