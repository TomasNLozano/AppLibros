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

namespace AppLibros.Controllers
{

    public class HomeController : Controller
    {


        private readonly LibrosDataBaseContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LibrosDataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            HttpContext.Session.SetString("userId", "");

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
            HttpContext.Session.SetString("username", id);
            HttpContext.Session.SetInt32("id", usuario.id);
            HttpContext.Session.SetString("esAdmin", usuario.esAdmin.ToString());
                      
            return RedirectToAction(nameof(UsuarioController.Details), nameof(UsuarioController), usuario);
        }

    }
}
