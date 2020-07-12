using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class TypeProductData : DataProvider
    {
        public TypeProductData(OurDbContext context) : base(context)
        {
        }
        public void AddType(TypeProduct typeProduct)
        {
            _context.TypeProducts.Add(typeProduct);
            _context.SaveChanges();
        }
    }
}

