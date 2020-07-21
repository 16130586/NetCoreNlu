using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Cart
    {
        private Dictionary<int, Product> Data { get; set; }

        public List<Product> List { get { return Data.Values.AsQueryable().Where(s => s.Quantity > 0).ToList(); } }
        public int Total
        {
            get
            {
                var sum = 0;
                var products = Data.Values.AsQueryable().Where(s => s.Quantity > 0).ToList();
                sum = products.Sum(p => p.Quantity * p.PriceListed);
                return sum;
            }
        }

        public Cart()
        {
            Data = new Dictionary<int, Product>();
        }

        public bool Remove(int id)
        {
            return Data.Remove(id);
        }
        public int Put(int id, int quantity)
        {
            if (Data.ContainsKey(id))
            {
                if (Data[id].Quantity < 3)
                {
                    Data[id].Quantity = quantity;
                }
            }
            return Data[id].Quantity;
        }
        public int Put(Product product)
        {
            if (Data.ContainsKey(product.Id))
            {
                if (Data[product.Id].Quantity < 3)
                {
                    Data[product.Id].Quantity++;
                }
            }
            else
            {
                Data.Add(product.Id, product);
            }
            return Data[product.Id].Quantity;
        }
        public Product Get(int id)
        {
            if (Data.ContainsKey(id))
                return Data[id];
            return null;
        }

    }
}
