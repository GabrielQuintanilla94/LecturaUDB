using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LecturaUDB.Models
{
    public class LecturaContext : DbContext
    {
        public LecturaContext() : base("LecturaContext") 
        {
            Database.SetInitializer<LecturaContext>(null);

        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
    }
}