using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string ImageSlider { get; set; }

        public int Active { get; set; }
    }
}
