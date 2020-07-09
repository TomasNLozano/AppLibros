using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppLibros.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using AppLibros.Context;
using AppLibros.Helper_Functions;

namespace AppLibros.Controllers
{

    public class HomeController : Controller
    {


        private readonly LibrosDataBaseContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly Helpers _helpers;

        public HomeController(ILogger<HomeController> logger, LibrosDataBaseContext context)
        {
            _logger = logger;
            _context = context;
            _helpers = new Helpers(_context);
        }

        public IActionResult Index()
        {
            
            HttpContext.Session.SetString("userId", "");
            if (HttpContext.Session.GetString("esAdmin") != "True")
            {
                if (!HttpContext.Session.GetInt32("id").HasValue) 
                {
                    HttpContext.Session.SetInt32("id", 0);
                }
                
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        //nunca llega
        public IActionResult LogIn(String id, String id2)
        {
            Usuario usuario = _context.usuarios.FirstOrDefault(u => (u.username == id && u.password == id2));
            if (usuario != null)
            {
                HttpContext.Session.SetString("username", id);
                HttpContext.Session.SetInt32("id", usuario.id);
                HttpContext.Session.SetString("esAdmin", usuario.esAdmin.ToString());
                ViewBag.logeo = usuario.id;
                var idUsuario = new { id = usuario.id };

                return RedirectToAction(nameof(UsuarioController.Details), nameof(Usuario), idUsuario);
            }
            string error = "Nombre de usuario o contraseña incorrectos";
            ViewBag.Error = error;
            return View("Index");
        }
        public IActionResult desLogear()
        {
            HttpContext.Session.SetString("username", "");
            HttpContext.Session.SetInt32("id", 0);
            HttpContext.Session.SetString("esAdmin", "False");
            return View(nameof(Index));
        }
    }
}
