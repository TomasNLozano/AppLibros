using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppLibros.Models
{
    public class Usuario
    {
        [Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ForeignKey("idUsuario")]
        public int id { get; set; }
        [DisplayName("Nombre")]
        [Required]
        public string nombre { get; set; }
        [DisplayName("Apellido")]
        [Required]
        public string apellido { get; set; }
        [Range(12, 99)]
        public int edad { get; set; }
        [Required]
        [EmailAddress]
        [DisplayName("Nombre de usuario")]
        public string username { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña")]
        public string password { get; set; }
        [DisplayName("Libros Favoritos")]
        public List<Libro> librosFavoritos { get; set; }
        [DisplayName("Autores Favoritos")]
        public List<Autor> autoresFavoritos { get; set; }
        public bool esAdmin { get; set; }
        public List<LibrosPuntuados> librosPuntuados { get; set; }
    }
}
