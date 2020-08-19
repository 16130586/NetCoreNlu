using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class CategoryData : DataProvider
    {
        public CategoryData(OurDbContext context) : base(context)
        {
        }
        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public List<Category> GetCategoryProduct() {
            try
            {
                return _context.Categories.Where(sld => sld.Active == 1).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Category>();
            }
        }

        public  IEnumerable<Category> getAll()
        {
            return _context.Categories.ToList();
        }
    }
}
