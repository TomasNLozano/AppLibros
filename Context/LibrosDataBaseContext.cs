using AppLibros.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppLibros.Context
{
    public class LibrosDataBaseContext : DbContext
    {
        public
            LibrosDataBaseContext(DbContextOptions<LibrosDataBaseContext> options) : base(options)
        {
        }
        public DbSet<Autor> autores { get; set; }
        public DbSet<Libro> libros { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<LibrosFavoritos> librosFavoritos { get; set; }
        public DbSet<AutoresFavoritos> autoresFavoritos { get; set; }
        public DbSet<LibrosPuntuados> librosPuntuados { get; set; }
        

        
    }
}