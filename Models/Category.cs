using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string NameCategory { get; set; }

        public string IconCategory { get; set; }

        public int Active { get; set; }
    }
}
