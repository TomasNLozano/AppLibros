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

namespace AppLibros.Helper_Functions
{
    public class Helpers
    {
        private readonly LibrosDataBaseContext _context;

        public Helpers(LibrosDataBaseContext context)
        {
            _context = context;
            
        }

        //Busca autores por nombre, apellido o keyword dentro de cualquiera de los dos y los devuelve como una lista.
        public List<Autor> buscarAutores(string key)
        {
            var autores = from Autores in _context.autores
                          where Autores.nombre.Contains(key) || Autores.apellido.Contains(key)
                          select Autores;

            List<Autor> resultado = autores.ToList();
            CompletarLibros(resultado);

            return resultado;
        }

        //Completa todos los autores de una lista con sus respectivos libros.
        private void CompletarLibros(List<Autor> resultado)
        {
            foreach (Autor autor in resultado)
            {
                autor.libros = new List<Libro>();
                autor.libros = _context.libros.Where(e => e.autorid == autor.id).ToList();
            }
        }

        //Busca un registro de autor favorito. Si existe lo devuelve, de lo contrario devuelve null.
        public async Task<AutoresFavoritos> buscarAutorFavorito(int idAutor, int? idUsuario)
        {
            AutoresFavoritos autorFav = await _context.autoresFavoritos.FirstOrDefaultAsync(t => t.idAutor == idAutor && t.idUsuario == idUsuario);

            return autorFav;
        }

        //Busca un registro de libro favorito. Si existe lo devuelve, de lo contrario devuelve null.
        public async Task<LibrosFavoritos> BuscarLibroFavorito(int idLibro, int? idUsuario)
        {
            var libroFav = await _context.librosFavoritos.FirstOrDefaultAsync(t => t.idLibro == idLibro && t.idUsuario == idUsuario);
            
            return libroFav;
        }

        //Remueve todos los libros de un autor.
       public async void quitarLibros(int autorId)
        {
            var libros = await _context.libros.Where(e => e.autorid == autorId).ToListAsync();
            foreach (Libro libro in libros)
            {
                _context.libros.Remove(libro);
            }
        }

        //Busca un autor por id y devuelve su nombre completo.
        public string BuscarAutor(int id)
        {
            Autor autor = _context.autores.Find(id);
            return autor.nombre + " " + autor.apellido;
        }

        //Busca libros por titulo o keyword dentro del titulo y los devuelve como una lista.
        public async Task<List<Libro>> buscarLibros(string key)
        {
            var libros = from Libros in _context.libros
                         where Libros.titulo.Contains(key)
                         select Libros;

            return await libros.ToListAsync();
        }

        //Busca y devuelve el puntaje que un usuario le asigno a un libro. Si el libro no fue puntuado por este usuario devuelve -1.
        public int BuscarPuntaje(int idLibro, int? idUsuario)
        {
            int puntaje = -1;
            LibrosPuntuados lp = _context.librosPuntuados.FirstOrDefault(e => e.idLibro == idLibro && e.idUsuario == idUsuario);
            if (lp != null)
            {
                puntaje = lp.puntaje;
            }
            return puntaje;
        }

        //Devuelve una entidad de LibroPuntuado.
        public LibrosPuntuados CalificarLibro(int idLibro, int idUsuario, int puntaje)
        {
            LibrosPuntuados lp = new LibrosPuntuados();
            lp.idLibro = idLibro;
            lp.idUsuario = idUsuario;
            lp.puntaje = puntaje;

            return lp;
        }

        //Actualiza los votos y el puntaje acumulado de un libro.
        public void PuntuarLibro(Libro libro, int puntaje)
        {
            libro.puntaje += puntaje;
            libro.votos++;
            _context.Update(libro);
        }

        //Calula y devuelve la calificacion promedio de un libro.
        public double Promedio(Libro libro)
        {
            double promedio = 0;

            if (libro.votos != 0)
            {
                promedio = libro.puntaje / libro.votos;
            }
           
            return promedio;
        }

        //Devuelve una lista de los libros favoritos de un usuario.
        public async Task<List<Libro>> ListarLibrosFavoritos(int id)
        {
            var listaLibro = from Libro in _context.libros
                             join LibrosFavoritos in _context.librosFavoritos on Libro.id equals LibrosFavoritos.idLibro
                             where LibrosFavoritos.idUsuario == id
                             select Libro;

            List<Libro> lista = await listaLibro.ToListAsync();

            return lista;
        }

        //Devuelve una lista de los autores favoritos de un usuario.
        public async Task<List<Autor>> ListarAutoresFavoritos(int id)
        {
            var listaAutor = from Autor in _context.autores
                             join AutoresFavoritos in _context.autoresFavoritos on Autor.id equals AutoresFavoritos.idAutor
                             where AutoresFavoritos.idUsuario == id
                             select Autor;

            List<Autor> lista = await listaAutor.ToListAsync();

            return lista;
        }
    }
}
