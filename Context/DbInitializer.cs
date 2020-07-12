using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Context
{
    public class DbInitializer
    {
        public static void Initialize(OurDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
