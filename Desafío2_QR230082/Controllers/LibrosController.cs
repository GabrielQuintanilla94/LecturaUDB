using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LecturaUDB.Models;
using PagedList;

namespace LecturaUDB.Controllers
{
    public class LibrosController : Controller
    {
        private LecturaContext db = new LecturaContext();

        // GET: Libros
        public ActionResult Index(string searchTitle, string searchGenero, int? page)
        {
            // Obtener lista de géneros para el filtro
            var generos = db.Libros
                .Select(l => l.Genero)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            ViewBag.GeneroList = new SelectList(generos); // esta clave debe coincidir con la vista
            ViewBag.CurrentGenero = searchGenero;

            // Consulta base
            var libros = db.Libros.AsQueryable();

            // Filtro por título
            if (!String.IsNullOrEmpty(searchTitle))
            {
                libros = libros.Where(l => l.Titulo.Contains(searchTitle));
            }

            // Filtro por género
            if (!String.IsNullOrEmpty(searchGenero))
            {
                libros = libros.Where(l => l.Genero == searchGenero);
            }

            ViewBag.CurrentFilter = searchTitle;

            // Paginación
            int pageSize = 5;
            int pageNumber = page ?? 1;

            return View(libros.OrderBy(l => l.Titulo).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult TopLecturas()
        {
            var topLibros = db.Calificaciones
                .Include(c => c.Libro)
                .Where(c => c.Libro != null)
                .GroupBy(c => new
                {
                    c.Libro.Id,
                    c.Libro.Titulo,
                    c.Libro.Autor,
                    c.Libro.Genero,
                    c.Libro.PortadaUrl
                })
                .Select(g => new TopLibroViewModel
                {
                    Titulo = g.Key.Titulo,
                    Autor = g.Key.Autor,
                    Genero = g.Key.Genero,
                    PortadaUrl = g.Key.PortadaUrl,
                    Promedio = g.Average(c => c.Puntuacion)
                })
                .OrderByDescending(x => x.Promedio)
                .Take(5)
                .ToList();

            int posicion = 1;
            foreach (var item in topLibros)
            {
                item.Posicion = posicion++;
            }

            return View(topLibros);
        }

        // GET: Libros/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var libro = db.Libros.Find(id);
            if (libro == null)
                return HttpNotFound();

            // Calcular el promedio de calificaciones
            var promedio = db.Calificaciones
                             .Where(c => c.LibroId == libro.Id)
                             .Select(c => (double?)c.Puntuacion)
                             .Average() ?? 0;

            ViewBag.Promedio = Math.Round(promedio, 2);

            return View(libro);
        }

        [HttpPost]
        public ActionResult Calificar(int idLibro, int puntuacion)
        {
            var calificacion = new Calificacion
            {
                LibroId = idLibro,
                Puntuacion = puntuacion,
                FechaHora = DateTime.Now
            };

            db.Calificaciones.Add(calificacion);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = idLibro });
        }

        // GET: Libros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Autor,Genero,Sinopsis,PortadaUrl")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Libros.Add(libro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        // GET: Libros/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Libro libro = db.Libros.Find(id);
            if (libro == null)
                return HttpNotFound();

            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Autor,Genero,Sinopsis,PortadaUrl")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(libro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(libro);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Libro libro = db.Libros.Find(id);
            if (libro == null)
                return HttpNotFound();

            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Libro libro = db.Libros.Find(id);
            db.Libros.Remove(libro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    } 
}