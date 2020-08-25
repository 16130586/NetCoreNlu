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
        public List<Category> GetActiveCategoryProduct() {
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

        public  IEnumerable<Category> GetALL()
        {
            return _context.Categories.ToList();
        }
        public Category GetOne(int id) {
            return _context.Categories.Where(ct => ct.Id == id).FirstOrDefault();
        }

        public bool Update(Category update)
        {
            try
            {
                var requested = GetOne(update.Id);
                requested.Active = update.Active;
                requested.IconCategory = update.IconCategory;
                requested.NameCategory = update.NameCategory;
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool Delete(int id_cate)
        {
            try
            {
                var requested = GetOne(id_cate);
                _context.Categories.Remove(requested);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }
    }
}
