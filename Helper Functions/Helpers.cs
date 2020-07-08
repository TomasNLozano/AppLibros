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

        public List<Autor> buscarAutores(string key)
        {
            var autores = from Autores in _context.autores
                          where Autores.nombre.Contains(key) || Autores.apellido.Contains(key)
                          select Autores;

            List<Autor> resultado = autores.ToList();

            foreach (Autor autor in resultado)
            {
                autor.libros = new List<Libro>();
                autor.libros = _context.libros.Where(e => e.autorid == autor.id).ToList();
            }

            return resultado;
        }

        public AutoresFavoritos buscarAutorFavorito(int idAutor, int? idUsuario)
        {
            AutoresFavoritos autorFav = _context.autoresFavoritos.FirstOrDefault(t => t.idAutor == idAutor && t.idUsuario == idUsuario);

            return autorFav;
        }

       
    }
}
