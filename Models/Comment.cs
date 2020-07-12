using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int IdProduct { get; set; }

        public int ContentComment { get; set; }

        public string DateComment { get; set; }

        public int Active { get; set; }
    }
}
