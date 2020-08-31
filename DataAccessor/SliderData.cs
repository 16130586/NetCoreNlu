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
       public List<Slider> GetSlidersIndex()
        {
            try
            {
                return _context.Sliders.Where(sld => sld.Active == 1).ToList(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Slider>();
            }

        }
        public Slider GetSliderById(int id)
        {
            try
            {
                return _context.Sliders.Where(sld => sld.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool UpdateSlider(Slider slider)
        {
            try
            {
                var requested = GetSliderById(slider.Id);
                if (requested == null) return false;
                requested.Id = slider.Id;
                requested.ImageSlider = slider.ImageSlider;
                requested.Active = slider.Active;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public IEnumerable<Slider> GetAll()
        {
            return _context.Sliders.ToList();
        }
        public bool Delete(int id_slider)
        {
            try
            {
                var requested = GetSliderById(id_slider);
                _context.Sliders.Remove(requested);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
