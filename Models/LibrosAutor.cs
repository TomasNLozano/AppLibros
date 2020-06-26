using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppLibros.Models
{
    public class LibrosAutor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int idAutor { get; set; }
        public int idLibro { get; set; }

        public LibrosAutor(int autor, int libro)
        {
            idAutor = autor;
            idLibro = libro;
        }
    }
}
