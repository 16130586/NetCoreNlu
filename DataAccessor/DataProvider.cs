using NetProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class DataProvider
    {
        protected readonly OurDbContext _context;
        public DataProvider(OurDbContext context) {
            _context = context;
        }
    }
}
