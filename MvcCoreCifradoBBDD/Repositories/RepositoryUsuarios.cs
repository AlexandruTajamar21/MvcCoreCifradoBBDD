using MvcCoreCifradoBBDD.Data;
using MvcCoreCifradoBBDD.Helpers;
using MvcCoreCifradoBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Repositories
{
    public class RepositoryUsuarios
    {
        UsuariosContext context;
        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        private int GetMaxIdUsuario()
        {
            if(this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }

        public int RegistrarUsuario(string nombre, string email, string password, string imagen)
        {
            int idusuario = this.GetMaxIdUsuario();
            Usuario usuario = new Usuario();
            usuario.IdUsuario = idusuario;
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Imagen = imagen;
            usuario.Salt = HelperCryptography.GenerarSalt();
            usuario.Password = HelperCryptography.EncriptarPassword(password, usuario.Salt);
            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();
            return idusuario;
        }

        public Usuario LogInUsuario(string email, string password)
        {
            Usuario usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == email);
            if(usuario == null)
            {
                return null;
            }
            else
            {
                byte[] passUsuario = usuario.Password;
                string salt = usuario.Salt;
                byte[] temporal = HelperCryptography.EncriptarPassword(password, salt);
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temporal);
                if(respuesta)
                {
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
