using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialApplication.Services
{
    public class FileSystem
    {
        public static string Savefile(HttpPostedFileBase file,string path, string name,string extension)
        {
            path = path.Trim();
            var ok = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(Path.Combine(ok, "uploads/" + path)))
            {
                
                Directory.CreateDirectory(Path.Combine(ok, "uploads/" + path));
            }
            using (FileStream fileStream = File.Open(Path.Combine(ok, "uploads/" + path) + "/" + name+"."+extension, FileMode.Create))
            {
                file.InputStream.CopyTo(fileStream);
            }
                return $"{name}.{extension}";
        }
    }
}