using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using LecturaUDB.Models;

namespace LecturaUDB.Controllers
{
    public class ApiLibrosController : ApiController
    {
        private LecturaContext db = new LecturaContext();

        // GET: api/apilibros
        [HttpGet]
        public IHttpActionResult GetLibros()
        {
            var libros = db.Libros.Select(l => new
            {
                l.Id,
                l.Titulo,
                l.Autor,
                l.Genero,
                l.Sinopsis,
                l.PortadaUrl
            }).ToList();

            return Ok(libros);
        }

        // POST: api/apilibros
        [HttpPost]
        public IHttpActionResult PostLibro(Libro libro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Libros.Add(libro);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = libro.Id }, libro);
        }

        // PUT: api/apilibros/5
        [HttpPut]
        public IHttpActionResult PutLibro(int id, Libro libro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var libroExistente = db.Libros.Find(id);
            if (libroExistente == null)
                return NotFound();

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.Genero = libro.Genero;
            libroExistente.Sinopsis = libro.Sinopsis;
            libroExistente.PortadaUrl = libro.PortadaUrl;

            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/apilibros/5
        [HttpDelete]
        public IHttpActionResult DeleteLibro(int id)
        {
            var libro = db.Libros.Find(id);
            if (libro == null)
                return NotFound();

            db.Libros.Remove(libro);
            db.SaveChanges();

            return Ok();
        }
    }
}