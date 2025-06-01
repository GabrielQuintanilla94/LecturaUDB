using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LecturaUDB.Models
{
    [Table("Calificaciones")]
    public class Calificacion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Libro")]
        public int LibroId { get; set; }

        [Range(1, 5)]
        public int Puntuacion { get; set; }

        public DateTime FechaHora { get; set; }

        public virtual Libro Libro { get; set; }
    }
}