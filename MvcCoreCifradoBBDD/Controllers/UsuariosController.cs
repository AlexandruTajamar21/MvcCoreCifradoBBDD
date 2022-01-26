using Microsoft.AspNetCore.Mvc;
using MvcCoreCifradoBBDD.Models;
using MvcCoreCifradoBBDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;

        public UsuariosController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Register(string nombre, string email, string password, string imagen)
        {
            this.repo.RegistrarUsuario(nombre, email, password, imagen);
            ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string email, string password)
        {
            Usuario user = this.repo.LogInUsuario(email, password);
            if(user == null)
            {
                ViewData["MENSAJE"] = "No Tiene Credenciales correctas";
                return View();
            }
            else
            {
                return View(user);
            }
        }
    }
}
