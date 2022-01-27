using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Provider
{
    public class PathProvider
    {
        public enum Folders {  Images = 0, Documents = 1, Users = 2}

        private IWebHostEnvironment hostEnvironment;

        public PathProvider(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if(folder == Folders.Documents)
            {
                carpeta = "documents";
            }
            else if(folder == Folders.Images){
                carpeta = Path.Combine("images", "users");
            }
            string path = Path.Combine(this.hostEnvironment.WebRootPath, carpeta, fileName);
            return path;
        }
    }
}
