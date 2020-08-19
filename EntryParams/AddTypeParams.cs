using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class AddTypeParams
    {
        public int Select_nameCate { get; set; }
        public string Name_type { get; set; }
        public IFormFile Image_type { get; set; }
    }
}
