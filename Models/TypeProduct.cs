using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class TypeProduct
    {
        public int Id { get; set; }
        public int IdCategory { get; set; }
        public string NameType { get; set; }
        public string ImageType { get; set; }
        public int Active { get; set; }

    }
}
