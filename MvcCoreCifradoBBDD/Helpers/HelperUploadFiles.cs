using Microsoft.AspNetCore.Http;
using MvcCoreCifradoBBDD.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static MvcCoreCifradoBBDD.Provider.PathProvider;

namespace MvcCoreCifradoBBDD.Helpers
{
    public class HelperUploadFiles
    {
        private PathProvider pathProvider;

        public HelperUploadFiles(PathProvider pathProvider)
        {
            this.pathProvider = pathProvider;
        }

        public async Task<string> UploadFileAsync(string nameFile, IFormFile formFile, Folders folder, string filename)
        {

            string path = this.pathProvider.MapPath(nameFile, Folders.Images);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return nameFile;
        }
    }
}
