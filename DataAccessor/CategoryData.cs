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
    }
}
