using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class SliderData : DataProvider
    {
        public SliderData(OurDbContext context) : base(context)
        {
        }
        public void AddSlider(Slider slider)
        {
            _context.Sliders.Add(slider);
            _context.SaveChanges();
        }
    }
}
