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
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido { get; set; }
        [Range(1, 99)]
        public int edad { get; set; }
        [Required]
        [MinLength(8)]
        public string username { get; set; }
        [Required]
        [PasswordPropertyText]
        [MinLength(8)]
        public string password { get; set; }
        [DisplayName("Reseñas Favoritas")]
        public List<Libro> librosFavoritos { get; set; }
        [DisplayName("Autores Favoritos")]
        public List<Autor> autoresFavoritos { get; set; }
        public bool esAdmin { get; set; }

        //public Usuario(int id, string nombre, string apellido, int edad, string username, string password)
        //{
        //    this.id = id;
        //    this.nombre = nombre;
        //    this.apellido = apellido;
        //    this.edad = edad;
        //    this.username = username;
        //    this.password = password;
        //    esAdmin = false;
        //    resenasFavoritas = new List<Resena>();
        //    librosFavoritos = new List<Libro>();
        //    autoresFavoritos = new List<Autor>();
        //}

        public void agregarLibroFavorito(Libro libro)
        {
            librosFavoritos.Add(libro);
        }

        public void agregarAutorFavorito(Autor autor)
        {
            autoresFavoritos.Add(autor);
        }
    }
}
