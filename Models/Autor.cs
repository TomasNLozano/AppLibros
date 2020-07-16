using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppLibros.Models
{
    public class Autor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("idAutor")]
        public int id { get; set; }
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [DisplayName("Apellido")]
        public string apellido { get; set; }
        [DisplayName("Libros")]
        public List<Libro> libros { get; set; }

        //public Autor(int id, string nombre, string apellido)
        //{
        //    this.id = id;
        //    this.nombre = nombre;
        //    this.apellido = apellido;
        //    libros = new List<Libro>();
        //}

        //public void agregarLibro(Libro libro)
        //{
        //    libros.Add(libro);
        //}
    }
}
