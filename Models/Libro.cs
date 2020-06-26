using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppLibros.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("idLibro")]
        public int id { get; set; }
        [DisplayName("Título")]
        public string titulo { get; set; }
        [DisplayName("ISBN")]
        public string isbn { get; set; }
        [DisplayName("Autor")]
        public int autorid { get; set; }
        [DisplayName("Reseña")]
        public string resena { get; set; }
        public double puntaje { get; set; }
        [DisplayName("Votos")]
        public int votos { get; set; }
       
    }
}
