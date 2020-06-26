using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppLibros.Models;
using AppLibros.Context;
using System.Diagnostics.Eventing.Reader;


namespace AppLibros.Controllers
{

    public class HomeController : Controller
    {
       

        private readonly LibrosDataBaseContext _context;

        public HomeController(LibrosDataBaseContext context)
        {
            _context = context;
        }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        //public IActionResult LogIn(String user, String pass)
        //{
        //    Usuario usuario = _context.usuarios.First(u => (u.username == user && u.password == pass));
        //    //Session["UsId"] = usuario.id;


        //    return RedirectToAction("Details", "UsuarioController", usuario.id);
        //}

    }
}
