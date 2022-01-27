using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreCifradoBBDD.Helpers;
using MvcCoreCifradoBBDD.Models;
using MvcCoreCifradoBBDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MvcCoreCifradoBBDD.Provider.PathProvider;

namespace MvcCoreCifradoBBDD.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;
        private HelperUploadFiles helper;

        public UsuariosController(RepositoryUsuarios repo, HelperUploadFiles helper)
        {
            this.helper = helper;
            this.repo = repo;
        }

        public IActionResult Register(string nombre, string email, string password, string imagen)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password,IFormFile imagen)
        {
            string fileName = imagen.FileName;
            string extension = fileName.Split(".")[1];
            string fileNameGuardar = nombre + "." + extension;
            int idusuario = this.repo.RegistrarUsuario(nombre, email, password, fileNameGuardar);
            fileName = idusuario + "_" + fileName;
            await this.helper.UploadFileAsync(fileNameGuardar,imagen, Folders.Users, fileName);
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
