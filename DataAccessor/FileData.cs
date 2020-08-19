using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetProject.Util;
using System;
using System.IO;

namespace NetProject.DataAccessor
{
    public class FileData
    {

        private readonly string _storageUrl;
        private readonly string _deployUrl;
        public FileData(IHostingEnvironment env, IConfiguration appConfig)
        {
            _storageUrl = env.WebRootPath;
            _deployUrl = appConfig["DeployUrl"];
        }
        public string Save(IFormFile formFile)
        {
            var random = new Random();
            var randomString = Convert.ToInt64(Math.Floor((random.NextDouble() * 10))) + "";
            var newName = formFile.Name + DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + randomString + ".png";
            var url = Path.Combine(_storageUrl, newName);
            var size = formFile.Length;
            using (FileStream fs = File.Create(url))
            {
                formFile.CopyTo(fs);
                fs.Flush();
            }
            return _deployUrl + "/" + newName;
        }
        public bool Delete(string fileName)
        {
            return false;
        }
    }
}
