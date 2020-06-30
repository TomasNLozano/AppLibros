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

namespace AppLibros.Controllers
{
    public class AutorController : Controller
    {
        private readonly LibrosDataBaseContext _context;

        public AutorController(LibrosDataBaseContext context)
        {
            _context = context;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {

            var autores = await _context.autores.ToListAsync();
            foreach (Autor autor in autores) 
            {
                autor.libros = new List<Libro>();
                autor.libros = await _context.libros.Where(e => e.autorid == autor.id).ToListAsync();
            } 
            return View(await _context.autores.ToListAsync());
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores
                .FirstOrDefaultAsync(m => m.id == id);
            if (autor == null)
            {
                return NotFound();
            }

            autor.libros = new List<Libro>();
            autor.libros = await _context.libros.Where(e => e.autorid == autor.id).ToListAsync();

            AutoresFavoritos esFav = buscarFavorito(autor.id);
            ViewBag.esFav = esFav;

            ViewBag.idAutor = autor.id;

            return View(autor);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,apellido")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                autor.libros = new List<Libro>();
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,apellido")] Autor autor)
        {
            if (id != autor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.id))
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
            return View(autor);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.autores
                .FirstOrDefaultAsync(m => m.id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.autores.FindAsync(id);
            var libros = await _context.libros.Where(e => e.autorid == id).ToListAsync();
            foreach(Libro libro in libros) 
            {
                _context.libros.Remove(libro);
            }
            _context.autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.autores.Any(e => e.id == id);
        }
        private AutoresFavoritos buscarFavorito(int id)
        {
            return _context.autoresFavoritos.FirstOrDefault(e => e.idAutor == id);
            
        }
        public async Task<IActionResult> agregarFavorito(int id)
        {
            //Task<IActionResult>
            var autorFav = new AutoresFavoritos();
            autorFav.idAutor = id;
            autorFav.idUsuario = (int)HttpContext.Session.GetInt32("id");
            await _context.autoresFavoritos.AddAsync(autorFav);
            await _context.SaveChangesAsync();
            var idAutor = new { id = autorFav.idAutor };
            return RedirectToAction("Details", idAutor);

        }
        public async Task<IActionResult> quitarFavorito(int id)
        {
            //Task<IActionResult>
            var autorFav = await _context.autoresFavoritos.FirstOrDefaultAsync(t => t.idAutor == id && t.idUsuario == HttpContext.Session.GetInt32("id"));

            _context.autoresFavoritos.Remove(autorFav);
            await _context.SaveChangesAsync();
            var idAutor = new { id = autorFav.idAutor };
            return RedirectToAction("Details", idAutor);

        }
    }
}
