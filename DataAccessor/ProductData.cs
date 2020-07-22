using NetProject.Context;
using NetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetProject.DbAccessor
{
    public class ProductData : DataProvider
    {
        public ProductData(OurDbContext context) : base(context)
        {
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public List<Product> GetPromitionProduct() {
            try
            {
                return _context.Products.Where(pd => pd.Active == 1 && pd.PricePromotion > 0).OrderBy(x => Guid.NewGuid()).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Product>();
            }
        }

        public int CountPromotionProducts(){
            try
            {
                return _context.Products.Where(pd => pd.Active == 1 && pd.PricePromotion > 0).Count();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public List<Product> GetProductByCategory(int idCate)
        {
            try
            {
                return (from products in _context.Products
                        join types in _context.TypeProducts
                        on products.IdType equals types.Id
                        join categories in _context.Categories
                        on types.IdCategory equals categories.Id
                        where categories.Id == idCate
                        select new Product
                        {
                            Id = products.Id,
                            CategoryName = categories.NameCategory,
                            NameProduct = products.NameProduct,
                            ImageProduct = products.ImageProduct,
                            PriceListed = products.PriceListed,
                            PricePromotion = products.PricePromotion,
                            Parameter = products.Parameter,
                            ImageDetailProduct = products.ImageDetailProduct
                        }).ToList();
                        
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Product>();
            }
        }
    }
}
