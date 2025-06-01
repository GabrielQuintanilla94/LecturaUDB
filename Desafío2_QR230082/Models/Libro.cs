using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaUDB.Models
{
    [Table("Libros")]
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Sinopsis { get; set; }
        public string PortadaUrl { get; set; }

        public virtual ICollection<Calificacion> Calificaciones { get; set; }
    }
}