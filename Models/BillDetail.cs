using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class BillDetail
    {
        public int Id { get; set; }
        public int IdBill { get; set; }

        public int IdProduct { get; set; }

        public int Quantity { get; set; }

        public int TotalPrice { get; set; }

        public int Active { get; set; }

    }
}
