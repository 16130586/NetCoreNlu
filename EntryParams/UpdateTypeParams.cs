using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class UpdateTypeParams
    {
        public int Select_nameCate { get; set; }

        public int Select_Status { get; set; }

        public int Id_type { get; set; }

        public string Name_type { get; set; }
        public IFormFile Image_type { get; set; }

        public string Image_type_old { get; set; }
    }
}
