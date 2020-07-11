using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppLibros.Context;
using AppLibros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient.Server;
using AppLibros.Helper_Functions;

namespace AppLibros.Controllers
{
    public class LibroController : Controller
    {
        private readonly LibrosDataBaseContext _context;
        private readonly Helpers _helpers;

        public LibroController(LibrosDataBaseContext context)
        {
            _context = context;
            _helpers = new Helpers(_context);
        }

        // GET: Libro
        public async Task<IActionResult> Index()
        {
            
            if (HttpContext.Session.GetString("esAdmin") != "True")
            {
                return View("IndexUser", await _context.libros.ToListAsync());
            }
            return View("Index", await _context.libros.ToListAsync());
        }

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros
                .FirstOrDefaultAsync(m => m.id == id);

            if (libro == null)
            {
                return NotFound();
            }

            double promedio = _helpers.Promedio(libro);
           
            ViewBag.autor = _helpers.BuscarAutor(libro.autorid);
            ViewBag.promedio = promedio;
            ViewBag.esFav = await _helpers.BuscarLibroFavorito(libro.id, HttpContext.Session.GetInt32("id"));
            ViewBag.idLibro = libro.id;
            
            if (HttpContext.Session.GetString("esAdmin") == "True")
            {
                return View("Details", libro);
            }
            if (HttpContext.Session.GetInt32("id") != 0 )
            {
                ViewBag.puntaje = _helpers.BuscarPuntaje(libro.id, HttpContext.Session.GetInt32("id"));

                return View("DetailsUsuario", libro);
            }

            return View("DetailsInvitado", libro);
        }

        // GET: Libro/Create
        public IActionResult Create()
        {
            List<Autor> lAutores = new List<Autor>();
            lAutores = _context.autores.ToList();
            foreach (Autor autor in lAutores) 
            {
                autor.apellido = autor.nombre + " " + autor.apellido;
            }
            var listaautores = new SelectList(lAutores, "autor", "apellido");
            ViewBag.listaAutores = lAutores;
            ViewBag.lista2 = listaautores;
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,titulo,isbn,autorid,resena")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                libro.puntaje = 0;
                libro.votos = 0;
                _context.libros.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
        
            return View(libro);
        }

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("id,titulo,isbn,resena,puntaje,votos")] Libro libro*/ string titulo, string isbn, string resena)
        {
            var libro = _context.libros.Find(id);
            if (id != libro.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    libro.titulo = titulo;
                    libro.isbn = isbn;
                    libro.resena = resena;
                    _context.libros.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.libros
                .FirstOrDefaultAsync(m => m.id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.libros.FindAsync(id);
            _context.libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.libros.Any(e => e.id == id);
        }

        public async Task<IActionResult> agregarFavorito(int id)
        {
          
            var libroFav = new LibrosFavoritos();

            libroFav.idLibro = id;
            libroFav.idUsuario = (int)HttpContext.Session.GetInt32("id");

            await _context.librosFavoritos.AddAsync(libroFav);
            await _context.SaveChangesAsync();

            var idLibro = new { id = libroFav.idLibro };
            return RedirectToAction("Details",idLibro);
           
        }
        public async Task<IActionResult> quitarFavorito(int id)
        {
            var libroFav = await _helpers.BuscarLibroFavorito(id, HttpContext.Session.GetInt32("id"));

            _context.librosFavoritos.Remove(libroFav);
            await _context.SaveChangesAsync();

            var idLibro = new { id = libroFav.idLibro };
            return RedirectToAction("Details", idLibro);

        }

        //Califica un libro y recarga la pagina.
        public async Task<IActionResult> puntuar(int idLibro, int puntaje)
        {
            Libro libro = await _context.libros.FindAsync(idLibro);

            _context.librosPuntuados.Add(_helpers.CalificarLibro(idLibro,(int)HttpContext.Session.GetInt32("id"),puntaje));
            _helpers.PuntuarLibro(libro, puntaje);

            await _context.SaveChangesAsync();

            var idDetails = new { id = libro.id };
            return RedirectToAction("Details",idDetails);
        }
        

        //Devuelve un Index con los resultados de la busqueda
        public async Task<IActionResult> Search(string testo)
        {
            
            var resultado = await _helpers.buscarLibros(testo);
            ViewBag.busquedaLibro = testo;
            if(HttpContext.Session.GetString("esAdmin") == "True")
            {
                return View("IndexBusquedaAdmin", resultado);
            }

            return View("IndexBusquedaUser", resultado);
        }

     

    }
}
