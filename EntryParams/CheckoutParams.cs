using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.EntryParams
{
    public class CheckoutParams
    {
        public string YourName { get; set; }
        public string YourGender { get; set; }

        public int Select_City { get; set; }

        public int Select_District { get; set; }

        public string YourAddress { get; set; }

        public string YourNumberPhone { get; set; }

        public string YourNote { get; set; }
    }
}
