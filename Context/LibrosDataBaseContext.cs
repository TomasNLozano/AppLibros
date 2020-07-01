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
        //public DbSet<LibrosAutor> libros_Autor { get; set; }

        //Agregar DBset de librosXAutor y volver a hacer toda la puta base de la reconcha de tu vieja miriam y chrischu pasion. P.S. El gordo salchi se nos caga de risa en la jeta mientras nosotros intentamos remas un yate en dulce de leche repostero hecho con leche de pinguino
    }
}