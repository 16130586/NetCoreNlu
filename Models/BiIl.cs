using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int TotalPrice { get; set; }
        public string DateOrder { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public int Active { get; set; }
    }
}
