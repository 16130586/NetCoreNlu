using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetProject.Models
{
    public class Cart
    {
        [JsonExtensionData]
        public Dictionary<string, Product> Data { get; set; }
        [JsonExtensionData]
        public Dictionary<string, int> Quantities { get; set; }

        public List<Product> List { get { return Data.Values.AsQueryable().Where(s => Quantities[s.Id + ""] > 0).ToList(); } }
        public int Total
        {
            get
            {
                var sum = 0;
                var products = List;
                sum = products.Sum(p => Quantities[p.Id + ""] * p.PricePromotion);
                return sum;
            }
        }

        public Cart()
        {
            Data = new Dictionary<string, Product>();
            Quantities = new Dictionary<string, int>();
        }

        public bool Remove(int id)
        {
            return Data.Remove(id + "") && Quantities.Remove(id + "");
        }
        public int Put(int id, int quantity)
        {
            var key = id + "";
            if (Data.ContainsKey(key))
            {
                if (quantity <= 3)
                    Quantities[key] = quantity;
            }
            return Quantities[key];
        }
        public int Put(Product product , int quantity)
        {
            var key = product.Id + "";
            if (Data.ContainsKey(key))
            {
                if ((Quantities[key]  + quantity ) <= 3)
                {
                    Quantities[key] = Quantities[key] + quantity;
                }
            }
            else
            {
                Data.Add(key, product);
                Quantities.Add(key, quantity);
            }
            return Quantities[key];
        }
        public Product Get(int id)
        {
            if (Data.ContainsKey(id + ""))
                return Data[id + ""];
            return null;
        }

    }
}
