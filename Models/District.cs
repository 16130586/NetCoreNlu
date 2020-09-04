using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public int CityId { get; set; }
    }
}
