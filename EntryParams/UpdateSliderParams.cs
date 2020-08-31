using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class UpdateSliderParams
    {
        public int Id_Slider { get; set; }/*id_slider và select_status */
        public int Select_status { get; set; }
        public IFormFile Image_slider { get; set; }

        public string Image_slider_old { get; set; }
    }
}

