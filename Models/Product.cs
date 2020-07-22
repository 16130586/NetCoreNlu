using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int IdType { get; set; }

        public string NameProduct { get; set; }

        public int Quantity { get; set; }

        public int PriceListed { get; set; }

        public int PricePromotion { get; set; }

        public string ImageProduct { get; set; }

        public string ImageDetailProduct { get; set; }

        public string Parameter { get; set; }

        public string Description { get; set; }

        public int NumLike { get; set; }

        public string DateCreated { get; set; }

        public int Active { get; set; }

        public string CategoryName { get; set; }

    }
}
