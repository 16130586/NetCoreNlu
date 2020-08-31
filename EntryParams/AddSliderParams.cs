using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class AddSliderParams
    {
        public IFormFile Image_slider { get; set; }
        public int select_Status { get; set; }

    }
}
